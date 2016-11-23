using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;




namespace EntityFrameworkCF.ViewModels
{
    public class LoginPageViewModel : MasterpageViewModel
    {
        public void Redirect()
        {
            Context.RedirectToRoute("Register");
        }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Bind(Direction.ServerToClient)]
        public string ErrorMessage { get; set; }


        public void Login()
        {
            var identity = UserService.Login(Username, Password);
            if (identity == null)
            {
                ErrorMessage = "Your Email or Password Are incorrect.";
            }
            else
            {
                Context.OwinContext.Authentication.SignIn(identity);
                Context.RedirectToRoute("Index");
            }

        }

    }
}

