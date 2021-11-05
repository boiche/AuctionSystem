using AuctionSystem.Data.Model;
using AuctionSystem.Server.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.IO.Pipelines;
using System.Text;

namespace AuctionSystem.Server.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(AuctionSystemContext context) : base(context) { }

        [HttpPost]
        public void Register()
        {
            string body = Encoding.Default.GetString(System.Buffers.BuffersExtensions.ToArray(HttpContext.Request.BodyReader.ReadAsync().Result.Buffer)); //reader.ReadToEndAsync();
            User newUser = JsonConvert.DeserializeObject<Root>(body).data;
            context.Users.Add(newUser);
            context.SaveChanges();
            //TODO: return response for 200 OK
        }

        [HttpPost]
        public void Login()
        {

        }
    }
}
