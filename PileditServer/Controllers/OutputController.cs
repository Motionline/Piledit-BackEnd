using Microsoft.AspNetCore.Mvc;
using PileditBackend;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PileditBackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutputController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var mat = new Mat(Path.Combine(MESystem.AppLocation, "ErJbVcrVQAcqsqc.jpg"));
            var bytes = mat.ToBytes();
            return File(bytes, "application/octet-stream");
        }
    }
}
