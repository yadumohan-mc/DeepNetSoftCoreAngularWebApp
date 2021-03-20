using DeepnetSoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepnetSoft.ViewModels
{
    public class CategoryProductViewModel
    {
        public Category Category { get; set; }
        public List<Category> SubCategories { get; set; }
        public List<Product> Products { get; set; }
        public CategoryProductViewModel()
        {
            this.SubCategories = new List<Category>();
            this.Products = new List<Product>();

        }
    }
}
