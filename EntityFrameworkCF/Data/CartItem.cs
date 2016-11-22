using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EntityFrameworkCF.ViewModels
{
    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }

        public int ProductID { get; set; }

        public virtual Product Product { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public int CartID { get; set; }
    }
}