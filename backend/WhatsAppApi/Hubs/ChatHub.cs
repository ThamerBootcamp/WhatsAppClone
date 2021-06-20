using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WhatsAppApi.Hubs
{
    public class ChatHub : Hub
    {
        // jwt, room, receiveId, message
        public async Task SendMessage(string jwt, string room, int receiverId, string message)
        {
            // store the message in the db
            await Clients.Groups(room).SendAsync("ReceiveMessage", message, receiverId);
        }

        public async Task JoinRoom(string roomName)
        {
            // you can add jwt authN
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            // await Clients.Group(roomName).SendAsync("Rece/iveNotify", userName + " has joined the room.");
        }
    }
}