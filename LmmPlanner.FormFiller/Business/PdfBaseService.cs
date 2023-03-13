using System;
using System.Collections;
using System.IO;
using iTextSharp.text.pdf;
using LmmPlanner.LmmFormFiller.Entities;

namespace LmmPlanner.LmmFormFiller.Business
{
    public static class PdfBaseService
    {
        public static PdfReader RepairBrokenFormFields(PdfReader reader)
        {
            // PdfReader reader = new PdfReader(src);
            PdfDictionary root = reader.Catalog;
            PdfDictionary form = root.GetAsDict(PdfName.Acroform);
            PdfArray fields = form.GetAsArray(PdfName.Fields);

            PdfDictionary page;
            PdfArray annots;
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                page = reader.GetPageN(i);
                annots = page.GetAsArray(PdfName.Annots);
                for (int j = 0; j < annots.Size; j++)
                {
                    fields.Add(annots.GetAsIndirectObject(j));
                }
            }
            using var ms = new MemoryStream();
            PdfStamper stamper = new PdfStamper(reader, ms); // new FileStream(dest, FileMode.Create));
            stamper.Close();
            reader.Close();
            return new PdfReader(ms.ToArray());
        }
        public static ICollection GetFormFields(Stream pdfStream)
        {
            PdfReader? reader = null;
            try
            {
                PdfReader pdfReader = new PdfReader(pdfStream);
                AcroFields acroFields = pdfReader.AcroFields;
                if (acroFields.Fields.Keys.Count == 0)
                {
                    var newReader = RepairBrokenFormFields(pdfReader);
                    acroFields = newReader.AcroFields;
                    // return acroFields.Fields.Keys;
                    // var form = pdfReader.Catalog.GetAsDict(PdfName.Acroform);
                    // var fields = form.GetAsArray(PdfName.Fields);
                    // return pdfReader.AcroForm.Keys;
                }
                foreach (var item in acroFields.Fields.Keys)
                {
                    Console.WriteLine($"{item}, {acroFields.GetField(item.ToString())}");
                }
                return acroFields.Fields.Keys;
            }
            finally
            {
                reader?.Close();
            }
        }

        public static PdfLoader LoadPdfReader(Stream inputStream)
        {
            var pdfReader = new PdfReader(inputStream);
            if (pdfReader.AcroFields.Fields.Keys.Count == 0)
            {
                pdfReader = RepairBrokenFormFields(pdfReader);
            }
            return new PdfLoader
            {
                PdfReader = pdfReader
            };
        }

    }
}