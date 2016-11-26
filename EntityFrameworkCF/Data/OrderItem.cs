using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EntityFrameworkCF.ViewModels
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }

        
    }
}