using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LmmPlanner.WebGUI.TagHelpers
{
    [HtmlTargetElement("bool-icon")]
    public class BooleanIconTagHelper : TagHelper
    {
        public bool? IsChecked { get; set; }
        public BooleanIconTagHelper()
        {
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            var header = new TagBuilder("span");
            header.AddCssClass("bi");
            if (IsChecked != true)
            {
                header.AddCssClass("bi-square");
            }
            else
            {
                header.AddCssClass("bi-check-square");
            }
            output.PreContent.AppendHtml(header.GetString());
            var ce = await output.GetChildContentAsync();
            var childString = ce.GetContent();
            if (!string.IsNullOrEmpty(childString))
            {
                output.Content.AppendHtml(ce.GetContent());
            }
            // return base.ProcessAsync(context, output);
        }
    }

    public static class IHtmlContentExtensions
{
    public static string GetString(this Microsoft.AspNetCore.Html.IHtmlContent content)
    {
        using (var writer = new System.IO.StringWriter())
        {        
            content.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
}