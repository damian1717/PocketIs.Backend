using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Application.Common.Codes;
using PocketIS.Common;
using PocketIS.Domain;
using PocketIS.Framwork;
using PocketIS.Infrastucture.Validation;
using PocketIS.Models.Report;
using PocketIS.Models.Report.ChartOrg;
using PocketIS.ReportGenerator;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RaportController : BaseController
    {
        private readonly IDocumentService _documentService;
        private readonly IRenderRazorToStringService _renderService;
        private readonly IUserService _userService;
        private readonly IOrganizationChartService _organizationChartService;
        public RaportController(IDocumentService documentService, IRenderRazorToStringService renderService, IUserService userService, IOrganizationChartService organizationChartService)
        {
            Check.NotNull(_documentService = documentService, nameof(documentService));
            Check.NotNull(_renderService = renderService, nameof(renderService));
            Check.NotNull(_userService = userService, nameof(userService));
            Check.NotNull(_organizationChartService = organizationChartService, nameof(organizationChartService));
        }

        [HttpPost]
        [Route("GenerateQualityPolicyRaportPdf")]
        public async Task<IActionResult> GenerateQualityPolicyRaportPdf(QualityPolicyReportModel model)
        {
            var user = await _userService.GetAsync(UserId);

            var allRaports = await _documentService.GetAllDocumentsByCodeAsync(RaportCodes.QualityPolicy, CompanyId);

            var numberForRaport = allRaports is not null ? allRaports.Count + 1 : 1;
            model.ReportName = "Polityka Jakości";
            string raportName = GetRaportNameForQualityPolicy(numberForRaport);
            return await GeneratePdfReportAsync(ReportViews.GetDefault(), new ReportModel(model, user), null, raportName, RaportCodes.QualityPolicy, numberForRaport);
        }

        [HttpPost]
        [Route("GenerateOrgChartRaportPdf")]
        public async Task<IActionResult> GenerateOrgChartRaportPdf(ChartOrgModel model)
        {
            var user = await _userService.GetAsync(UserId);

            var allRaports = await _documentService.GetAllDocumentsByCodeAsync(RaportCodes.OrganizationChart, CompanyId);

            var numberForRaport = allRaports is not null ? allRaports.Count + 1 : 1;
            model.Image64String = _organizationChartService.GenerateOrganizationChartImage64String(model.ChartNodes);
            model.ReportName = "Schemat Organizacji";
            string raportName = GetRaportNameForOrgChart(numberForRaport);
            return await GeneratePdfReportAsync(ReportViews.GetDefault(), new ChartOrgReportModel(model, user), null, raportName, RaportCodes.OrganizationChart, numberForRaport);
        }

        /// <summary>   
        /// Generates a report in PDF format
        /// </summary>
        /// <param name="reportModel">an instance of Report Model</param>
        /// <param name="views">names of the report's section views to render</param>
        /// <param name="layout">page parameters</param>
        /// <returns>pdf-file in the form of a byte array</returns>
        private async Task<FileContentResult> GeneratePdfReportAsync<T>(IViewConfig views, IReportModel<T> reportModel, LayoutConfig layout, string raportName, string raportCode, int version)
        {
            var content = await HtmlToPdfConverter.RenderHtmlPagesToPdfAsync(views, reportModel, _renderService, layout)
                .ConfigureAwait(false);

            var document = new Document
            {
                Id = Guid.NewGuid(),
                Code = raportCode,
                Name = raportName,
                FileData = content,
                InsertedDate = DateTime.Now,
                CompanyId = CompanyId,
                Version = version
            };
            
            await _documentService.SaveDocumentAsync(document);

            return File(content, Constants.PdfContentMime, raportName);
        }

        private string GetRaportNameForQualityPolicy(int numberForRaport)
        {
            var currentDate = DateTime.Now.ToString("dd-MM-yyyy");
            var raportName = $"Polityka_jakości_wersja_{numberForRaport}_{currentDate}";
            return raportName;
        }

        private string GetRaportNameForOrgChart(int numberForRaport)
        {
            var currentDate = DateTime.Now.ToString("dd-MM-yyyy");
            var raportName = $"Schemat_Organizacji_wersja_{numberForRaport}_{currentDate}";
            return raportName;
        }
    }
}
