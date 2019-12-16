using cinema.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class AdminUser: IIdentifiableEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string Role { get; set; }
        public int EntityId
        { get { return Id; }
            set { value = Id; }
        }
    }
}