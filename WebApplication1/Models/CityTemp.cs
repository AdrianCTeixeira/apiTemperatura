using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CityTemp
    {
        public string city { get; set; }
        public TemperaturaResponse temperatures { get; set; }
    }
}