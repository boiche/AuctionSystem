using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AuctionSystem.Data.Model;

namespace AuctionSystem.Server.Chat
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("ChatHub hub connected");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("ChatHub hub disconnected");
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string recipient, string message)
        {
            try
            {
                User user1 = JsonConvert.DeserializeObject<User>(user);
                User recipient1 = JsonConvert.DeserializeObject<User>(recipient);
                Console.WriteLine("Message received");
                await Clients.Caller.SendCoreAsync("ReceiveMessage", new object[3] { user1.Username, recipient1.Username, message });
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
