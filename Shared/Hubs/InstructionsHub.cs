using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace Shareds.Hubs
{
    public class InstructionsHub : Hub
    {
        private HubConnection? HC;
        private List<string> _instructions;

        public bool IsConnected => HC?.State == HubConnectionState.Connected;

        public InstructionsHub() 
        {
            _instructions = new List<string>();
        }

        public async Task ConnectToHub()
        {
            HC = new HubConnectionBuilder()
                .WithUrl("https://localhost:7289/instructionshub")
                .WithAutomaticReconnect()
                .Build();

            HC.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var formattedMessage = $"{user}: {message}";
                _instructions.Add(formattedMessage);
            });

            await HC.StartAsync();
        }

        // Definit le client pour l'envoi du message
        public Task ToSend(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessage(string user, string message)
        {
            if (HC is not null)
            {
                await HC.SendAsync("ToSend", user, message);
            }
        }

        public List<string> GetInstructions()
        {
            return _instructions;
        }

        public async ValueTask DisposeAsync()
        {
            if (HC is not null)
            {
                await HC.DisposeAsync();
            }
        }
    }
}
