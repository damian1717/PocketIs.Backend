using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Application.Common.Codes;
using PocketIS.Common;
using PocketIS.Domain;
using PocketIS.Framwork;
using PocketIS.Infrastucture.Validation;
using PocketIS.Models.Report;
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
        public RaportController(IDocumentService documentService, IRenderRazorToStringService renderService, IUserService userService)
        {
            Check.NotNull(_documentService = documentService, nameof(documentService));
            Check.NotNull(_renderService = renderService, nameof(renderService));
            Check.NotNull(_userService = userService, nameof(userService));
        }

        [HttpPost]
        [Route("GenerateQualityPolicyRaportPdf")]
        public async Task<IActionResult> GenerateQualityPolicyRaportPdf(QualityPolicyReportModel model)
        {
            var user = await _userService.GetAsync(UserId);

            var newUser = new User(user.Id, user.Email, user.Role, user.FirstName, user.LastName, user.CompanyId);
            string raportName = await GetRaportNameForQualityPolicy();
            return await GeneratePdfReportAsync(ReportViews.GetDefault(), new ReportModel(model, newUser), null, raportName, RaportCodes.QualityPolicy);
        }

        /// <summary>   
        /// Generates a report in PDF format
        /// </summary>
        /// <param name="reportModel">an instance of Report Model</param>
        /// <param name="views">names of the report's section views to render</param>
        /// <param name="layout">page parameters</param>
        /// <returns>pdf-file in the form of a byte array</returns>
        private async Task<FileContentResult> GeneratePdfReportAsync<T>(IViewConfig views, IReportModel<T> reportModel, LayoutConfig layout, string raportName, string raportCode)
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
                CompanyId = CompanyId
            };
            
            await _documentService.SaveDocumentAsync(document);

            return File(content, Constants.PdfContentMime);
        }

        private async Task<string> GetRaportNameForQualityPolicy()
        {
            var allRaports = await _documentService.GetAllDocumentsByCodeAsync(RaportCodes.QualityPolicy, CompanyId);

            var numberForRaport = allRaports is not null ? allRaports.Count + 1 : 1;
            var currentDate = DateTime.Now.ToString("dd-MM-yyyy");
            var raportName = $"Polityka_jakości_wersja_{numberForRaport}_{currentDate}";
            return raportName;
        }
    }
}
