using EdwardShortener.Functions;
using EdwardShortener.Objects;
using Nancy;
using Nancy.Testing;
using Nancy.ModelBinding;
using Newtonsoft.Json;
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
                //HttpBrowserCapabilities browser = Request.Browser;
                string s = Request.UserHostAddress;
                
                return s;
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
                int urlObjId = parameters.urlObjectId;
                UrlObject urlObj = TableFunctions.getUrlTableDetails(urlObjId);

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
                UrlObject urlObject = ShortFunctions.getUrlObjectIdByShorted(toShort);

                if (urlObject != null && !string.IsNullOrEmpty(urlObject.shortedUrl))
                {

                    if (ShortFunctions.insertNewClick(urlObject.idShortedUrl) > 0 )
                    {
                        return Response.AsRedirect(urlObject.longUrl);
                    }
                    return "error";

                }
                else
                {

                    return "not shorted ";
                }

            };


            Post["/addUrl"] = parameters =>
            {
                string urlToAdd = Request.Form["urlToShort"];
                if(!ShortFunctions.urlAlreadyShorted(urlToAdd))
                {
                    int result = ShortFunctions.addNewUrl(urlToAdd);

                }
                return 0;
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
