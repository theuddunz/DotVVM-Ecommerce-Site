using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using System.Security.Claims;

namespace EntityFrameworkCF.ViewModels
{
    public class UserService
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
    }
}