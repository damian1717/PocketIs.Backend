using PdfSharp.Fonts;
using PocketIS.Infrastucture.Extensions;

namespace PocketIS.ReportGenerator
{
    public class FontResolver : IFontResolver
    {
        public string FontFolder { get; }

        public FontResolver(string fontFolder)
        {
            FontFolder = fontFolder;
        }

        public byte[] GetFont(string faceName)
        {
            using (var ms = new MemoryStream())
            {
                string fontPath = Path.Combine(FontFolder, faceName);
                using (FileStream fs = File.Open(fontPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            string fontName = string.Empty;

            if (familyName.EQ("OpenSans") || familyName.EQ("Segoe UI") || familyName.EQ("DejaVu Sans") || familyName.EQ("Arial"))
            {
                fontName = "DejaVuSans{0}{1}{2}.ttf".FormatInvariant(
                    isBold || isItalic ? "-" : "",
                    isBold ? "Bold" : "",
                    isItalic ? "Oblique" : "");
            }
            else if (familyName.EQ("Serif") || familyName.EQ("Times New Roman") || familyName.EQ("DejaVu Serif"))
            {
                fontName = "DejaVuSerif{0}{1}{2}.ttf".FormatInvariant(
                    isBold || isItalic ? "-" : "",
                    isBold ? "Bold" : "",
                    isItalic ? "Italic" : "");
            }
            else if (familyName.EQ("Courier New") || familyName.EQ("DejaVu Sans Mono"))
            {
                fontName = "DejaVuSansMono{0}{1}{2}.ttf".FormatInvariant(
                    isBold || isItalic ? "-" : "",
                    isBold ? "Bold" : "",
                    isItalic ? "Oblique" : "");
            }

            return string.IsNullOrEmpty(fontName) ?
                PlatformFontResolver.ResolveTypeface(familyName, isBold, isItalic) :
                new FontResolverInfo(fontName);
        }
    }
}

