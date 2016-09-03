using EdwardShortener.Model;
using EdwardShortener.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdwardShortener.Functions
{
    public class UserFunctions
    {
        public static Objects.User logInUser (string userName, string userPass)
        {
            

            using (var db = new edShortenerModel())
            {
                var cryptedPass = cryptDecrypt.cryptPass(userPass);
                var queryResult = from User in db.Users
                                  where User.userName == userName && User.userPass == cryptedPass
                                  select User;

               if(queryResult.Count() == 1)
               {
                    Objects.User user = new Objects.User();
                    var userResult = queryResult.FirstOrDefault();
                    user.id = userResult.idUser;
                    user.name = userResult.userName;
                    user.userUrlList = new UserUrlList();
                    user.userUrlList.urlLists = new List<TableShortedUrl>();
                    userResult.ShortedUrls.ToList().ForEach(url => 
                    {
                        user.userUrlList.urlLists.Add(new TableShortedUrl
                        {
                            id = url.idShortedUrl,
                            created = url.created,
                            longUrl = url.longUrl,
                            shortedUrl = url.shortedUrl1,
                            pageStatus = url.pageStatus,
                            lastStatusChange = url.lastStatusChange,
                            clicks = url.Clicks.Where(click => click.created >= DateTime.Now.AddYears(-100)).Count()
                        });
                    });
                    return user;
               }
            }
            return null;

        }

        public static Objects.User registerUser(string userName, string userPass, string userPass2)
        {

            if (userPass == userPass2)
            {
                using (var db = new edShortenerModel())
                {
                    Model.User user = new Model.User();
                    user.userName = userName;
                    user.userPass = cryptDecrypt.cryptPass(userPass);                        
                    user.idUser = Guid.NewGuid();
                    db.Users.Add(user);
                    db.SaveChanges();
                    return new Objects.User
                    {
                        id = user.idUser,
                        name = user.userName,
                        userUrlList = new UserUrlList
                        {
                            urlLists = new List<TableShortedUrl>()
                        }
                    };
                }
            }
            return null;
        }

        public static userIdentity getUserIdentityByGuid (Guid guid)
        {
            using (var db = new edShortenerModel())
            {
                var queryResult = from User in db.Users
                                  where User.idUser == guid
                                  select User;

                userIdentity ui = new userIdentity();
                if (queryResult.Count() == 1)
                {
                    var userResult = queryResult.FirstOrDefault();
                    ui.guid = userResult.idUser;
                    ui.UserName = userResult.userName;
                    return ui;
                }
                else
                {
                    return getDummyCurrentUser();
                }               
            }
        }

        public static userIdentity getDummyCurrentUser()
        {
            using (var db = new edShortenerModel())
            {
                var queryResult = from User in db.Users
                                  where User.userName == "dummyDefault"
                                  select User;

                userIdentity ui = new userIdentity();
                if (queryResult.Count() == 1)
                {
                    var userResult = queryResult.FirstOrDefault();
                    ui.guid = userResult.idUser;
                    ui.UserName = userResult.userName;
                    return ui;
                }
                
                return null;
            }
        }

        public static int updateProfile (Objects.User user, bool changePass)
        {
            

            using (var db = new edShortenerModel())
            {

                try
                {
                    var original = db.Users.Find(user.id);
                    original.dateB = user.dateB;
                    original.gender = user.gender;
                    original.imgScr = user.imgScr == null ? null : cryptDecrypt.cryptPass(user.imgScr); 
                    original.mail = user.mail;
                    original.phone = user.phone;
                    original.realName = user.realName;
                    if(changePass)
                    {
                        string cryptedPass = cryptDecrypt.cryptPass(user.newPass1);
                        original.userPass = cryptedPass;
                    }
               
                    return db.SaveChanges();   
                }
                catch (Exception e)
                {
                    return 0;
                }
            }            

        }

        internal static Objects.User getFullUserByGuid(Guid guid)
        {
            using (var db = new edShortenerModel())
            {
                var original = db.Users.Find(guid);
                Objects.User user = new Objects.User();
                user.dateB = original.dateB;
                user.gender = original.gender;
                user.id = original.idUser;
                user.imgScr = original.imgScr;
                user.mail = original.mail;
                user.name = original.userName;
                user.phone = original.phone;
                user.realName = original.realName;
                return user;
            }
        }

        public static Guid getGuidByName (string name)
        {
            using (var db = new edShortenerModel())
            {
                var queryResult = from User in db.Users
                                  where User.userName == name
                                  select User;

                userIdentity ui = new userIdentity();
                if (queryResult.Count() == 1)
                {
                    var userResult = queryResult.FirstOrDefault();
                    ui.guid = userResult.idUser;
                    ui.UserName = userResult.userName;
                    return ui.guid;
                }

                return new Guid();
            }
        }

        internal static bool verifyOldPass(Objects.User user)
        {
            using (var db = new edShortenerModel())
            {
                var queryResult = from User in db.Users
                                  where User.idUser == user.id
                                  select User;

               
                if (queryResult.Count() == 1)
                {
                    var userResult = queryResult.FirstOrDefault();
                    return userResult.userPass == cryptDecrypt.cryptPass(user.oldPasss);
                }

                return false;
            }
        }
    }
}