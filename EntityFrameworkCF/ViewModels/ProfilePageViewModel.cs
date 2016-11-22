using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using System.ComponentModel.DataAnnotations;
using DotVVM.Framework.Runtime.Filters;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
namespace EntityFrameworkCF.ViewModels
{
    [Authorize]
    public class ProfilePageViewModel : MasterpageViewModel
    {
        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "The Email is Not Valid.")]
        public string email { get; set; } 
        [Required(ErrorMessage = "The Password is Required.")]
        public string password { get; set; } 
        public string country { get; set; } 
        public string Username { get; set; }

        public bool Enabled { get; set; } = false;
        public bool mEnabled { get; set; } = true;

        public void SetTrue()
        {
            Enabled = true;
            mEnabled = false;
        }
        public void SetFalse()
        {
            Enabled = false;
            mEnabled = true;
        }

        public void SaveChanges()
        {
            using (var db = new Database())
            {
                var id = UserService.GetCurrentUserId();
                var user = db.Users.Find(id);
                user.Email = email;
                user.Country = country;
                user.Password = password;
                db.SaveChanges();
                Context.RedirectToRoute("ProfilePage");
            }
        }

        public override Task PreRender()
        {
            email = UserService.GetEmail();
            password = UserService.GetPassword();
            country = UserService.GetCountry();
            Username = UserService.GetUsername();
            return base.PreRender();
        }
    }
}

