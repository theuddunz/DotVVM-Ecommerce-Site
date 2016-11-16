using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFrameworkCF.ViewModels
{
    public class CartItem
    {

        public int CartItemID { get; set; }

        public int ProductID { get; set; }

        public virtual Product Product { get; set; }

        public int CartID { get; set; }
        public double Price { get; set; }

        public virtual Cart Cart { get; set; }

        public int Quantity { get; set; }

    }
}