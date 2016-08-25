using EdwardShortener.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdwardShortener.Functions
{
    public class ShortFunctions
    {
        #region sql
       
        public static int insertNewClick (int shortedUrlId)
        {
            try
            {
                using (var db = new edShortenerModel())
                {
                    var newClick = new Click();
                    newClick.Fk_idShortedUrl = shortedUrlId;
                    newClick.created = DateTime.Now;
                    db.Clicks.Add(newClick);                
                    return db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public static UrlObject getUrlObjectIdByShorted(string shortedUrl)
        {
            UrlObject urlItem = new UrlObject();

            using (var db = new edShortenerModel())
            {
                var queryResult = from url in db.ShortedUrls
                                  where url.shortedUrl1 == shortedUrl
                                  select url;

                var urlResult = queryResult.FirstOrDefault();

                urlItem.idShortedUrl = urlResult.idShortedUrl;
                urlItem.longUrl = urlResult.longUrl;
                urlItem.shortedUrl = urlResult.shortedUrl1;
                urlItem.created = urlResult.created;                
            }

            return urlItem;
        }

        public static bool urlAlreadyShorted (string longUrl)
        {            
            using (var db = new edShortenerModel())
            {
                var queryResult = from url in db.ShortedUrls
                                  where url.longUrl == longUrl
                                  select url;

                var urlResult = queryResult.FirstOrDefault();

                return urlResult != null;
                               
            }            
        }

        public static int addNewUrl (string longUrl)
        {
            try
            {
                string s = string.Empty;
                using (var db = new edShortenerModel())
                {
                    var newUrl = new ShortedUrl();
                    newUrl.created = DateTime.Now;
                    newUrl.longUrl = longUrl;
                    newUrl.shortedUrl1 = "asafadsfa";
                    db.ShortedUrls.Add(newUrl);
                    
                    return db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return 0;
            }
            
        }
    

        #endregion

        #region functions

        public static string shortUrl (string longUrl)
        {
            string shortUrl = string.Empty;

            return shortUrl;
        }

        #endregion

    }
}