using AngleSharp.Dom;
using AngleSharp.Html;
using AngleSharp.Html.Parser;
using PocketIS.Services.Interfaces;
using System.Text.RegularExpressions;

namespace PocketIS.ReportGenerator
{
    public static class HtmlGenerator
    {
        public static async Task<string> RenderHtml<T>(
            IViewConfig viewConfig,
            IReportModel<T> model,
            IRenderRazorToStringService renderService)
        {
            var pages = model.GetSubReports().ToArray();
            if (pages.Length < 1)
            {
                return string.Empty;
            }

            var content = await renderService.RenderToStringAsync(viewConfig.Body, pages[0], true).ConfigureAwait(false);
            content = Cleanup(content);
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);

            for (var i = 1; i < pages.Length; i++)
            {
                var section = await renderService.RenderToStringAsync(viewConfig.Body, pages[i], true).ConfigureAwait(false);
                section = Cleanup(section);
                var sectionHtml = parser.ParseDocument(section);

                document.Body.AppendNodes(sectionHtml.Body.ChildNodes.ToArray());
            }

            using (var writer = new StringWriter())
            {
                document.ToHtml(writer, new PrettyMarkupFormatter { Indentation = "  ", NewLine = "\n" });
                var html = writer.ToString();

                return html;
            }
        }

        private static string Cleanup(string content)
        {
            content = Regex.Replace(content, @"\n|\r", string.Empty);
            content = Regex.Replace(content, @"(\s)\1+", @"$1");
            return content;
        }
    }
}
