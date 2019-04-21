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
            this.Date = Date;
            this.Temperature = Temperature;
        }
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
    }
}