using Nancy.Authentication.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;

namespace EdwardShortener.Objects
{
    public class User : IUserMapper
    {
        public UserUrlList userUrlList { get; set; }
        public string name { get; set; }
        public Guid id;
        public string imgScr { get; set; }
        public string realName { get; set; }
        public string dateB { get; set; }
        public string gender { get; set; }
        public string mail { get; set; }
        public string phone { get; set; }
        public string changePass { get; set; }
        public string oldPasss { get; set; }
        public string newPass1 { get; set; }
        public string newPass2 { get; set; }
        public string error { get; set; }

        public User()
        {

        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var u = Functions.UserFunctions.getUserIdentityByGuid(identifier);
            context.CurrentUser = u;
            return u;
        }
    }

    public class userIdentity : IUserIdentity
    {
        public Guid guid { get; set; }

        public string UserName { get; set; }

        public IEnumerable<string> Claims { get; set; }


    }
}