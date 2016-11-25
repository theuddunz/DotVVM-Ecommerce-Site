using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFrameworkCF.ViewModels
{
    public class Order
    {
        public int  OrderID { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public string Status { get; set; }
        public double Price { get; set; }
        public int AddressID { get; set; }
    }
}