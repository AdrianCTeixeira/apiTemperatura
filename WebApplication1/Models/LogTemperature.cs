using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LogTemperature
    {
        public string City { get; set; }
        public List<TemperaturaResponse> Temperatures { get; set; }
    }

}