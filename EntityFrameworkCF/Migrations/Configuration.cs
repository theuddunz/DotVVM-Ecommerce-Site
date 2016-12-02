using EntityFrameworkCF.ViewModels;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
namespace EntityFrameworkCF.Migrations
{


    internal sealed class Configuration : DbMigrationsConfiguration<EntityFrameworkCF.ViewModels.Database>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "EntityFrameworkCF.ViewModels.Database";
        }

        protected override void Seed(EntityFrameworkCF.ViewModels.Database context)
        {

            if (!context.Users.Any())
            {
                var GuestUser = new User
                {
                    Username = "GuestUser",
                    Email = "guestuser@guest.com",
                    Password = "guestguest",
                    UserRole = UserRole.User
                };
                var AdminUser = new User
                {
                    Username = "AdminUser",
                    Email = "adminuser@admin.com",
                    Password = "adminadmin",
                    UserRole = UserRole.Admin
                };


                context.Users.Add(GuestUser);
                context.Users.Add(AdminUser);
                context.SaveChanges();
            }
            if (!context.Products.Any())
            {
                var product = new Product
                {
                    Name = "TestProduct",
                    Description = "TestDescription",
                    Price = 9999,
                    Image = "https://static1.squarespace.com/static/5346b528e4b0e4b25f532794/t/54ecb349e4b0a3ebc2cfdcc6/1424798562058/ifttt-animated-gif.gif",
                };
                context.Products.Add(product);
                context.SaveChanges();
                }

        }
    }
}
