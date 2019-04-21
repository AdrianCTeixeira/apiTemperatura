using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Response
    {
        public string By { get; set; }
        public Temperature Results { get; set; }
    }
    public class Temperature
    {
        public string Temp { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string City_name { get; set; }
    }
}