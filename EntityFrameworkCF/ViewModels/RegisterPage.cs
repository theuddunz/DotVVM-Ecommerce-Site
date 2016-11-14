using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCF.ViewModels
{
    public class RegisterPage : DotvvmViewModelBase
    {

        public string Title { get; set; }
        [Required(ErrorMessage ="The Username is required")]
        public string usn { get; set; }
        [Required(ErrorMessage ="The Password is required")]
        public string pas { get; set; }
        [Required(ErrorMessage ="The Email is required")]
        [EmailAddress(ErrorMessage="This is not a valid E-mail")]
        public string email { get; set; }
        public string country { get; set; }
        [Bind(Direction.ServerToClient)]
        public string ErrorMessage { get; set; }

        Database db = new Database();
        public RegisterPage()
        {
            Title = "REGISTER PAGE";

        }

        public void Register()
        {
            try
            {
                var user = new User();
                var checkuserandemail = from p in db.Users
                                        where (usn == p.Username || email == p.Email)
                                        select p;
                if (checkuserandemail.Count() == 0)
                {
                    user.Username = usn;
                    user.Password = pas;
                    user.Email = email;
                    user.Country = country;
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                else
                {
                    usn = null;
                    email = null;
                    ErrorMessage = "This Email or Username are alredy taken";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
