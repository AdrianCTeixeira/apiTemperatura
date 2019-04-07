using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Response
    {
        public string by { get; set; }
        public Temperature results { get; set; }
    }
    public class Temperature
    {
        public string temp { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string city_name { get; set; }
    }
}