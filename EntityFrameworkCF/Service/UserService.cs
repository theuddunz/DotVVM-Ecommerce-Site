using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotVVM.Framework.Controls;
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
    }
}