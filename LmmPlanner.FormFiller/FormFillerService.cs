using System;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using LmmPlanner.LmmFormFiller.Business;
using LmmPlanner.LmmFormFiller.Entities;
using LmmPlanner.Entities.Interfaces;

namespace LmmPlanner.LmmFormFiller
{
    public partial class FormFillerService : IFormFillerService
    {
        public string DbPath { get; }
        public string S89Path { get; }
        public string ExportPath { get; }

        public FormFillerService(string dbPath, string s89Path, string exportPath)
        {
            this.DbPath = dbPath;
            this.S89Path = s89Path;
            this.ExportPath = exportPath;
        }
        public void Export(DateTime from, DateTime to)
        {
            Console.WriteLine("-------------------");
            Console.WriteLine("LmmFormFiller Start");

            DateTime dt = DateTime.Now;
            string excelPath = $"{S89Path}.xlsx";
            int iter2 = 1;
            var scheds = ReadMe(from, to).OrderBy(d => d.datum).ThenBy(d => d.artId);
            ClosedXML.Excel.XLWorkbook ex;
            if (System.IO.File.Exists(excelPath))
            {
                ex = new ClosedXML.Excel.XLWorkbook(excelPath);
            }
            else
            {
                ex = new ClosedXML.Excel.XLWorkbook(excelPath);
            }
            var blubb = ex.AddWorksheet();
            foreach (var sched in scheds)
            {
                blubb.Cell(iter2, 1).Value = sched.datum.ToShortDateString();
                blubb.Cell(iter2, 2).Value = sched.name;
                blubb.Cell(iter2, 3).Value = sched.art;
                iter2++;
                // ex.SaveAs();
                string dateiname = $"S-89_{sched.datum:MM-dd}_{sched.artId:000}_{sched.name.Replace(" ", "_")}.pdf";
                string path = $"{ExportPath}{from:yyyy-MM-dd}_89/";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                string outputPath = @$"{path}{dateiname}";
                using (Stream pdfInputStream = new FileStream(path: S89Path, mode: FileMode.Open))
                using (Stream resultPDFOutputStream = new FileStream(path: outputPath, mode: FileMode.Create))
                using (MemoryStream resultPDFStream = FillForm(pdfInputStream, sched))
                {
                    resultPDFStream.Position = 0;
                    resultPDFStream.CopyTo(resultPDFOutputStream);
                    var imgService = new ImageService();
                    byte[]? up = imgService.PdfToImage(resultPDFStream.ToArray());
                    if (up != null)
                    {
                        System.IO.File.WriteAllBytes(outputPath.Replace(".pdf", ".png"), up);
                    }
                }
                var anschreiben = ErzeugeAnschreiben(sched);
                System.IO.File.WriteAllText($"{outputPath}.txt", anschreiben);
            }
            ex.Save();
            Console.WriteLine("-------------------");
            Console.WriteLine("LmmFormFiller Ende");
        }
        public static string ErzeugeAnschreiben(Sched sched)
        {
            var anschreiben = System.IO.File.ReadAllText("App_Data/anschreiben.txt");
            var art = "";
            var partner = sched.male ? "Dein Partner" : "Deine Partnerin";
            var duPartner = sched.male ? "du oder dein Partner" : "du oder deine Partnerin";
            if (sched.Bibellesung)
            {
                art = $"die Bibellesung ({sched.quelle})";
                art = $"Bibellesung ({sched.quelle})";
                partner = "";
                duPartner = "du";
            }
            else if (sched.ErstesGespraech)
            {
                // art = "das erste Gespräch";
                art = "Erstes Gespräch";
                partner = $"{partner} ist {sched.partner}.";
            }
            else if (sched.Rueckbesuch)
            {
                // art = "den Rückbesuch";
                art = "Rückbesuch";
                partner = $"{partner} ist {sched.partner}.";
            }
            else if (sched.Bibelstudium)
            {
                // art = "das Bibelstudium";
                art = "Bibelstudium";
                partner = $"{partner} ist {sched.partner}.";
            }
            else if (sched.Vortrag)
            {
                // art = "den 5-Minuten-Vortrag";
                art = "5-Minuten-Vortrag";
                partner = $"({sched.quelle})";
                duPartner = "du";
            }
            anschreiben = anschreiben.Replace("[*Vorname*]", sched.vorname)
                .Replace("[*Datum*]", sched.datum.ToShortDateString())
                .Replace("[*Art*]", art)
                .Replace("[*Partner*]", partner)
                .Replace("[*DuPartner*]", duPartner);
            return anschreiben;
        }

