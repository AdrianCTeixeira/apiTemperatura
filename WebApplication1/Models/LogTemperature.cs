using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LogTemperature
    {
        public string city { get; set; }
        public List<TemperaturaResponse> temperatures { get; set; }
    }
    
}