using Desktop.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace Desktop.Hubs
{
    public class InstructionsHub : Hub
    {
        private HubConnection? HC;
        private readonly string _hubURL = "https://localhost:7281/instructionshub"; // Adresse du backend

        public string liveCicle = string.Empty;

        private DeviceManagerService? _deviceManagerS;

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
                        case "STARTSCREENCAPTURE":
                            _deviceManagerS = new DeviceManagerService(100, SendScreenCapture);
                            _deviceManagerS.StartCapture();
                            break;
                        case "STOPSCREENCAPTURE":
                            if (_deviceManagerS != null)
                            {
                                _deviceManagerS.StopeCapture();
                            }
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
                    Task.Delay(5000);
                    HC.StartAsync();
                    liveCicle = "Connexion Fermé";
                });

                return Task.CompletedTask;
            };
            #endregion

            try
            {
                await Task.Delay(5000);
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

        private async void SendScreenCapture(Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                var imageBytes = memoryStream.ToArray();
                string[] base64Image = [$"data:image/jpge;base64,{Convert.ToBase64String(imageBytes)}"];

                try
                {
                    if (HC is not null)
                    {
                        await HC.InvokeAsync("SendScreenImage", base64Image);
                    }
                }catch (Exception ex)
                {
                    liveCicle = ex.Message.ToString();
                }
            }
        }
    }
}
