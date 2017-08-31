using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCookBook.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        //public IQueryable<RecipeModel> Recipes { get; set; }

        //...
    }
}
