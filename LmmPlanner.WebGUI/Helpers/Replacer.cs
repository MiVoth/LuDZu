using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using HtmlAgilityPack;

namespace DocumentForger
{
    // class TReplacer<TEntity> : Replacer //, IReplacer<TEntity>
    // {
    //     public TReplacer(string input) : base(input)
    //     {

    //     }
    //     public void AddReplacementRelation<R>(Expression<Func<R>> expression)
    //     {
    //         R value = expression.Compile()();
    //         MemberExpression body = expression.Body as MemberExpression;
    //         string n = string.Empty;
    //         var val = value;
    //         if (val != null)
    //         {
    //             var vtype = val.GetType();
    //             if (vtype == typeof(DateTime))
    //             {
    //                 n = (DateTime.Parse(value.ToString())).ToShortDateString();
    //             }
    //             else if (vtype == typeof(bool))
    //             {
    //                 n = (bool.Parse(val.ToString())) ? "Ja" : "Nein";
    //             }
    //             else
    //             {
    //                 n = value.ToString();
    //             }
    //         }
    //         AddStringReplacement(body.Member.Name, n);
    //         //ReplacementRelation.Add(body.Member.Name, n);
    //     }
    // }

    class Replacer //: IPdfReplacer
    {
        public bool Blue { get; set; } = false;
        private readonly string _input;
        public string ReplacePattern { get; set; } = "[*{0}*]";
        public string RemovePatternStart { get; set; } = "[?{0}?]";
        public string RemovePatternEnd { get; set; } = "[?/{0}?]";

        public List<Exception> Errors { get; set; }

        private string NullToString(object o) => $"{0}";
        // {
        //     if (o == null)
        //     {
        //         return string.Empty;
        //     }
        //     return o.ToString();
        // }
        public Replacer(string input)
        {
            _input = input;
            Errors = new();
        }

        public Dictionary<string, IReplacement> ReplacementRelation { get; set; } = new Dictionary<string, IReplacement>();

        /// <summary>
        /// Der Key gibt das Schlüsselwort an, nach dem gesucht werden soll
        /// Bei Value = False wird nur der Tag entfernt aber nicht der Inhalt, = True alles dazwischen wird entfernt
        /// </summary>
        public Dictionary<string, bool> RemoveRelation { get; set; } = new Dictionary<string, bool>();

        public string GetText<T>(T obj)
        {
            return GetText(obj, typeof(T));
        }
        public string GetText(object? obj = null, Type? t = null)
        {
            // Blue = true;
            if (obj != null && t != null)
            {
                TypeReflector.Reflector(obj, t, this);
            }
            StringBuilder sb = new StringBuilder(_input);
            foreach (var key in ReplacementRelation.Keys)
            {
                switch (ReplacementRelation[key].Type)
                {
                    case ReplacementType.Html:
                    case ReplacementType.String:
                        var rr = (string)ReplacementRelation[key].Value;
                        if (string.IsNullOrEmpty(rr))
                        {
                            rr = "&nbsp;";
                        }
                        if (Blue)
                        {
                            rr = $"<span class=\"blue-highlight\">{rr}</span>";
                        }
#if DEBUG
#endif
                        sb.Replace(string.Format(ReplacePattern, key), rr);
                        break;
                    case ReplacementType.List:
                        var liste = (List<string>)ReplacementRelation[key].Value;
                        string bluecss = Blue ? " class=\"blue-highlight\" " : "";
                        StringBuilder sb2 = new StringBuilder($"<ul {bluecss}>");
                        foreach (var item in liste)
                        {
                            sb2.Append($"<li>{item}</li>");
                        }
                        sb2.Append("</ul>");
                        sb.Replace(string.Format(ReplacePattern, key), sb2.ToString());
                        break;
                    case ReplacementType.Image:

                        if (ReplacementRelation[key].Value.GetType() == typeof(byte[]))
                        {
                            var by = (byte[])ReplacementRelation[key].Value;
                            var str = Convert.ToBase64String(by);
                            sb.Replace(string.Format(ReplacePattern, key), str);
                        }
                        else
                        {
                            sb.Replace(string.Format(ReplacePattern, key), (string)ReplacementRelation[key].Value);
                        }
                        break;
                    case ReplacementType.Docx:
                        break;
                    case ReplacementType.Template: // template funktioniert hier nicht
                        // var tbl = (System.Collections.IEnumerable)ReplacementRelation[key].Value;
                        // var tabelle = GetTabelle(tbl);
                        //sb.Replace(string.Format(ReplacePattern, key), tabelle);
                        break;
                    default:
                        break;
                }
            }
            string txt = sb.ToString();
            txt = AgilityReplaceAndRemove(txt);

            foreach (string key in RemoveRelation.Keys)
            {
                txt = CheckAndReplaceIf(key, RemoveRelation[key], txt);
            }
            if (Blue)
            {
                var css = ".green-highlight, span[data-remove], div[data-remove],span[data-remove-not], div[data-remove-not] { /*color: darkgreen;*/ } .blue-highlight, *[data-replace], *[data-replace][data-remove] { /*color: blue;*/ }";
                if (txt.Contains("<style>"))
                {
                    txt = txt.Replace("<style>", $"<style> {css}");
                }
                else
                {
                    txt = txt.Replace("<style type=\"text/css\">", $"<style type=\"text/css\"> {css}");
                }
            }
            return txt;
        }

