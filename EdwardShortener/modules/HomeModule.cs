using EdwardShortener.Objects;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdwardShortener.modules
{
    public class HomeModule : NancyModule
    {

        public HomeModule ()
        {
            Get["/"] = parameters =>
            {
                var blogPost = new BlogPost
                {
                    id = 1,
                    Title = "Just Test",
                    Content = "Dummy",
                    Tags = {"Tag", "SuperTag"}
                };
                return View["Index", blogPost];
            };
        }

    }
}
