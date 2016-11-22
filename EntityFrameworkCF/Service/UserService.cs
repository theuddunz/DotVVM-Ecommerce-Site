using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace EntityFrameworkCF.ViewModels
{
    public class UserService : DotvvmViewModelBase
    {
        public static void LoadUser(GridViewDataSet<User> dataset)
        {
            using (var db = new Database())
            {
                var query = from p in db.Users
                            select p;
                dataset.LoadFromQueryable(query);
            }
        }

        public static int? GetCurrentUserId()
        {
            var identity = HttpContext.Current.GetOwinContext().Authentication.User.Identity as ClaimsIdentity;
            if (identity.IsAuthenticated)
            {
                var id = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Convert.ToInt32(id);

            }
            else
            {
                return null;
            }
        }

        public static string GetUsername()
        {
            using (var db = new Database())
            {
                var userid = db.Users.Find(UserService.GetCurrentUserId());
                if (userid != null)
                {
                    return userid.Username.ToString();
                }
                else
                {
                    return null;
                }

            }

        }

        public static string UserName()
        {
            var identity = HttpContext.Current.GetOwinContext().Authentication.User.Identity as ClaimsIdentity;
            if (identity.IsAuthenticated)
            {
                var username = identity.FindFirst(ClaimTypes.Name).Value;
                return username;
            }
            return "Guest";
        }

        public static string GetEmail()
        {
            using (var db = new Database())
            {
                var user = db.Users.Find(GetCurrentUserId());
                return user.Email;
            }
        }
        public static string GetPassword()
        {
            using (var db = new Database())
            {
                var user = db.Users.Find(GetCurrentUserId());
                return user.Password;
            }
        }
        public static string GetCountry()
        {
            using (var db = new Database())
            {
                var user = db.Users.Find(GetCurrentUserId());
                return user.Country;
            }
        }
        public static void Login(string Usern, string pass, string mess)
        {
            using (var db = new Database())
            {
                var query = from p in db.Users
                            where p.Username == Usern && p.Password == pass
                            select p;
                var user = query.SingleOrDefault();
                if (query.Count() != 0)
                {
                    var claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.Username));
                    claims.Add(new Claim(ClaimTypes.Role, Convert.ToString(user.UserRole)));
                    var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.Current.GetOwinContext().GetDotvvmContext().OwinContext.Authentication.SignIn(identity);                   
                }
                else
                {
                    mess = "Invalid Email or Password.";
                }
            }
        }
    }
}