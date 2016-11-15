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

        [Required(ErrorMessage = "The Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }

        [Bind(Direction.ServerToClient)]
        public string ErrorMessage { get; set; }


        public void Login()
        {
            try
            {
                using (Database db = new Database())
                {
                    var checkdata = from p in db.Users
                                    where (p.Username == Username && p.Password == Password)
                                    select p.UserID;
                    
                   /* Test Code for the new Authentication
                    Set the Email as the PrimaryKey
                    var email = db.users.find(Email);
                    if (email.password == Password && email.Email == InputEmail)
                    {
                    var claims = new list<Claim>
                    claims.Add(()) NameIdentifier - Email
                    claims.Add(()) Name - Username
                    claims.add(()) UserRole - User and Admin

                    var identity = new ClaimsIdentity(claims,DefaultAuthenticationTypes.ApplicationCookie);
                    Context.OwinContext.Authentication.SignIn(identity);
                    if(email.UserRole == Admin)
                    {
                    Context.RedirectToRoute("Admin");
                    
                    }else (Else for redirect the user checking the UserRole)
                    {
                    Context.RedirectToRoute("Index");
                    }
                    }else(Else for The first condition)
                    {
                    ErrorMessage = "Your Email or Password are incorrect";
                    }
                  */

                    if (checkdata.Count() != 0)
                    {
                        var claims = new List<Claim>();

                        claims.Add(new Claim(ClaimTypes.NameIdentifier, Username));
                        claims.Add(new Claim(ClaimTypes.Name, Username));
                       
                        var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                        Context.OwinContext.Authentication.SignIn(identity);
                        Context.RedirectToRoute("Admin");

                    }
                    else
                    {
                        ErrorMessage = "The Email or The Password are incorrect";
                    }
                }
            }
            catch (Exception)
            {

                throw;

            }
        }
    }
}

