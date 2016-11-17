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


        public string Username { get; set; } = UserService.GetUsername();


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
    }
}

