using System;
using System.Collections.Generic;

#nullable disable

namespace AuctionSystem.Data.Model
{
    public partial class User
    {
        public User()
        {
            Auctions = new HashSet<Auction>();
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public int? Role { get; set; }

        public virtual Role RoleNavigation { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
