using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Controls;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using DotVVM.Framework.Runtime.Filters;
using System.Web;
using System.Security.Claims;

namespace EntityFrameworkCF.ViewModels
{
    public class IndexViewModel : MasterpageViewModel
    {

        public string pName { get; set; }
        public double pPrice { get; set; }
        public string pDesc { get; set; }
        public string pIMG { get; set; }
        public string Username { get; set; } = UserService.GetUsername() ?? "Guest";
        public bool Displayed { get; set; } = false;
        public int pid { get; set; }
        public int CartItem { get; set; } = Convert.ToInt32(CartService.GetCartCountItem());
        public bool GoLogin { get; set; } = false; //Implementa la ModalView Per Ricordarti di Accedere

        public GridViewDataSet<Cart> Carts { get; set; } = new GridViewDataSet<Cart>
        {
            SortExpression = nameof(Cart.CartItems),
            PageSize = 1000,
            SortDescending = false
        };
        public GridViewDataSet<Product> Products { get; set; } = new GridViewDataSet<Product>
        {
            SortExpression = nameof(Product.ProductID),
            SortDescending = false,
            PageSize = 1000

        };

        public List<Product> ListP { get; set; } = new List<Product>
        {

        };
        public override Task PreRender()
        {
            using (var db = new Database())
            {
                ProductService.LoadProduct(Products);
                //CartService.LoadDataCart(Carts);
            }

            return base.PreRender();
        }


        public void AddToCart(int productid)
        {
            if (UserService.GetCurrentUserId() != null)
            {
                using (var db = new Database())
                {
                    var user = db.Users.Find(UserService.GetCurrentUserId());
                    var id = user.UserID;
                    var product = db.Products.Find(productid);
                    var citem = new CartItem();
                    var query = from p in db.Carts
                                where p.UserID == id
                                select p;
                    citem.Price = product.Price;
                    citem.Name = product.Name;
                    citem.ProductID = product.ProductID;
                    var cart = query.FirstOrDefault();

                    if (query.Count() != 0)
                    {
                        cart.Count++;
                        cart.CartItems.Add(citem);
                        CartItem = Convert.ToInt32(CartService.GetCartCountItem());
                        db.SaveChanges();
                    }
                    else
                    {
                        var newcart = new Cart();
                        newcart.Count++;
                        newcart.UserID = id;
                        newcart.CartItems.Add(citem);
                        db.Carts.Add(newcart);
                        CartItem = Convert.ToInt32(CartService.GetCartCountItem());
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                GoLogin = true;
            }

        }

        public void DeleteProduct(int productid)
        {
            using (var db = new Database())
            {
                var product = db.Products.Find(productid);
                db.Products.Remove(product);
                db.SaveChanges();
            }

        }
        public void EditProduct(int productid)
        {

            using (var db = new Database())
            {

                var product = db.Products.Find(productid);
                product.Name = pName;
                product.Price = pPrice;
                product.Image = pIMG;
                product.Description = pDesc;
                db.SaveChanges();
                Displayed = false;

            }
        }

        public void ShowEditProduct(int productid)
        {
            using (var db = new Database())
            {
                var product = db.Products.Find(productid);
                pName = product.Name;
                pPrice = product.Price;
                pDesc = product.Description;
                pIMG = product.Image;
                pid = productid;
                Displayed = true;
            }
        }

        public void AddProduct()
        {
            using (var db = new Database())
            {
                var product = new Product();
                product.Name = "New Product";
                product.Price = 999;
                product.Image = "https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg";
                product.Description = "No Description Available";
                db.Products.Add(product);
                db.SaveChanges();
            }
        }
        public void Redirect()
        {
            if (UserService.GetCurrentUserId() == null)
            {
                Context.RedirectToRoute("LoginPage");
            }
            else
            {
                Context.RedirectToRoute("ProfilePage");
            }

        }
    }
}

