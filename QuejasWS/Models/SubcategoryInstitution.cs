using System;
using System.Collections.Generic;

namespace QuejasWS.Models
{
    public partial class SubcategoryInstitution
    {
        public string IdSubCategoryInstitution { get; set; }
        public string IdSubcategory { get; set; }
        public string IdInstitution { get; set; }

        public Institution IdInstitutionNavigation { get; set; }
        public Subcategory IdSubcategoryNavigation { get; set; }
    }
}
