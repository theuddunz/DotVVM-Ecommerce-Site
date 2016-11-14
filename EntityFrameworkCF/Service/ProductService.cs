using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotVVM.Framework.Controls;
namespace EntityFrameworkCF.ViewModels
{
    public class ProductService
    {
        public static void LoadProduct(GridViewDataSet<Product> dataset)
        {
            using (var db = new Database())
            {
                var query = from p in db.Products
                            select p;
                dataset.LoadFromQueryable(query);
            }
        }
    }
}