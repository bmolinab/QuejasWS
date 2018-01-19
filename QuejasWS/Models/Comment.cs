using System;
using System.Collections.Generic;

namespace QuejasWS.Models
{
    public partial class Comment
    {
        public string IdComment { get; set; }
        public string Comment1 { get; set; }
        public string IdComplain { get; set; }
        public int IdUser { get; set; }

        public Complain IdComplainNavigation { get; set; }
        public UserC IdUserNavigation { get; set; }
    }
}