        // private string GetTabelle(System.Collections.IEnumerable tbl)
        // {
        //     if (tbl != null)
        //     {
        //         var tableSb = new StringBuilder("<table class=\"reclection-table\">");
        //         var first = true;
        //         foreach (var item in tbl)
        //         {
        //             var pp = item.GetType().GetPublicProperties();
        //             if (first)
        //             {
        //                 tableSb.Append("<thead><tr>");
        //                 foreach (var p in pp)
        //                 {
        //                     tableSb.Append($"<th>{p.Name}</th>");
        //                 }
        //                 tableSb.Append("</tr></thead><tbody>");
        //                 first = false;
        //             }
        //             tableSb.Append($"<tr>");
        //             foreach (var p in pp)
        //             {
        //                 var st = p.GetValue(item);
        //                 var myval = "";
        //                 if (st != null)
        //                 {
        //                     myval = st.ToString();
        //                 }
        //                 tableSb.Append($"<td>{myval}</td>");
        //             }
        //             tableSb.Append($"</tr>");
        //         }
        //         tableSb.Append("</tbody></table>");
        //         return tableSb.ToString();
        //     }
        //     return string.Empty;
        // }

        private string GetReplacement(ReplacementType rtype, object value1)
        {
            string result = "";
            switch (rtype)
            {
                case ReplacementType.String:
                    string rr = (string)value1;
                    if (string.IsNullOrEmpty(rr))
                    {
                        rr = "";
                    }
                    result = rr;
                    break;
                case ReplacementType.List:
                    List<string> liste = (List<string>)value1;
                    StringBuilder sb2 = new StringBuilder($"<ul>");
                    foreach (string item in liste)
                    {
                        sb2.Append($"<li>{item}</li>");
                    }
                    sb2.Append("</ul>");
                    result = sb2.ToString();
                    break;
                case ReplacementType.Image:
                    if (value1.GetType() == typeof(byte[]))
                    {
                        byte[] by = (byte[])value1;
                        string str = Convert.ToBase64String(by);
                        result = str;
                    }
                    else
                    {
                        result = (string)value1;
                    }
                    break;
                case ReplacementType.Html:
                    string value = (string)value1;
                    value = value ?? "";
                    result = value;
                    //node.InnerHtml = value;
                    //node.Attributes.RemoveAll();
                    break;
                    //case ReplacementType.Docx:
                    //    break;
                    // case ReplacementType.Template:
                    //     var tbl = (System.Collections.IEnumerable)value1;
                    //     var tabelle = GetTabelle(tbl);
                    //     result = tabelle;
                    //node.InnerHtml = tabelle;
                    // break;
                    //default:
                    //    break;
            }
            return result;
        }
        public string ReplaceDataName { get; set; } = "data-replace";
        public string ShowDataName { get; set; } = "data-show"; //"data-remove";
        public string HideDataName { get; set; } = "data-hide"; //"data-remove";
        private string AgilityReplaceAndRemove(string txt)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(txt);


