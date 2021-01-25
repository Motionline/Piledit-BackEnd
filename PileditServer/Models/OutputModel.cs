using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PileditServer.Models
{
    public class OutputRequest
    {
        public string Uuid { get; set; }
        public string Extention { get; set; }
        public string FourCC { get; set; }
    }
}
