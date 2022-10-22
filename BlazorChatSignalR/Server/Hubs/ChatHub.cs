using Microsoft.AspNetCore.SignalR;

namespace BlazorChatSignalR.Server.Hubs
{
    public class ChatHub:Hub
    {
        private static Dictionary<string, string> Users = new Dictionary<string, string>();
        
        public override async Task OnConnectedAsync()
        {
            string userName=Context.GetHttpContext().Request.Query["username"];
            Users.Add(Context.ConnectionId, userName);
            await SendMessageToChat(string.Empty,$"{userName} connected!");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            string userName = Users.FirstOrDefault(u => u.Key == Context.ConnectionId).Value;
            await SendMessageToChat(string.Empty,$"{userName} Left..");
        }

        public async Task SendMessageToChat(string name, string message)
        {
            await Clients.All.SendAsync("ReceivedMessage",name, message);
        }
    }
}
