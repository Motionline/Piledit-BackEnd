using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PileditBackendServer.Models
{
    public class ProjectRequest
    {
        public string Name { get; set; }
        public string Uuid { get; set; }
        public double SizeX { get; set; }
        public double SizeY { get; set; }
    }
}
