using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using NZWalks.API.Models.DTO;
using System.Linq;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;
using System.Runtime.Intrinsics.X86;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        const int Max10Mb = (1024 *1024)*10;
        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                // convert DTO to Domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };
                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }
            
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            // Validate the extension and the size of the image
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (request.File.Length > Max10Mb)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }
    }
}
