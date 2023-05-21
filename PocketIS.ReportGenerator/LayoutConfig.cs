using PocketIS.PdfConverter;

namespace PocketIS.ReportGenerator
{
    public class LayoutConfig
    {
        public PdfGenerateConfig HeaderConfig { get; set; }

        public PdfGenerateConfig BodyConfig { get; set; }

        public PdfGenerateConfig FooterConfig { get; set; }
    }
}
