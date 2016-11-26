using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace EntityFrameworkCF.ViewModels
{
    public class Database : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Address> ShippingAddress { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public Database() : base("name=Connection") { }
    }
}