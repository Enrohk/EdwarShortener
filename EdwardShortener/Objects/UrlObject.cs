using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdwardShortener.Objects
{
    public class UrlObject
    {
        public int id { get; set; }
        public string longUrl { get; set; }
        public string shotUrl { get; set; }
        public DateTime created { get; set; }
        public List<Click> clicks { get; set; }     


        public UrlObject ()
        {
               
        }


    }
}