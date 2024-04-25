using Microsoft.AspNetCore.SignalR;

namespace Shareds.Hubs
{
    public class InstructionsHub : Hub
    {
        public InstructionsHub() { }

        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
