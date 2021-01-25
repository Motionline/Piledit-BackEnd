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
            if (ServerData.ProjectList.ContainsKey(req.Uuid)) return BadRequest();

            var p = Project.Create(req.Name, new(req.SizeX, req.SizeY));
            ServerData.ProjectList.Add(req.Uuid, p);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetProject(ProjectRequest req)
        {
            return ServerData.ProjectList.ContainsKey(req.Uuid) ? Ok() : NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteProject(ProjectRequest req)
        {
            if (!ServerData.ProjectList.ContainsKey(req.Uuid)) return NotFound();

            ServerData.ProjectList.Remove(req.Uuid);
            return Ok();
        }
    }
}
