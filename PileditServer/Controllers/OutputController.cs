using Microsoft.AspNetCore.Mvc;
using PileditBackend;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PileditBackendServer.Models;
using PileditBackend.IO;
using PileditServer.Models;

namespace PileditBackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutputController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var mat = new Mat(Path.Combine(PileditSystem.AppLocation, "ErJbVcrVQAcqsqc.jpg"));
            var bytes = mat.ToBytes();
            return File(bytes, "application/octet-stream");
        }

        [HttpPost]
        public IActionResult OutputMovie(OutputRequest req)
        {
            if (!ServerData.ProjectList.ContainsKey(req.Uuid)) return NotFound();

            Movie.OutputMovie(Path.Combine(PileditSystem.AppLocation, "output"), req.Extention,
                FourCC.FromString(req.FourCC) , ServerData.ProjectList[req.Uuid]);
            return Ok();
        }
    }
}
