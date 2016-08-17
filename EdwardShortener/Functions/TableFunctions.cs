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
            //DBConnector connector = new DBConnector();
            //DateTime d = DateTime.Now;
            //DateTime newDate = getDate(fromDate);
            //var param = new { QueryTime = newDate };
            //string query = Properties.Resources.SQL_Function_GetTableUrlList;
            //userUrlList.urlLists = connector.getListItem<TableShortedUrl>(query, param);

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