using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocketIS.Application.Common.Codes;
using PocketIS.Common;
using PocketIS.Domain;
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

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload()
        {
            var file = Request.Form.Files[0];

            var document = new Document
            {
                Id = Guid.NewGuid(),
                Code = DocumentsCodes.ORG_CHAR,
                Name = $"SCHEMAT_ORGANIZACJI - {Guid.NewGuid()}",
                FileData = await GetBytes(file),
                InsertedDate = DateTime.Now
            };

            await _documentService.SaveDocumentAsync(document);


            return Ok();
        }

        private async Task<byte[]> GetBytes(IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
