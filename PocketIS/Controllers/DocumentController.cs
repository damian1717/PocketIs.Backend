using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Common;
using PocketIS.Services.Interfaces;

namespace PocketIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : BaseController
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Route("GetDocumentsByCode")]
        public async Task<IActionResult> Get(string code) => Ok(await _documentService.GetAllDocumentsByCodeAsync(code));

        [HttpGet]
        [Route("GetDocument")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _documentService.GetDocumnetAsync(id));

        [HttpPost]
        [Route("DownloadPdf")]
        public async Task<IActionResult> DownloadPdf(Guid id)
        {
            var document = await _documentService.GetDocumnetAsync(id);

            if (document?.FileData is null) NotFound();

            return File(document.FileData, Constants.PdfContentMime);
        }
    }
}
