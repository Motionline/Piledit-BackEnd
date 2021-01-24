using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PileditBackendServer.Models;
using PileditBackend;

namespace PileditBackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateProject(ProjectRequest req)
        {
            if (ServerData.ProjectList.ContainsKey(req.ProjectUuid)) return NotFound();

            var p = Project.Create(req.ProjectName, new(req.SizeX, req.SizeY));
            ServerData.ProjectList.Add(req.ProjectUuid, p);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetProject(ProjectRequest req)
        {
            return ServerData.ProjectList.ContainsKey(req.ProjectUuid) ? Ok() : NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteProject(ProjectRequest req)
        {
            if (!ServerData.ProjectList.ContainsKey(req.ProjectUuid)) return NotFound();

            ServerData.ProjectList.Remove(req.ProjectUuid);
            return Ok();
        }
    }
}
