using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Controls;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace EntityFrameworkCF.ViewModels
{
    public class IndexViewModel : MasterpageViewModel
    {

        public string pName { get; set; }
        public double pPrice { get; set; }
        public string pDesc { get; set; }
        public string pIMG { get; set; }
        public string Username { get; set; } = UserService.GetUsername();
        public bool Displayed { get; set; } = false;
        public int pid { get; set; }


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

            }

            return base.PreRender();
        }
        public void AddToCart(int productid)
        {
            using (var db = new Database())
            {
                var userid = UserService.GetCurrentUserId();
                var product = db.Products.Find(productid);
                Cart cart = db.Carts.Find(userid);
                var citem = new CartItem();
                citem.Price = product.Price;
                citem.Name = product.Name;
                citem.ProductID = product.ProductID;
                db.CartItems.Add(citem);
                cart.CartItems.Add(citem);
                db.SaveChanges();

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
    }
}