            foreach (var key in ReplacementRelation.Keys)
            {
                //var replaceNodes = doc.DocumentNode.SelectNodes($"//span[@data-replace='{key}']|//div[@data-replace='{key}']");
                //var replaceNodes = doc.DocumentNode.SelectNodes($"//span[@data-replace='{key}']|//div[@data-replace='{key}']");
                HtmlNodeCollection? replaceNodes = doc.DocumentNode.SelectNodes($"//*[@{ReplaceDataName}='{key}']");

                if (replaceNodes != null)
                {
                    foreach (var node in replaceNodes)
                    {
                        bool done = node.GetAttributeValue("data-done", "") == "true";
                        // if (done)
                        // {
                        //     node.SetAttributeValue("data-hier", "hihi");
                        // }
                        if (!done)
                        {
                            if (ReplacementRelation[key].Type == ReplacementType.Html)
                            {
                                node.Attributes.RemoveAll();
                            }

                            if (ReplacementRelation[key].Type == ReplacementType.Template)
                            {
                                try
                                {
                                    IEnumerable liste = (IEnumerable)ReplacementRelation[key].Value;
                                    StringBuilder sb = new("");

                                    foreach (object item in liste)
                                    {
                                        HtmlNode clone = node.CloneNode(true);
                                        string r = clone.InnerHtml;
                                        Replacer rpl = new(r);
                                        // var t = liste.GetType().GetGenericArguments()[0];
                                        Type itemType = item.GetType();
                                        //sb.Append(rpl.GetText(item, t));
                                        clone.InnerHtml = rpl.GetText(item, itemType);
                                        node.ParentNode.InsertBefore(clone, node);
                                    }
                                    node.Remove();
                                    //node.OuterHtml = sb.ToString();
                                }
                                catch (Exception) { }
                                //ReplacementRelation[key].Type;
                            }
                            else
                            {
                                node.InnerHtml = GetReplacement(ReplacementRelation[key].Type, ReplacementRelation[key].Value);

                            }
                        }
                        node.SetAttributeValue("data-done", "true");
                    }
                }
            }

            foreach (var key in RemoveRelation.Keys)
            {
                HtmlNodeCollection? removeNodes = doc.DocumentNode.SelectNodes($"//*[@{ShowDataName}='{key}']");
                if (removeNodes != null)
                {
                    if (!RemoveRelation[key])
                    {
                        foreach (HtmlNode? node in removeNodes)
                        {
                            node.ParentNode.RemoveChild(node);
                        }
                    }
                }
                // remove-not
                removeNodes = doc.DocumentNode.SelectNodes($"//*[@{HideDataName}='{key}']");
                if (removeNodes != null)
                {
                    if (RemoveRelation[key])
                    {
                        foreach (HtmlNode? node in removeNodes)
                        {
                            node.ParentNode.RemoveChild(node);
                        }
                    }
                }

            }
            using (var sw = new StringWriter())
            {
                doc.Save(sw);
                return sw.ToString();
            }
        }

        private string CheckAndReplaceIf(string tagname, bool? val, string html)
        {
            string result = "";
            var yes = val ?? false;
            string starttag = string.Format(RemovePatternStart, tagname); // "[?" + tagname + "?]";
            string endtag = string.Format(RemovePatternEnd, tagname); // "[?/" + tagname + "?]";
            if (!yes)
            {
                int first = html.IndexOf(starttag);
                int last = html.IndexOf(endtag);
                if (first > -1 && last > -1)
                {
                    last += endtag.Length;
                    result = html.Substring(0, first) + html.Substring(last, html.Length - last);
                }
                else
                {
                    result = html.Replace(starttag, "");
                    result = result.Replace(endtag, "");
                }
            }
            else
            {
                string begin = "";
                string end = "";
                if (Blue)
                {
                    begin = "<span class=\"green-highlight\">";
                    end = "</span>";
                }
                result = html.Replace(starttag, begin);
                result = result.Replace(endtag, end);
            }
            return result;
        }

        public void AddString(string pattern, string replacement)
        {
            AddStringReplacement(pattern, replacement);
            AddRemoveRelation(pattern, !string.IsNullOrEmpty(replacement));
        }

        public void AddStringReplacement(string pattern, string replacement)
        {
            if (ReplacementRelation.ContainsKey(pattern))
            {
                ReplacementRelation[pattern] = new Replacement { Type = ReplacementType.String, Value = replacement };
            }
            else
            {
                ReplacementRelation.Add(pattern, new Replacement { Type = ReplacementType.String, Value = replacement });
            }
        }

        public void AddImageReplacement(string pattern, string replacement)
        {
            ReplacementRelation.Add(pattern, new Replacement { Type = ReplacementType.Image, Value = replacement });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern">Muster nach dem gesucht wird</param>
        /// <param name="val">true => wird angezeigt und tags entfernt; false => text wird entfernt </param>
        public void AddRemoveRelation(string pattern, bool val)
        {
            if (RemoveRelation.ContainsKey(pattern))
            {
                RemoveRelation[pattern] = val;
            }
            else
            {
                RemoveRelation.Add(pattern, val);
            }
        }
    }
}
