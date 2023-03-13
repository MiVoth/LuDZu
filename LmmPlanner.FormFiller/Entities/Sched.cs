using System;

namespace LmmPlanner.LmmFormFiller.Entities;
public class Sched
{
    public string artId { get; set; } = string.Empty;
    public bool Bibellesung { get => artId.StartsWith("4"); }
    public bool ErstesGespraech { get => artId.StartsWith("6"); }
    public bool Rueckbesuch { get => art.Contains("Rück"); }  //artId.StartsWith("70") || artId.StartsWith("71"); }
    public bool Bibelstudium { get => art.Contains("Bibels"); }  //artId.StartsWith("100"); }
    public bool Vortrag { get => artId.StartsWith("110"); }
    internal int lektion;
    internal string vorname { get; set; } = string.Empty;
    internal string quelle { get; set; } = string.Empty;
    internal string art { get; set; } = string.Empty;
    internal string partner { get; set; } = string.Empty;
    internal string partnerText
    {
        get => string.IsNullOrEmpty(partner) ? $"-- Quelle: {quelle} --" : partner;
    }
    internal string name { get; set; } = string.Empty;
    internal DateTime datum;
    internal bool dienstwoche;
    internal bool male;

    public string ErstesGespraechText { get => ErstesGespraech || Bibellesung ? $"th Lektion {lektion}" : ""; }
    public string RueckbesuchText { get => Rueckbesuch ? $"th Lektion {lektion}" : ""; }
    public string AnderesText { get => Vortrag || Bibelstudium ? $"th Lektion {lektion}" : ""; }

    public override string ToString()
    {
        return $"{name} {partner}, {art}, {quelle}, {lektion}, {vorname}";
    }
}