using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PileditBackend;
using PileditBackend.Effects;
using PileditBackend.TL;
using PileditBackendServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace PileditBackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimelineController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostComponent(JsonElement obj)
        {
            if (!ServerData.IsAccessable(HttpContext.Connection.RemoteIpAddress))
                return Forbid();

            var json = JObject.Parse(obj.GetRawText());
            var id = json["uuid"].Value<string>();
            if (!ServerData.ProjectList.ContainsKey(id)) return NotFound();

            var project = ServerData.ProjectList[id];
            var components = (JObject)json["components"];
            var clips = (JObject)json["clips"];
            foreach (var comp in components.Children<JProperty>())
            {
                var c = comp.Value.ToObject<JObject>();
                var cid = comp.Name;
                var def = false;
                TimelinePrintObject tpo = null;
                List<PrintEffectBase> list = new();
                foreach (var block in c["blocks"].Children<JProperty>())
                {
                    var b = block.Value.ToObject<EditBlockBase>();
                    if (b.Kind == "DefineComponentBlock")
                    {
                        if (def) return BadRequest();
                        else continue;
                    }
                    if (!def) return BadRequest();

                    if (b is MovieLoadingBlock mlb)
                    {
                        tpo = new TimelineMovie(new(0, 0), mlb.MaterialPath, project.OutputSize);
                    }
                    else if (b is GrayScaleFilterBlock)
                    {
                        list.Add(PrintEffect.COLORCONVERT(OpenCvSharp.ColorConversionCodes.GRAY2BGR));
                    }
                }
                var tc = new TimelineComponent(new(0, 0), project.OutputSize, list.ToArray(), tpo);
                var frame = new FrameInfo(clips[cid]["startFrame"].ToObject<uint>(), clips[cid]["endFrame"].ToObject<uint>());
                project.Timeline.AddObject(clips[cid]["Layer"].ToObject<ushort>(), frame, tc);
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateComponent()
        {
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteComponent()
        {
            return NoContent();
        }
    }
}
