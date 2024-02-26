using AzureBlobManagedIdentity.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HW3NoteKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private const string GetAttachmentByIdRoute = "GetAttachmentByIdRoute";
        private readonly StorageRepository _storageRepository;
        private readonly ILogger<AttachmentController> _logger;

        public AttachmentController(StorageRepository storageRepository, ILogger<AttachmentController> logger)
        {
            _storageRepository = storageRepository;
            _logger = logger;
        }

        [HttpGet("{noteId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllAttachments(Guid noteId)
        {
            var attachments = await _storageRepository.GetListOfBlobs(noteId.ToString());
            return Ok(attachments);
        }

        [HttpGet("{noteId}/attachments/{attachmentId}", Name = GetAttachmentByIdRoute)]
        public async Task<IActionResult> GetAttachmentById(Guid noteId, string attachmentId)
        {
            try
            {
                var (fileStream, contentType) = await _storageRepository.GetFileAsync(noteId.ToString(), attachmentId);
                return File(fileStream, contentType, attachmentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting attachment");
                return NotFound();
            }
        }

        [HttpPost("upload/{noteId}")]
        public async Task<IActionResult> UploadAttachment(Guid noteId, IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return BadRequest("File is not provided or is empty.");
            }

            using (var fileStream = formFile.OpenReadStream())
            {
                await _storageRepository.UploadFile(noteId.ToString(), formFile.FileName, fileStream, formFile.ContentType);
            }

            return CreatedAtRoute(GetAttachmentByIdRoute, new { noteId = noteId, attachmentId = formFile.FileName }, null);
        }

        [HttpDelete("{noteId}/attachments/{attachmentId}")]
        public async Task<IActionResult> DeleteAttachment(Guid noteId, string attachmentId)
        {
            await _storageRepository.DeleteFile(noteId.ToString(), attachmentId);
            return NoContent();
        }
    }
}
