using System.ComponentModel.DataAnnotations;

namespace AuctionSystem.Server.Models.Http.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        [Required]
        public string ThirdName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
