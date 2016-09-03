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
using System.IO;
using EdwardShortener.Model;
using Nancy.Security;
using Nancy.Extensions;
using System.Dynamic;
using Nancy.Authentication.Forms;
using Newtonsoft.Json.Linq;

namespace EdwardShortener.modules
{
    public class HomeModule : NancyModule
    {

        public HomeModule()
        {
            if (Context != null )
            {
                this.RequiresAuthentication();  
            }

           
            Get["test2"] = parameters =>
            {
                try
                {
                    this.RequiresClaims(new[] { "Admin" });
                    
                    string s = "a";
                

                    return s;

                }
                catch (Exception e)
                {
                    return e.Message;
                }
            };

            Get["/"] = parameters =>
            {
                //var currentUser = "a";
                //try
                //{
                // currentUser = Context.CurrentUser.UserName;

                //}
                //catch(Exception e)
                //{

                //}
                var currentUser = Context.CurrentUser != null ? Context.CurrentUser.UserName : string.Empty;
                var guid = UserFunctions.getGuidByName(currentUser);
                if (guid != null)
                {
                    Objects.User u = new Objects.User();
                    u.userUrlList = TableFunctions.getUserList("all", guid);                    
                    return View["Index", u];
                }
                else return "error";

            };

            #region table index

            Get["/table/{date}"] = parameters =>
            {
                var currentUser = Context.CurrentUser.UserName;
                var guid = UserFunctions.getGuidByName(currentUser);
                if (guid != null)
                {
                    UserUrlList list = TableFunctions.getUserList(parameters.date, guid);
                    return View["_TableUrl", list];
                }
                else return "error";
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

            #region logIN
            Get["/login"] = parameter =>
            {
                dynamic model = new ExpandoObject();
                model.Errored = Request.Query.error.HasValue;
                return View["logIn",model];
            };

            Post["/login"] = parameter =>
            {
                
                string name = Request.Form["userName"];
                string pass = Request.Form["userPass"];
                string remember = Request.Form["rememberMe"];

                Objects.User user = UserFunctions.logInUser(name, pass);

                if(user != null)
                {
                    DateTime? expiry = null;
                    if (!string.IsNullOrEmpty(remember))
                    {
                        expiry = DateTime.Now.AddDays(7);
                    }
                    
                    return this.LoginAndRedirect(user.id, expiry);
                }
                else
                {
                    return Context.GetRedirect("~/login?error=true&username=" + (string)Request.Form.Username);
                }
            };

            Get["/logout"] = parameter =>
            {
                return this.LoginAndRedirect(UserFunctions.getDummyCurrentUser().guid);                
            };

            #endregion

            #region register

            Get["/register"] = parameter =>
            {
                dynamic model = new ExpandoObject();
                model.Errored = Request.Query.error.HasValue;
                return View["register", model];
            };

            Post["/register"] = parameter =>
            {
                string name = Request.Form["userName"];
                string pass = Request.Form["userPass"];
                string pass2 = Request.Form["userPass"];
                string remember = Request.Form["rememberMe"];

                Objects.User user = UserFunctions.registerUser(name, pass,pass2);

                if (user != null)
                {
                    DateTime? expiry = null;
                    if (!string.IsNullOrEmpty(remember))
                    {
                        expiry = DateTime.Now.AddDays(7);
                    }

                    return this.LoginAndRedirect(user.id, expiry);
                }
                else
                {
                    return Context.GetRedirect("~/register?error=true&username=" + (string)Request.Form.Username);
                }

            };

            #endregion

            #region profile

            Get["profile"] = parameters =>
            {
                var currentUser = Context.CurrentUser.UserName;
                var guid = UserFunctions.getGuidByName(currentUser);
                if (guid != null)
                {
                    Objects.User u = UserFunctions.getFullUserByGuid(guid);
                    u.userUrlList = TableFunctions.getUserList("all", guid);
                    return View["profile", u];
                }
                else return "error";
            };

            Post["/profile"] = parameter =>
            {
                Objects.User user = this.Bind();
                var changePass = Request.Form["changePass"];
                var currentUser = Context.CurrentUser.UserName;
                var guid = UserFunctions.getGuidByName(currentUser);
                user.id = guid;
                user.name = currentUser;

                if (changePass == "1")
                {
                    if(!string.IsNullOrEmpty(user.newPass1) && user.newPass1 == user.newPass2)
                    {                       
                        if(UserFunctions.verifyOldPass(user))
                        {
                            if(UserFunctions.updateProfile(user, true)>0)
                            {
                                return View["profile", user];
                            }
                            else
                            {
                                user.error = "Error Ocurred while updating profile";
                                return View["profile", user];
                            }
                        }
                        else
                        {
                            user.error = "Old password was incorrect";
                            return View["profile", user];
                        }

                        
                    }
                    else
                    {
                        user.error = "Password does not match";
                        return View["profile", user];
                    }
                }
                else
                {
                    if (UserFunctions.updateProfile(user, false) > 0)
                    {
                        return View["profile", user];
                    }
                    else
                    {
                        user.error = "Error Ocurred while updating profile";
                        return View["profile", user];
                    }
                }

            };

            #endregion
            #endregion

            #region short
            Get["/goTo/{toShort}"] = parameters =>
            {
                string toShort = parameters.toShort;                
                UrlObject urlObject = ShortFunctions.getUrlObjectIdByShorted(toShort);

                if (urlObject != null && urlObject.status.ToString() == "200")
                {

                    if (ShortFunctions.insertNewClick(urlObject.idShortedUrl) > 0 )
                    {
                        return Response.AsRedirect(urlObject.longUrl);
                    }
                    return View["error", new errorObj { errorMsg = "No se ha podido acceder a la url " + urlObject.shortedUrl + "error desconocdio" }];

                }
                else
                {              
                    return View["error", new errorObj { errorMsg = "Url Caida desde " + urlObject.lastStatusCHanged }];
                }

            };


            Post["/addUrl"] = parameters =>
            {                
                string urlToAdd = Request.Form["urlToShort"];
                var currentUser = Context.CurrentUser.UserName;
                if (!string.IsNullOrEmpty(currentUser))
                {
                    var guid = UserFunctions.getGuidByName(currentUser);
                    ShortedUrl newUrl = ShortFunctions.urlAlreadyShorted(urlToAdd, guid);
                    if (newUrl == null)
                    {
                        int result = ShortFunctions.addNewUrl(urlToAdd, guid);
                        if(result != 0)
                        {
                            return Response.AsRedirect("/");
                        }
                        else
                        {
                            return View["error", new errorObj { errorMsg = "No se ha podido crear la url " + urlToAdd}];
                        }

                    }

                    return Response.AsRedirect("/tableDetails/" + newUrl.idShortedUrl);
                }
                return "error";
               
            };
            #endregion

            #region csv

            Get["/csv"] = parameters =>
            {                
                return View["csv", null];
            };

            Post["/csv"] = parameters =>
            {
                string urlsToAdd = Request.Form["urlList"];
                string[] urlsArr = urlsToAdd.Split(',');
                var currentUser = Context.CurrentUser.UserName;
                var guid = UserFunctions.getGuidByName(currentUser);
                if (guid != null)
                {
                    csvResponse result = ShortFunctions.addArrUrls(urlsArr,guid); 
                    return View["csv", result];
                }

                return 0;
            };

            #endregion


            Get["/details/{url}"] = parameters =>
            {
                return View["register"];
            };

        }
    }
}
