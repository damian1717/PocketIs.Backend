﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Get(string code) 
            => Ok(await _documentService.GetAllDocumentsByCodeAsync(code));

        [HttpGet]
        [Route("GetAllDocumentsByCodeAndUserId")]
        public async Task<IActionResult> GetAllDocumentsByCodeAndUserId(string code)
            => Ok(await _documentService.GetAllDocumentsByCodeAndUserIdAsync(code, UserId));

        [HttpGet]
        [Route("GetDocument")]
        public async Task<IActionResult> Get(Guid id) 
            => Ok(await _documentService.GetDocumnetAsync(id));

        [HttpPost]
        [Route("DownloadPdf")]
        public async Task<IActionResult> DownloadPdf(Guid id)
        {
            var document = await _documentService.GetDocumnetAsync(id);

            if (document?.FileData is null) NotFound();

            return File(document.FileData, Constants.PdfContentMime, document.Name);
        }

        [HttpPost]
        [Route("DownloadPdfByCodeLastAdded")]
        public async Task<IActionResult> DownloadPdfByCodeLastAdded(string code)
        {
            var documents = await _documentService.GetAllDocumentsByCodeAsync(code);

            if (documents is null || documents.Count <= 0) return NotFound("Raport nie został jeszcze wygenerowany.");

            var document = documents.OrderByDescending(x => x.Version).First();

            return File(document.FileData, Constants.PdfContentMime, document.Name);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload()
        {
            var file = Request.Form.Files[0];
            var document = new Document
            {
                Id = Guid.NewGuid(),
                Code = await GetRaportNameAsync(),
                Name = file.FileName,
                FileData = await GetBytes(file),
                InsertedDate = DateTime.Now,
                CompanyId = CompanyId
            };

                await _documentService.SaveDocumentAsync(document);
            return Ok();
        }

        [HttpPost]
        [Route("upload/{code}")]
        public async Task<IActionResult> Upload(string code)
        {
            if (Request.Form.Files is null || !Request.Form.Files.Any()) return Ok();

            foreach (var file in Request.Form.Files)
            {
                var document = new Document
                {
                    Id = Guid.NewGuid(),
                    Code = code,
                    Name = file.FileName,
                    FileData = await GetBytes(file),
                    InsertedDate = DateTime.Now,
                    CompanyId = CompanyId
                };

                await _documentService.SaveDocumentAsync(document);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _documentService.DeleteAsync(id);

            return Ok();
        }

        private async Task<byte[]> GetBytes(IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        private async Task<string> GetRaportNameAsync()
        {
            var allRaports = await _documentService.GetAllDocumentsByCodeAsync(RaportCodes.OrganizationChart);

            var numberForRaport = allRaports is not null ? allRaports.Count + 1 : 1;
            var currentDate = DateTime.Now.ToString("dd-MM-yyyy");
            var raportName = $"Schemat_organizacji_{numberForRaport}_{currentDate}";
            return raportName;
        }
    }
}
