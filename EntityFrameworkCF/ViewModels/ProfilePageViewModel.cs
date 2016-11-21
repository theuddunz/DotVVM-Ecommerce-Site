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
        public string email { get; set; } = UserService.GetEmail();
        [Required(ErrorMessage = "The Password is Required.")]
        public string password { get; set; } = UserService.GetPassword();
        public string country { get; set; } = UserService.GetCountry();
        public string Username { get; set; } = UserService.GetUsername();

        public bool Enabled { get; set; } = false;
        
        public void SetTrue()
        {
            Enabled = true;
        }
        public void SetFalse()
        {
            Enabled = false;
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

    }
}

