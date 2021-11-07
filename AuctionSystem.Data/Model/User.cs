using Newtonsoft.Json;
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

        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "SecondName")]
        public string SecondName { get; set; }

        [JsonProperty(PropertyName = "ThirdName")]
        public string ThirdName { get; set; }

        public string FullName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "Phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "Username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "Address")]
        public string Address { get; set; }

        public int? Role { get; set; }

        public virtual Role RoleNavigation { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
