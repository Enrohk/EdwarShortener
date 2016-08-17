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
            Get["test"] = parameters =>
            {
                string data = string.Empty;
                using (var db = new edShortenerModel())
                {
                    var query = from s in db.ShortedUrls
                                select s;
                    foreach (var s in query)
                    {
                        data += " " + s.longUrl;
                    }
                }

                return data;
            };
            
            Get["/"] = parameters =>
            {
                User u = new User();
                u.userUrlList = TableFunctions.getUserList("all");
                return View["Index",u];
            };

            #region table index

            Get["/table/{date}"] = parameters =>
            {
                UserUrlList list = TableFunctions.getUserList(parameters.date);
                return View["_TableUrl", list];
            };


            Get["/tableDetails/{urlObjectId}"] = parameters =>
            {

                UrlObject urlObj = TableFunctions.getUrlTableDetails(id);

                if (urlObj != null)
                {
                    return View["_ShortedUrlInfo", urlObj];
                }
                else
                {
                    return "error";
                }
            };

            #endregion

            #region user

            Get["/login"] = parameters =>
            {
                return View["login"];
            };

            Get["/register"] = parameters =>
            {
                return View["register"];
            };

            #endregion

            #region short
            Get["/{toShort}"] = parameters =>
            {
                string toShort = parameters.toShort;
                ShortFunctions shortFunction = new ShortFunctions();
                UrlObject urlObject = shortFunction.getUrlObjectIdByShorted(toShort);

                if (urlObject != null)
                {
                    shortFunction.insertNewClick(urlObject.idShortedUrl);

                    return Response.AsRedirect(urlObject.longUrl);
                }
                else
                {

                    return "not shorted ";
                }

            };
            #endregion

            Get["/csv"] = parameters =>
            {               
                return View["csv"];
            };

            

            Get["/details/{url}"] = parameters =>
            {
                return View["register"];
            };

        }

    }
}
