using Microsoft.AspNetCore.SignalR;

namespace BlazorChatSignalR.Server.Hubs
{
    public class ChatHub:Hub
    {
        public override async Task OnConnectedAsync()
        {
            await SendMessageToChat("", "User connected");
            await base.OnConnectedAsync();
        }
        public async Task SendMessageToChat(string name, string message)
        {
            await Clients.All.SendAsync("ReceivedMessage",name, message);
        }
    }
}
