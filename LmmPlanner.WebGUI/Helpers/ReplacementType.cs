using System;
using System.Reflection;

namespace DocumentForger
{
    public enum ReplacementType
    {
        Html,
        String,
        List,
        Image,
        Docx,
        Template
    }

    public class Replacement : IReplacement
    {
        public ReplacementType Type { get; set; }
        public object Value { get; set; } = null!;
    }

    public interface IReplacement
    {
        ReplacementType Type { get; set; }
        object Value { get; set; }

    }
    // public static class Extension
    // {
    //     public static PropertyInfo[] GetPublicProperties(this Type type)
    //     {
    //         var pup = type.GetProperties(); //BindingFlags.Instance, BindingFlags.Public);
    //         return pup;
    //     }
    // }

    // public static class TypeReflector
    // {
    //     internal static void Reflector(object obj, Type t, Replacer replacer)
    //     {
    //         var pup = t.GetPublicProperties();
    //         foreach (var prop in pup)
    //         {
    //             if (prop.PropertyType == typeof(string))
    //             {
    //                 replacer.AddStringReplacement(prop.Name, $"{prop.GetValue(obj)}");
    //             }
    //             else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
    //             {
    //                 replacer.AddStringReplacement(prop.Name, $"{prop.GetValue(obj):dd.MM.yyyy}");
    //             }
    //             else if (prop.PropertyType == typeof(int?) || prop.PropertyType == typeof(int)
    //             || prop.PropertyType == typeof(long?) || prop.PropertyType == typeof(long))
    //             {
    //                 replacer.AddStringReplacement(prop.Name, $"{prop.GetValue(obj)}");
    //             }
    //         }
    //     }
    // }
}