using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Runtime.Filters;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;


namespace EntityFrameworkCF.ViewModels
{
    [Authorize(roles: "Admin")]
    public class AdminPageViewModel : MasterpageViewModel
    {
        //variables for the New Product -
        public bool Displayed { get; set; } = false;
        public bool eDisplayed { get; set; } = false;

        public string uName { get; set; }
        public string uPass { get; set; }
        public string uEmail { get; set; }
        public string uCountry { get; set; }
        public UserRole us { get; set; }

        public int uid { get; set; }


        public void ShowEditUser(int id)
        {
            using (var db = new Database())
            {
                var user = db.Users.Find(id);
                uName = user.Username;
                uPass = user.Password;
                uEmail = user.Email;
                uCountry = user.Country;
                us = user.UserRole;
                uid = id;

            }
            eDisplayed = true;
        }
        public void ShowAddUser()
        {
           
           uName = "NewUser";
            uPass = "newuserpassword";
            uEmail = "-";
            uCountry = "-";
            Displayed = true;
        }
        public void AddUser()
        {
            
            using (var db = new Database())
            {
                var user = new User();
                user.Username = uName;
                user.Email = uEmail;
                user.Password = uPass;
                user.Country = uCountry;
                user.UserRole = us;
                db.Users.Add(user);
                db.SaveChanges();
                Displayed = false;
                UserService.LoadUser(Users);
            }
        }

        public void EditUser(int id)
        {
            using (var db = new Database())
            {
                var user = db.Users.Find(id);
                user.Username = uName;
                user.Password = uPass;
                user.Email = uEmail;
                user.Country = uCountry;
                user.UserRole = us;
                db.SaveChanges();
                eDisplayed = false;
                UserService.LoadUser(Users);
            }
        }
        public void DeleteUser(int id)
        {
            using (var db = new Database())
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                UserService.LoadUser(Users);
            }
        }

        public GridViewDataSet<User> Users { get; set; } = new GridViewDataSet<User>
        {
            PageSize = 1000,
            SortExpression = nameof(User.UserID),
            SortDescending = true
        };

        public GridViewDataSet<Product> Products { get; set; } = new GridViewDataSet<Product>
        {
            SortExpression = nameof(Product.ProductID),
            SortDescending = true,
            PageSize = 1000
        };
        public override Task Load()
        {
            ProductService.LoadProduct(Products);
            UserService.LoadUser(Users);
            return base.Load();
        }
    }
}

