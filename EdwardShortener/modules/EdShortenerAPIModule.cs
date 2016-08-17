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

            Get["/csv"] = parameters =>
            {               
                return View["csv"];
            };

            Get["/login"] = parameters =>
            {
                return View["login"];
            };

            Get["/register"] = parameters =>
            {
                return View["register"];
            };

            Get["/details/{url}"] = parameters =>
            {
                return View["register"];
            };

            Get["/table/{date}"] = parameters =>
            {
                UserUrlList list = TableFunctions.getUserList(parameters.date);
                return View["_TableUrl", list];
            };

            Get["/tableDetails/{urlObjectId}"] = parameters =>
            {
                DBConnector connector = new DBConnector();
                string query = Properties.Resources.SQL_Function_GerUrlObjectById;
                int id = parameters.urlObjectId;
                var param = new { QueryId = id };
                List<UrlObject> listObject = connector.getListItem<UrlObject>(query, param);
                if(listObject.Count == 1)
                {
                    return View["_ShortedUrlInfo", listObject.FirstOrDefault()];
                }
                else
                {
                    return "error";
                }
            };

            Get["/{toShort}"] = parameters =>
            {
                string toShort = parameters.toShort;
                ShortFunctions shortFunction = new ShortFunctions();
                UrlObject urlObject = shortFunction.getUrlObjectIdByShorted(toShort);

                if(urlObject!= null)
                {                   
                    shortFunction.insertNewClick(urlObject.idShortedUrl);

                    return Response.AsRedirect(urlObject.longUrl);
                }
                else
                {

                    return "not shorted ";
                }

            };

        }

    }
}
