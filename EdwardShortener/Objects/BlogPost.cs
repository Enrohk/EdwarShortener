using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdwardShortener.Objects
{
    public class BlogPost
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Tags { get; set; }

        public BlogPost ()
        {
            Tags = new List<string>();
        }


    }
}