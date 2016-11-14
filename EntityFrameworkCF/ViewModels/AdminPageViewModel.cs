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
    [Authorize]
    public class AdminPageViewModel : MasterpageViewModel
    {
        //variables for the New Product
        [Required(ErrorMessage = "The name is required")]
        public string pNameN { get; set; }
        public string pDescN { get; set; }
        [Required(ErrorMessage = "The price is Required")]
        public double pPriceN { get; set; }
        public string pImgN { get; set; }

        public string pMessageN { get; set; }
        public bool pVisibileN { get; set; }
        [Bind(Direction.ServerToClient)]
        public string ErrorMessage { get; set; }

        //Variable for DeleteProduct
        public int pIDD { get; set; }
        public bool pVisibleD { get; set; }
        //Variable For EditProduct
        public bool pVisibleE { get; set; }

        //Variable for UsersCommands
        [Required(ErrorMessage ="The Username is required.")]
        public string uName { get; set; }
        [Required]
        public string uPass { get; set; }
        [Required(ErrorMessage = "The Email is required")]
        public string uEmail { get; set; }
        public string uCountry { get; set; }
        [Required(ErrorMessage = "The UserID is required." )]
        public int uID { get; set; }
        public UserRole ur { get; set; }
        public bool uVisibleN { get; set; }
        public bool uVisibleE { get; set; }
        public bool uVisibleD { get; set; }

        //FUNCTION FOR SET TRUE AND FALSE IN THE BOOLEAN VARIABLE
        public void CallUN()
        {
            uVisibleN = true;
        }
        public void CallUD()
        {
            uVisibleD = true;
        }
        public void CallUE()
        {
            uVisibleE = true;
        }

        public void CallPN()
        {
            pVisibileN = true;
        }
        public void CallPD()
        {
            pVisibleD = true;
        }
        public void CallPE()
        {
            pVisibleE = true;
        }
        public void setfalseN()
        {
            pVisibileN = false;
        }
        public void setfalseD()
        {
            pVisibleD = false;
        }
        public void setfalseE()
        {
            pVisibleE = false;
        }
        public void setfalseuN()
        {
            uVisibleN = false;
        }
        public void setfalseuD()
        {
            uVisibleD = false;
        }
        public void setfalseuE()
        {
            uVisibleE = false;
        }
        public void addpr()
        {
            using (Database db = new Database())
            {
                var pr = new Product();

                pr.Name = pNameN;
                pr.Description = pDescN;
                pr.Price = pPriceN;
                pr.Image = pImgN;
                db.Products.Add(pr);
                db.SaveChanges();
                pMessageN = "The Product" + pNameN + "has been inserted Correctly ";
                pVisibileN = false;
                Context.RedirectToRoute("Admin");
            }
        }
        public void DeleteProduct()
        {
            using (var db = new Database())
            {
                try
                {
                    var entity = db.Products.Find(pIDD);
                    if (entity != null)
                    {
                        db.Products.Remove(entity);
                        db.SaveChanges();
                        Context.RedirectToRoute("Admin");
                        pVisibleD = false;
                    }
                    else
                    {
                        ErrorMessage = "The ProductID you insert does not exist.";
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
        public void EditProduct()
        {
            using (var db = new Database())
            {

                try
                {
                    var entity = db.Products.Find(pIDD);
                    if (entity != null)
                    {
                        entity.Name = pNameN;
                        entity.Price = pPriceN;
                        entity.Description = pDescN;
                        entity.Image = pImgN;
                        db.SaveChanges();
                        pVisibleE = false;
                        Context.RedirectToRoute("Admin");
                    }
                    else
                    {
                        ErrorMessage = "The ProductID you insert does not exist.";
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }

        public void AddUser()
        {
            using (var db = new Database())
            {
                var user = new User();
                user.Username = uName;
                user.Password = uPass;
                user.Email = uEmail;
                user.Country = uCountry;
                user.UserRole = ur;
                
                db.Users.Add(user);
                db.SaveChanges();
                uVisibleN = false;
                Context.RedirectToRoute("Admin");
            }
        }

        public void DeleteUser()
        {
            try
            {
                using (var db = new Database())
                {
                    var entity = db.Users.Find(uID);
                    if (entity != null)
                    {
                        db.Users.Remove(entity);
                        uVisibleD = false;
                        db.SaveChanges();
                        Context.RedirectToRoute("Admin");
                    }
                    else
                    {
                        ErrorMessage = "The User with this ID does not exist.";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void EditUser()
        {
            try
            {
                using (var db = new Database())
                {
                    var entity = db.Users.Find(uID);
                    if (entity != null)
                    {
                        entity.Username = uName;
                        entity.Password = uPass;
                        entity.Email = uEmail;
                        entity.Country = uCountry;
                        entity.UserRole = ur;
                        db.SaveChanges();
                        uVisibleE = false;
                        Context.RedirectToRoute("Admin");
                    }
                    else
                    {
                        ErrorMessage = "The User with this id does not exist.";
                    }

                }
            }
            catch (Exception)
            {

                throw;
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

