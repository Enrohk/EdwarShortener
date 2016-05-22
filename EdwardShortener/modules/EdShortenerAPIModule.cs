using EdwardShortener.Objects;
using Nancy;
using Nancy.ModelBinding;
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

                return View["Index"];
            };

            Post["/addUrl"] = parameters =>
            {
                var urlToAdd = this.Bind<UrlObject>();
                return View["Index"];
            };
        }

    }
}
