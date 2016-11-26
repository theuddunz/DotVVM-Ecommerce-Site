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
        //variables for Display
        public bool Displayed { get; set; } = false;
        public bool eDisplayed { get; set; } = false;
        public bool pDisplayed { get; set; } = false;
        public bool peDisplayed { get; set; } = false;
        //varibles for User
        public string uName { get; set; }
        public string uPass { get; set; }
        public string uEmail { get; set; }
        public string uCountry { get; set; }
        public UserRole us { get; set; }

        //Varibles for pass the ID
        public int pid { get; set; }
        public int uid { get; set; }

        //Variables for the Product
        public string pName { get; set; }
        public double pPrice { get; set; }
        public string pDesc { get; set; }
        public string imgUrl { get; set; }

        //Variables For Orders

        public bool ShowAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string AL1 { get; set; }
        public string AL2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        

        //Show metodh Products
        public void ShowShippingAddress(int id)
        {
            using (var db = new Database())
            {
                var order = db.Orders.Find(id);
                var user = db.Users.Find(order.UserID);
                CustomerEmail = user.Email;
                AL1 = order.AddressLine1;
                AL2 = order.AddressLine2;
                City = order.City;
                State = order.State;
                PostalCode = order.PostalCode;
                ShowAddress = true;
                OrderService.LoadItemOrder(OrderItems,id);
            }
        }
        public void ShowEditProduct(int id)
        {
            using (var db = new Database())
            {
                var product = db.Products.Find(id);
                pName = product.Name;
                pPrice = product.Price;
                pDesc = product.Description;
                imgUrl = product.Image;
                peDisplayed = true;
                pid = id;
            }
        }
        public void ShowAddProduct()
        {
            pName = "New Product";
            pPrice = 999;
            imgUrl = "https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg";
            pDesc = "-";
            pDisplayed = true;
        }
        //Metodh for The Product
        public void AddProduct()
        {
            using (var db = new Database())
            {
                var product = new Product();
                product.Name = pName;
                product.Price = pPrice;
                product.Image = imgUrl;
                product.Description = pDesc;
                db.Products.Add(product);
                db.SaveChanges();
                pDisplayed = false;
                ProductService.LoadProduct(Products);
            }
        }
        public void EditProduct(int id)
        {
            using (var db = new Database())
            {
                var product = db.Products.Find(id);
                product.Name = pName;
                product.Price = pPrice;
                product.Image = imgUrl;
                product.Description = pDesc;
                db.SaveChanges();
                peDisplayed = false;
                ProductService.LoadProduct(Products);
            }
        }
        public void DeleteProduct(int id)
        {
            using (var db = new Database())
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                ProductService.LoadProduct(Products);
            }
        }

        //Show metodh Users
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
        //metodh for the users
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
            PageSize = 10,
            SortExpression = nameof(User.UserID),
            SortDescending = true
        };

        public GridViewDataSet<Product> Products { get; set; } = new GridViewDataSet<Product>
        {
            SortExpression = nameof(Product.ProductID),
            SortDescending = true,
            PageSize = 10
        };

        public GridViewDataSet<Order> Orders { get; set; } = new GridViewDataSet<Order>
        {
            SortExpression = nameof(Order.OrderDate),
            SortDescending = true,
            PageSize = 5
        };

        public GridViewDataSet<OrderItem> OrderItems { get; set; } = new GridViewDataSet<OrderItem>
        {
            SortExpression = nameof(OrderItem.OrderID),
            SortDescending = true,
            PageSize = 100

        };
        public override Task Load()
        {
            ProductService.LoadProduct(Products);
            UserService.LoadUser(Users);
            OrderService.LoadOrders(Orders);
            return base.Load();
        }
    }
}

