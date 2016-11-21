using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EntityFrameworkCF.ViewModels
{
    public class Cart
    {
        
        public int CartID { get; set; }
        public double Total { get; set; }
        public int Count { get; set; }
        public int UserID { get; set; }
       
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        
    }
}