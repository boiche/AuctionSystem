using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AuctionSystem.Data.Model;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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

        public void BeginChat(string user, string recipient)
        {
            User user1 = JsonConvert.DeserializeObject<User>(user);
            string groupName = GetGroupName(user1.Username, recipient);
            Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessage(string user, string recipient, string message)
        {
            try
            {
                User user1 = JsonConvert.DeserializeObject<User>(user);
                Console.WriteLine("Message received");
                string groupName = GetGroupName(user1.Username, recipient);
                await Clients.Group(groupName).SendAsync("ReceiveMessage", user1.Username, recipient, message);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private string GetGroupName(string firstName, string secondName)
        {
            byte[] userBytes = Encoding.UTF8.GetBytes(firstName.ToCharArray());
            byte[] recipientBytes = Encoding.UTF8.GetBytes(secondName.ToCharArray());
            var wholeArray = new byte[userBytes.Length + recipientBytes.Length];
            userBytes.CopyTo(wholeArray, 0);
            recipientBytes.CopyTo(wholeArray, userBytes.Length);
            ulong groupName = 0;
            for (int i = 0; i < wholeArray.Length; i++)
            {
                groupName += wholeArray[i];
            }
            return groupName.ToString();
        }
    }
}
