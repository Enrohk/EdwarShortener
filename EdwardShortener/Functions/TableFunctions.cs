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
            DBConnector connector = new DBConnector();
            DateTime d = DateTime.Now;
            DateTime newDate = getDate(fromDate);
            var param = new { QueryTime = newDate };
            string query = Properties.Resources.SQL_Function_GetTableUrlList;
            userUrlList.urlLists = connector.getListItem<TableShortedUrl>(query, param);
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