using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TemperaturaResponse
    {
        public TemperaturaResponse(DateTime Date, int Temperature)
        {
            date = Date;
            temperature = Temperature;
        }
        public DateTime date { get; set; }
        public int temperature { get; set; }
    }
}