using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PileditBackend.Effects;
using PileditBackend.TL;
using PileditBackendServer.Models;
using System.Collections.Generic;
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
            foreach (var comp in json["components"].Children<JProperty>())
            {
                var c = comp.Value.ToObject<JObject>();
                var cid = comp.Name;
                var def = false;
                TimelinePrintObject tpo = null;
                List<PrintEffectBase> list = new();
                foreach (var block in c["blocks"].Children<JProperty>())
                {
                    var b = block.Value.ToObject<ComponentBlockBase>();
                    if (b.Kind == "DefineComponentBlock")
                    {
                        if (def) return BadRequest();
                        def = true;
                        continue;
                    }
                    if (!def) return BadRequest();

                    if (b.Kind == "MovieLoadingBlock")
                    {
                        var mlb = block.Value.ToObject<MovieLoadingBlock>();
                        tpo = new TimelineMovie(new(0, 0), mlb.MaterialPath, project.OutputSize);
                    }
                    else if (b.Kind == "GrayScaleFilterBlock")
                    {
                        list.Add(PrintEffect.COLORCONVERT(OpenCvSharp.ColorConversionCodes.GRAY2BGR));
                    }
                }
                var tc = new TimelineComponent(new(0, 0), project.OutputSize, list.ToArray(), tpo);
                project.Timeline.RegistComponent(comp.Name, tc);
            }

            foreach (var clip in json["clips"].Children<JProperty>())
            {
                var c = clip.Value;
                project.Timeline.AddObject(c["layer"].Value<ushort>(),
                    new(c["frame"]["begin"].Value<uint>(), c["frame"]["end"].Value<uint>()),
                    project.Timeline.ComponentList[c["componentUuid"].Value<string>()]);
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
