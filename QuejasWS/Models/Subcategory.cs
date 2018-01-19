using System;
using System.Collections.Generic;

namespace QuejasWS.Models
{
    public partial class Subcategory
    {
        public Subcategory()
        {
            Complain = new HashSet<Complain>();
            SubcategoryInstitution = new HashSet<SubcategoryInstitution>();
        }

        public string IdSubcategory { get; set; }
        public string IdCategory { get; set; }
        public string Name { get; set; }

        public Category IdCategoryNavigation { get; set; }
        public ICollection<Complain> Complain { get; set; }
        public ICollection<SubcategoryInstitution> SubcategoryInstitution { get; set; }
    }
}
