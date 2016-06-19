using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdwardShortener.Objects
{
    public class UrlObject
    {
        public int idShortedUrl { get; set; }
        public string longUrl { get; set; }
        public string shortedUrl { get; set; }
        public DateTime created { get; set; }

        public UrlObject ()
        {
               
        }


    }
}