        public IEnumerable<Sched> ReadMe(DateTime from, DateTime to)
        {
            var result = new List<Sched>();
            var path = DbPath;
            using (var connection = new SqliteConnection(@$"Data Source={path}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                var abfrage = System.IO.File.ReadAllText("App_Data/abfrage.sql");
                command.CommandText = abfrage;
                command.Parameters.Add(new SqliteParameter("@from", from.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqliteParameter("@to", to.ToString("yyyy-MM-dd")));
                // command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sched = new Sched
                        {
                            name = $"{reader["Verkündiger"]}",
                            partner = $"{reader["Partner"]}",
                            art = $"{reader["Art"]}",
                            quelle = $"{reader["Quelle"]}",
                            vorname = $"{reader["vorname"]}",
                            lektion = int.Parse($"{reader["Lektion"]}"),
                            dienstwoche = int.Parse($"{reader["Dienstwoche"]}") == 1,
                            artId = $"{reader["ArtId"]}",
                            datum = reader.GetDateTime(reader.GetOrdinal("datum")).AddDays(4),
                            male = $"{reader["Geschlecht"]}" == "B"
                            // datum = $"{reader["datum"]}"
                        };
                        if (sched.dienstwoche)
                        {//.datum == new DateTime(2022, 9, 16)){
                            sched.datum = sched.datum.AddDays(-3);
                        }
                        result.Add(sched);
                        Console.WriteLine($"{sched}");
                    }
                }
            }
            return result;
        }

        public static MemoryStream FillForm(Stream inputStream, Sched sched)
        {
            MemoryStream outStream = new MemoryStream();
            PdfReader? pdfReader = null;
            PdfStamper? pdfStamper = null;
            Stream? inStream = null;
            try
            {
                var pdfLoader = PdfBaseService.LoadPdfReader(inputStream);
                pdfReader = pdfLoader.PdfReader;

                pdfStamper = new PdfStamper(pdfReader, outStream); //, '\0', true);
                // var haha = pdfStamper.AcroFields.GetAppearanceStates("900_4_CheckBox");
                AcroFields form = pdfStamper.AcroFields;
                // form.GetFieldItem("900_4_Checkbox")
                // form.SetFieldProperty("900_4_CheckBox", "textsize", 1f, null);
                // form.SetFieldProperty("900_4_CheckBox", "textfont", BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.EMBEDDED), null);
                form.SetField("900_1_Text", sched.name);
                form.SetField("900_2_Text", $"{sched.partnerText}");
                form.SetField("900_3_Text", $"{sched.datum:dd.MM.yyyy}");
                form.SetField("900_4_CheckBox", sched.Bibellesung ? "Yes" : "No"); // Bibellesung
                form.SetField("900_5_CheckBox", sched.ErstesGespraech ? "Yes" : "No"); // Erstes Gespräch
                form.SetField("900_6_Text", sched.ErstesGespraechText);
                form.SetField("900_7_CheckBox", sched.Rueckbesuch ? "Yes" : "No"); // Rückbesuch
                form.SetField("900_8_Text", sched.RueckbesuchText);
                form.SetField("900_9_CheckBox", sched.Bibelstudium ? "Yes" : "No"); // Bibelstudium
                form.SetField("900_10_CheckBox", sched.Vortrag ? "Yes" : "No"); // Vortrag
                form.SetField("900_11_CheckBox", "No"); // Anderes
                form.SetField("900_12_Text", sched.AnderesText);
                form.SetField("900_13_CheckBox", "Yes"); // Saal
                form.SetField("900_14_CheckBox", "No"); // Nebenraum 1
                form.SetField("900_15_CheckBox", "No"); // Nebenraum 2
                // pdfStamper.FormFlattening = true;
                // pdfStamper.AcroFields.GenerateAppearances = true;
                //pdfStamper.AcroFields.SubstitutionFonts = null; //.GenerateAppearances = false;
                // pdfStamper.FreeTextFlattening = true;
                // pdfStamper.FormFlattening = true;
                // pdfStamper.FormFlattening = true;
                // set this if you want the result PDF to not be editable. 
                //pdfStamper.FormFlattening = true;
                return outStream;
            }
            finally
            {
                pdfStamper?.Close();
                pdfReader?.Close();
                inStream?.Close();
            }
        }

    }
}
