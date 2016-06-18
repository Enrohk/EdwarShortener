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
        public string shortUrl { get; set; }
        public DateTime created { get; set; }

        public UrlObject ()
        {
               
        }


    }
}