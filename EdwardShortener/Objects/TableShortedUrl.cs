using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdwardShortener.Objects
{
    public class TableShortedUrl
    {
        public int id { get; set; }
        public string longUrl { get; set; }
        public string shortedUrl { get; set; }
        public DateTime created { get; set; }
        public int clicks { get; set; }
        public int? pageStatus { get; set; }
        public DateTime? lastStatusChange { get; set; }
        public TableShortedUrl ()
        {

        }
    }
}