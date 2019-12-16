using cinema.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class ReceiveMessage :IIdentifiableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}