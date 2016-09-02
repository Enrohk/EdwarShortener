using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdwardShortener.Objects
{
    public class csvResponse
    {
        public List<csvResponseObj> urlList { get; set; }
    }

    public class csvResponseObj
    {
        public string ulr { get; set; }
        public string status { get; set; }
    }
}