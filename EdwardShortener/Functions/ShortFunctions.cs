using EdwardShortener.Model;
using EdwardShortener.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Nancy.Authentication.Forms;
using Newtonsoft.Json.Linq;

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
                    var newClick = new Model.Click();
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
                urlItem.status = urlResult.pageStatus;
                urlItem.lastStatusCHanged = urlResult.lastStatusChange;                
            }

            return urlItem;
        }

        public static Model.ShortedUrl urlAlreadyShorted (string longUrl, Guid guid)
        {            
            using (var db = new edShortenerModel())
            {
                
                var queryResult = from url in db.ShortedUrls
                                  where url.longUrl == longUrl && url.Fk_idUsers == guid
                                  select url;

                var urlResult = queryResult.FirstOrDefault();

                return urlResult;
                               
            }            
        }

        public static csvResponse addArrUrls (string[] arr , Guid guid)
        {
            csvResponse obj = new csvResponse();
            obj.urlList = new List<csvResponseObj>();
            csvResponseObj response;
            arr.ToList().ForEach(url =>
            {
                int shorted;
                string txt;
                if (!string.IsNullOrEmpty(url))
                {
                    response = new csvResponseObj();
                    shorted = addNewUrl(url, guid);
                    if (shorted > 0)
                        txt = "Shorted";
                    else
                        txt = "Error";
                    response.status = txt;
                    response.ulr = url;
                    obj.urlList.Add(response);
                }
            });
            return obj;
        }


        public static int addNewUrl (string longUrl, Guid guid)
        {
            try
            {
                if(pingUrl(longUrl) == HttpStatusCode.OK.ToString())
                {                        
                    string s = string.Empty;
                    using (var db = new edShortenerModel())
                    {
                        
                        var newUrl = new ShortedUrl();
                        newUrl.created = DateTime.Now;
                        newUrl.longUrl = longUrl;
                        newUrl.shortedUrl1 = cryptDecrypt.Base64Encode(longUrl);
                        newUrl.pageStatus = 200;
                        newUrl.lastStatusChange = DateTime.Now;                        
                        newUrl.Fk_idUsers = guid;
                        db.ShortedUrls.Add(newUrl);
                    
                        return db.SaveChanges();
                    }
                }
                else
                {
                    return 0;
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

        public static string pingUrl (string url)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.AllowAutoRedirect = false;
                request.Method = "HEAD";
                var response = request.GetResponse();
                return ((HttpWebResponse)response).StatusCode.ToString();
                
            }
            catch (Exception wex)
            {
                return wex.Message.ToString();
            }
        }

        #endregion
       
    }
}