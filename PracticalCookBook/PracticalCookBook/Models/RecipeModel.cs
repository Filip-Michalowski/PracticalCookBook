using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCookBook.Models
{
    class RecipeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public string Preparation { get; set; }

        //public IQueryable<TagModel> Tags { get; set; }

        //...
    }
}
