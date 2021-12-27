using System.ComponentModel.DataAnnotations;

namespace AuctionSystem.Server.Models.Http.Requests
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Checked { get; set; }
    }
}
