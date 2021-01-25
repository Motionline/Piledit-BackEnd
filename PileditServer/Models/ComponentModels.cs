using PileditBackend.TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PileditBackendServer.Models
{
    public class ComponentBlockBase
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string TopUuid { get; set; }
        public string ParentUuid { get; set; }
    }

    public class MovieLoadingBlock : ComponentBlockBase
    {
        public string MaterialPath { get; set; }
    }

    public class GrayScaleFilterBlock : ComponentBlockBase
    {
        public string MaterialPath { get; set; }
    }

    /*
     * "name": "DefineComponentBlock",GrayScaleFilterBlock
          "kind": "DefineComponentBlock",
          "uuid": "759cc1eb-93fe-4458-9565-dce564a84e29",
          "topUuid": "",
          "parentUuid": "",
          "childUuid": "706fa8c3-de87-42cf-a696-8b4a26a88f92",
          "shadow": false,
          "position": {
            "x": 295,
            "y": 46
          },
          "tabUuid": "030e012a-49bb-42f4-a39a-a81c1985200b",
          "isSample": false,
          "shadowPath": "",
          "path": "m 0,0 c 25,-22 71,-22 96,0 H 300 a 4,4 0 0,1 4,4 v 40  a 4,4 0 0,1 -4,4 H 48   c -2,0 -3,1 -4,2 l -4,4 c -1,1 -2,2 -4,2 h -12 c -2,0 -3,-1 -4,-2 l -4,-4 c -1,-1 -2,-2 -4,-2 H 4 a 4,4 0 0,1 -4,-4 z",
          "strokeColor": "#bd9900",
          "fillColor": "#e3b100"
    */
}
