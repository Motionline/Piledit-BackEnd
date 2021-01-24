using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PileditBackendServer.Models
{
    public class ProjectRequest
    {
        public string ProjectName { get; set; }
        public string ProjectUuid { get; set; }
        public double SizeX { get; set; }
        public double SizeY { get; set; }
    }
}
