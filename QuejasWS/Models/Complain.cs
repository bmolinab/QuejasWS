using System;
using System.Collections.Generic;

namespace QuejasWS.Models
{
    public partial class Complain
    {
        public Complain()
        {
            Comment = new HashSet<Comment>();
        }

        public string IdComplain { get; set; }
        public int IdUser { get; set; }
        public string IdSubcategory { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        public Subcategory IdSubcategoryNavigation { get; set; }
        public UserC IdUserNavigation { get; set; }
        public ICollection<Comment> Comment { get; set; }
    }
}
