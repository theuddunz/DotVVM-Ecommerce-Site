using DotVVM.Framework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFrameworkCF.ViewModels
{
    public class OrderService
    {
        public static void LoadMyOrders(GridViewDataSet<Order> dataset) // Load The Orders For The USER not for The ADMINPAGE.
        {
            using (var db = new Database())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var user = db.Users.Find(UserService.GetCurrentUserId());
                int userid = Convert.ToInt32(UserService.GetCurrentUserId());
                var query = from p in db.Orders
                            where p.UserID == userid
                            orderby p.OrderDate
                            select p;

                dataset.LoadFromQueryable(query);
            }
        }

        public static void LoadOrders(GridViewDataSet<Order> dataset)
        {
            using (var db = new Database())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var query = from p in db.Orders
                            select p;
                dataset.LoadFromQueryable(query);
            }
        }

        public static void LoadItemOrder(GridViewDataSet<OrderItem> dataset, int id)
        {
            using (var db = new Database())
            {
                var order = db.Orders.Find(id);
                var query = from p in db.OrderItems
                            where p.OrderID == order.OrderID
                            select p;
                dataset.LoadFromQueryable(query);
            }
        }
    }
}