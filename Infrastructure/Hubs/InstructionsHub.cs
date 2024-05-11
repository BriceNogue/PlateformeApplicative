using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Infrastructure.Hubs
{
    public class InstructionsHub : Hub
    {
        private HubConnection? HC;
        private readonly string _hubURL = "https://localhost:7281/instructionshub";
        private List<string> instructions;

        public bool IsConnected => HC?.State == HubConnectionState.Connected;

        public InstructionsHub()
        {
            instructions = new List<string>();
        }

        public async Task ConnectToHub()
        {
            HC = new HubConnectionBuilder()
                .WithUrl(_hubURL)
                .WithAutomaticReconnect()
                .Build();

            /*HC.On<int, string>("ReceiveInstruction", (postId, instruction) =>
            {
                var formattedMessage = $"{postId}: {instruction}";
                instructions.Add(formattedMessage);
            }); */

            await HC.StartAsync();
        }

        // pour ajouter le poste au groupe qui reçoit les instructions
        public async Task JoinPostGroup(int postId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, postId.ToString());
        }

        // Definit le client pour l'envoi de l'instruction
        public Task SendInstruction(int postId, string instruction)
        {
            return Clients.Group(postId.ToString()).SendAsync("ReceiveInstruction", postId, instruction);
        }

        // Deconnecter le Hub
        public async ValueTask DisposeAsync()
        {
            if (HC is not null)
            {
                await HC.DisposeAsync();
            }
        }

        // Methode invoquée depuis WPF pour envoyer les images
        public Task SendScreenImage(byte[] imageBytes)
        {
            string base64Image = SetScreenImage(imageBytes);

            return Clients.All.SendAsync("ReceiveScreenImage", base64Image);
        }

        private string SetScreenImage(byte[] imageBytes)
        {
            var base64Image = $"data:image/jpge;base64,{Convert.ToBase64String(imageBytes)}";
            return base64Image;
        }

        // Methode appellée depuis Blazor pour recevoir les images
        /*public Task GetScreenImage(Action<string> callback)
        {
            var base64Image = "";

            if (HC != null)
            {
                HC.On<byte[]>("ReceiveScreenImage", async (imageBytes) =>
                {
                    base64Image = SetScreenImage(imageBytes);

                    callback.Invoke(base64Image);
                });
            }

            return Task.CompletedTask;
        }*/

    }
}
