using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EntityFrameworkCF.ViewModels
{
    public class Order
    {
        [Key]
        public int  OrderID { get; set; }
        public DateTime OrderDate { get; set; }  
        public OrderStatus Status { get; set; }
        public double Total { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int UserID { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}