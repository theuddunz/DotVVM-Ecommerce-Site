using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCF.ViewModels
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage ="The Name of the Product is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="The Price of the Product is Required")]
        public double Price { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Image { get; set; }
        

    }
}