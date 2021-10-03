using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lab04_EDII.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab04_EDII.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CifradosController : Controller
    {
        [HttpPost("cipher/{method}")]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file, [FromForm] string key, [FromRoute] string method)
        {
            try
            {
                Encryption.DirectoryCreation();
                var filePath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Temp\\" + file.FileName);
                if (file != null)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else { return StatusCode(500); }

                string[] fileName = file.FileName.Split('.');

                string finalName = Encryption.Encrypt(filePath, fileName[0], method, key);
                FileStream Sender = new FileStream(Directory.GetCurrentDirectory() + "\\Cifrados\\" + finalName, FileMode.OpenOrCreate);
                return File(Sender, "text/plain", finalName);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("decipher")]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file, [FromForm] string key)
        {
            try
            {
                Encryption.DirectoryCreation();
                var filePath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Temp\\" + file.FileName);
                if (file != null)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else { return StatusCode(500); }

                string[] fileName = file.FileName.Split('.');
                string method = fileName[1];
                switch (method)
                {
                    case "csr":
                            method = "cesar";
                            break;

                    case "zz":
                            method = "zigzag";
                            break;
                    default:
                        throw new Exception();
                }

                string finalName = Encryption.Decrypt(filePath, fileName[0], method, key);
                FileStream Sender = new FileStream(Directory.GetCurrentDirectory() + "\\Descifrados\\" + finalName, FileMode.OpenOrCreate);
                return File(Sender, "text/plain", finalName);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}

