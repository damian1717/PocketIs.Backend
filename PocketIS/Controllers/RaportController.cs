using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IIdentityService _identityService;
        public RaportController(IDocumentService documentService, IRenderRazorToStringService renderService, IIdentityService identityService)
        {
            Check.NotNull(_documentService = documentService, nameof(documentService));
            Check.NotNull(_renderService = renderService, nameof(renderService));
            Check.NotNull(_identityService = identityService, nameof(identityService));
        }

        [HttpPost]
        [Route("GenerateRaportPdf")]
        public async Task<IActionResult> GenerateRaportPdf(QualityPolicyReportModel model)
        {
            var user = await _identityService.GetUserByIdAsync(UserId);

            return await GeneratePdfReportAsync(ReportViews.GetDefault(), new ReportModel(model, user), null);
        }

        /// <summary>   
        /// Generates a report in PDF format
        /// </summary>
        /// <param name="reportModel">an instance of Report Model</param>
        /// <param name="views">names of the report's section views to render</param>
        /// <param name="layout">page parameters</param>
        /// <returns>pdf-file in the form of a byte array</returns>
        private async Task<FileContentResult> GeneratePdfReportAsync<T>(IViewConfig views, IReportModel<T> reportModel, LayoutConfig layout)
        {
            var content = await HtmlToPdfConverter.RenderHtmlPagesToPdfAsync(views, reportModel, _renderService, layout)
                .ConfigureAwait(false);

            var document = new Document
            {
                Id = Guid.NewGuid(),
                Code = "TEST",
                Name = $"Raport - {Guid.NewGuid()}",
                FileData = content,
                InsertedDate = DateTime.Now
            };
            
            await _documentService.SaveDocumentAsync(document);

            return File(content, Constants.PdfContentMime);
        }
    }
}
