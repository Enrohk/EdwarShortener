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
                UserUrlList list = new UserUrlList();
                list.urlLists = new List<UrlObject>();
                UrlObject o;
                for (int i = 0; i<10; i++)
                {
                    o = new UrlObject();
                    o.id = i;
                    o.shotUrl = "short " + i;
                    o.longUrl = "long" + i;
                    list.urlLists.Add(o);
                }
                u.userUrlList = list;
                return View["Index",u];
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
