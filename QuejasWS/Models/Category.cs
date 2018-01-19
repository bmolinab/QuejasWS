using System;
using System.Collections.Generic;

namespace QuejasWS.Models
{
    public partial class Category
    {
        public Category()
        {
            Subcategory = new HashSet<Subcategory>();
        }

        public string IdCategory { get; set; }
        public string Name { get; set; }

        public ICollection<Subcategory> Subcategory { get; set; }
    }
}
