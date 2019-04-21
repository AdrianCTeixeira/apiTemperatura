using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CityTemp
    {
        public string City { get; set; }
        public TemperaturaResponse Temperatures { get; set; }
    }
}