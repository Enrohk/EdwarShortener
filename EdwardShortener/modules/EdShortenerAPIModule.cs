using EdwardShortener.Functions;
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

        public HomeModule()
        {
            Get["/"] = parameters =>
            {
                User u = new User();
                u.userUrlList = TableFunctions.getUserList("all");
                return View["Index",u];
            };

            Get["/table/{date}"] = parameters =>
            {
                UserUrlList list = TableFunctions.getUserList(parameters.date);
                return View["_TableUrl", list];
            };
           
            Get["/test"] = parameters =>
            {


                UrlObject u = new UrlObject();

                u.shotUrl = "lelele";
                u.longUrl = "leleleeeeee";
                u.id = 1;
                return View["_ShortedUrlInfo", u];

            };


        }

    }
}
