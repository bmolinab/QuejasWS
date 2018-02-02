using System;
using System.Collections.Generic;

namespace QuejasWS.Models
{
    public partial class UserC
    {
        public UserC()
        {
            Comment = new HashSet<Comment>();
            Complain = new HashSet<Complain>();
        }

        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Locale { get; set; }
        public string TimeZone { get; set; }
        public string Verified { get; set; }
        public string LinkFacebook { get; set; }
        public string LinkTwitter { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string FacebookId { get; set; }
        public string TwitterId { get; set; }

        public ICollection<Comment> Comment { get; set; }
        public ICollection<Complain> Complain { get; set; }
    }
}
