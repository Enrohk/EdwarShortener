using EdwardShortener.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdwardShortener.Functions
{
    public class ShortFunctions
    {

        public UrlObject getUrlObjectIdByShorted (string shortedUrl)
        {
            UrlObject urlObject = null;
            DBConnector dbConnector = new DBConnector();
            var param = new
            {
                QueryShorted = shortedUrl
            };
            string query = Properties.Resources.SQL_Function_GerUrlObjectByShorted;
            List<UrlObject> listResult = dbConnector.getListItem<UrlObject>(query, param);
            if(listResult.Count == 1)
            {
                urlObject = listResult.First();
            }
            
            return urlObject;
        }

        public int insertNewClick (int shortedUrlId)
        {
            string query = Properties.Resources.SQL_INSERT_CLICK;
            var param = new { Fk_idShortedUrl = shortedUrlId };
            DBConnector connector = new DBConnector();
            return connector.addItemToDb(query, param);
        }
           

    }
}