using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Primitives;
using Shareds.Modeles;

namespace Shareds.Hubs
{
    public class InstructionsHub : Hub
    {
        private HubConnection? HC;
        private readonly string _hubURL = "https://localhost:7281/instructionshub";
        private List<string> instructions;

        public bool IsConnected => HC?.State == HubConnectionState.Connected;

        private string screenImageSource = string.Empty;

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

            HC.On<int, string>("ReceiveInstruction", (postId, instruction) =>
            {
                var formattedMessage = $"{postId}: {instruction}";
                instructions.Add(formattedMessage);
            });

            await HC.StartAsync();
        }

        // pour ajouter le poste au groupe qui reçoit les instructions
        public async Task JoinPostGroup(int postId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, postId.ToString());
        }

        // Definit le client pour l'envoi de l'instruction
        public Task ToSend(int postId, string instruction)
        {
            return Clients.Group(postId.ToString()).SendAsync("ReceiveInstruction", postId, instruction);
        }

        public async Task SendInstruction(int postId, string instruction)
        {
            if (HC is not null)
            {
                await HC.SendAsync("ToSend", postId, instruction);
            }
        }

        public List<string> GetInstructions()
        {
            return instructions;
        }

        public async ValueTask DisposeAsync()
        {
            if (HC is not null)
            {
                await HC.DisposeAsync();
            }
        }

        // Methode appellée depuis WPF pour envoyer les images
        public Task SendScreenImage(byte[] imageBytes)
        {
            return Clients.All.SendAsync("ReceiveScreenImage", imageBytes);
        }

        private string SetScreenImage(string imageBytes)
        {
            //var base64Image = $"data:image/jpge;base64,{Convert.ToBase64String(imageBytes)}";
            return imageBytes;
        }

        public string GetScreenImage()
        {
            return screenImageSource;
        }

    }
}
