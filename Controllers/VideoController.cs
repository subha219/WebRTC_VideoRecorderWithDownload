using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebRTCVideoRecorder.Models;

namespace WebRTCVideoRecorder.Controllers
{
    public class VideoController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public VideoController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> SaveRecoredFile()
        {
            if (Request.Form.Files.Any())
            {
                var file = Request.Form.Files["video-blob"];
                string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");
                string UniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName + ".webm";
                string UploadPath = Path.Combine(UploadFolder, UniqueFileName);
                await file.CopyToAsync(new FileStream(UploadPath, FileMode.Create));
            }
            return Json(HttpStatusCode.OK);
        }

    }
}
