using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;

namespace Desktop.Hubs
{
    public class InstructionsHub : Hub
    {
        private HubConnection? HC;
        private readonly string _hubURL = "https://localhost:7281/instructionshub"; // Adresse du backend

        public string liveCicle = string.Empty;

        public InstructionsHub(){}

        public async Task ConnectToHub(Window window)
        {
            HC = new HubConnectionBuilder()
                .WithUrl(_hubURL)
                .WithAutomaticReconnect()
                .Build();

            HC.On<int, string>("ReceiveInstruction", (postId, instruction) =>
            {
                window.Dispatcher.Invoke(() =>
                {
                    switch (instruction)
                    {
                        case "FOOLSCREEN":                           
                            window.WindowState = WindowState.Maximized;
                            window.Show();
                            break;
                        case "NORMALSCREEN":                           
                            window.WindowState = WindowState.Normal;
                            window.Show();
                            break;
                        case "MINIMIZESCREEN":
                            window.WindowState = WindowState.Minimized;
                            break;
                        case "HIDDEWINDOW":
                            window.Visibility = Visibility.Collapsed;
                            break;
                        case "SHOWWINDOW":
                            window.Visibility = Visibility.Visible;
                            break;
                        case "RESTART":
                            System.Windows.Forms.Application.Restart();
                            System.Windows.Application.Current.Shutdown();
                            break;
                        default:    
                            break;
                    }
                });
            });

            #region /// Evenements
            HC.Reconnecting += (sender) =>
            {
                window.Dispatcher.Invoke(() =>
                {
                    liveCicle = "En attente de reconnexion...";
                });

                return Task.CompletedTask;
            };

            HC.Reconnected += (sender) =>
            {
                window.Dispatcher.Invoke(() =>
                {
                    liveCicle = "Reconnexion";
                });

                return Task.CompletedTask;
            };

            HC.Closed += (sender) =>
            {
                window.Dispatcher.Invoke(() =>
                {
                    liveCicle = "Connexion Fermé";
                });

                return Task.CompletedTask;
            };
            #endregion

            try
            {
                await Task.Delay(10000);
                await HC.StartAsync();
                liveCicle = "Connection Started";
            }
            catch (Exception ex)
            {
                liveCicle = ex.Message.ToString();
            }
        }

        // pour ajouter le poste au groupe qui reçoit les instructions
        public async Task JoinPostGroup(int postId)
        {
            if (HC is not null)
            {
                await HC.InvokeAsync("JoinPostGroup", postId);
            }
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
