using Microsoft.AspNetCore.SignalR.Client;

namespace Web.Hubs
{
    public class InstructionsHub
    {
        private HubConnection? HC;
        private readonly string _hubURL = "https://localhost:7281/instructionshub";
        //private List<string> instructions;

        public bool IsConnected => HC?.State == HubConnectionState.Connected;

        public InstructionsHub()
        {
            //instructions = new List<string>();
        }

        public async Task ConnectToHub()
        {
            HC = new HubConnectionBuilder()
                .WithUrl(_hubURL)
                .WithAutomaticReconnect()
                .Build();

            /*HC.On<string>("ReceiveScreenImage", async (base64Image) =>
            {

            });*/

            /*HC.On<int, string>("ReceiveInstruction", (postId, instruction) =>
            {
                var formattedMessage = $"{postId}: {instruction}";
                instructions.Add(formattedMessage);
            });*/

            await HC.StartAsync();
        }

        public async Task SendInstruction(int postId, string instruction)
        {
            try
            {
                if (HC is not null)
                {
                    await HC.InvokeAsync("SendInstruction", postId, instruction);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (HC is not null)
            {
                await HC.InvokeAsync("DisposeAsync");
            }
        }

        // 'Func' Equivalent asynchrone de 'Action' 
        public async Task<string> GetScreenImage(Action<string> callback)
        {
            if (HC != null)
            {
                await Task.Run(() =>
                {
                    HC.On<string>("ReceiveScreenImage", async (base64Image) =>
                    {
                         callback.Invoke(base64Image);
                    });
                });
            }

            return "";
        }
    }
}
