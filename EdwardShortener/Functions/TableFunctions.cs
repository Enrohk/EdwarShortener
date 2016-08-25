using EdwardShortener.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdwardShortener.Functions
{
    public class TableFunctions
    {        

        public static UserUrlList getUserList(string fromDate )
        {
            UserUrlList userUrlList = new UserUrlList();
            userUrlList.urlLists = new List<TableShortedUrl>();

            using (var db = new edShortenerModel())
            {
                var queryResult = from url in db.ShortedUrls
                                    select url;

                queryResult.ToList().ForEach(shortedURl =>
                {
                    userUrlList.urlLists.Add(new TableShortedUrl
                    {
                        id = shortedURl.idShortedUrl,
                        created = shortedURl.created,
                        longUrl = shortedURl.longUrl,
                        shortedUrl = shortedURl.shortedUrl1,
                        clicks = shortedURl.Clicks.Where(click => click.created >= getDate(fromDate)).Count()
                    }
                    );
                });
            }

            return userUrlList;
        }

        public static UrlObject getUrlTableDetails (int id)
        {
            UrlObject urlTable = new UrlObject();

            using (var db = new edShortenerModel())
            {
                var queryResult = from url in db.ShortedUrls
                                  where url.idShortedUrl == id
                                  select url;

                var urlResult = queryResult.FirstOrDefault();

                urlTable.idShortedUrl = urlResult.idShortedUrl;
                urlTable.longUrl = urlResult.longUrl;
                urlTable.shortedUrl = urlResult.shortedUrl1;
                urlTable.created = urlResult.created;
            }

            return urlTable;
        }

        private static DateTime getDate (string fromDate)
        {
            DateTime now = DateTime.Now;
            switch (fromDate)
            {
                case "hours":
                    return now.AddHours(-2);

                case "days":
                    return now.AddDays(-1);

                case "weeks":
                    return now.AddDays(-7);

                case "months":
                    return now.AddMonths(-1);

                case "all":
                    return now.AddYears(-100);

                default:
                    return now;

            }
        }

    }
}