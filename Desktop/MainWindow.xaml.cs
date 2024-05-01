using Desktop.Hubs;
using Desktop.Presentation.Views;
using Desktop.Services;
using MaterialDesignThemes.Wpf;
using Shareds.Modeles;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Page currentPage = default!;
        private PosteService posteService;
        private UserService userService;

        private PosteModele posteM;

        private NotificationService notificationS;

        private InstructionsHub instsHub;

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper(); // Pour la gestion des themes

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        Login loginW = default!;

        public MainWindow()
        {
            InitializeComponent();

            notificationS = new NotificationService(this);
            notificationS.LoadNotificationIcon();

            userService = new UserService();
            posteService = new PosteService();

            posteM = new PosteModele();

            timer.Tick += new EventHandler(OnSetDateAndTime!);
            timer.Interval = new TimeSpan(0, 0, 0);
            timer.Start();

            instsHub = new InstructionsHub();
            _ = instsHub.ConnectToHub(this);

            _ = GetPost();

            _ = JoinGroup();

            LoadPagesGrid.Navigate(new PostIndexPage());

            SetBtnConnectText();
        }

        // Pour pouvoir deplacer la fenetre avec la souris
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private async Task GetPost()
        {
            PosteModele poste = await posteService.GetOne();
            if (poste is null)
            {
                txt_footer.Text = "Poste inconnu..";
            }
            {
                this.posteM = poste!;
                txt_footer.Text = "IP : " + poste?.AdresseIP;
            }
        }

        // Se connecter au group signalR pour la reception des instruction par id poste
        private async Task JoinGroup()
        {
            if (posteM != null)
                await Task.Delay(10000);
                await instsHub.JoinPostGroup(posteM!.Id);
        }

        private void LoadIndexPage(object sender, RoutedEventArgs e)
        {
            LoadPagesGrid.Navigate(new PostIndexPage());
        }

        private void OnBtnConnectClick(object sender, RoutedEventArgs e)
        {
            // btn_connect.Content = "Se Connecter";
            if (UserService.userSession is null)
            {
                this.loginW = new Login();
                this.loginW.Closed += HandleOnLoginWindowClosed!;
                this.loginW.ShowDialog();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment vous déconnecter ?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    userService.LogOut();
                    SetBtnConnectText();
                    //LoadPagesGrid.Navigate(new PostIndexPage());
                }
            }
        }

        public void HandleOnLoginWindowClosed(object sender, EventArgs e)
        {
            SetBtnConnectText();
        }

        // Definit le texte sur le boutton de connexion/déconnexion
        private void SetBtnConnectText()
        {
            if (UserService.userSession is null)
            {
                btn_connect.Content = "Se connecter";
                btn_connect.ToolTip = "Se connecter";
            }
            else
            {
                btn_connect.Content = "Se déconnecter";
                btn_connect.ToolTip = "Se déconnecter";
            }
        }

        private void LoadLoginView(object sender, RoutedEventArgs e)
        {
            loginW = new Login();
            loginW.ShowDialog();
        }

        private void LoadCreatePage(object sender, RoutedEventArgs e)
        {
            bool isAuthorized = CheckUserAuthorization();

            if (isAuthorized)
            {
                // bool isPostExist = await CheckExistPost();
                // if (!isPostExist){}

                LoadPagesGrid.Navigate(new CreatePostPage(this));
            }
        }

        private void LoadInfoPage(object sender, RoutedEventArgs e)
        {
            LoadPagesGrid.Navigate(new DeviceInfoPage());
        }

        // Vérifie si l'utilisateur est autorisé à accéder à la page
        public bool CheckUserAuthorization()
        {
            if (UserService.userSession is null)
            {
                MessageBoxResult result = MessageBox.Show("Vous n'etes pas connecté", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
            {
                string[] authorizedRoles = ["SuperAdmin", "Admin"];
                bool hasRole = authorizedRoles.Any(r => r == UserService.userSession!.Role);

                if (!hasRole)
                {
                    MessageBoxResult result = MessageBox.Show("Vous n'etes pas autorisé à accéder à cette page.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private async Task<bool> CheckExistPost()
        {
            var res = await posteService.GetOne();
            if (res == null)
            {
                return false;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Se Poste a deja ete cree.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }   
        }

        public void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ToggleWindowStates(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void OnSetDateAndTime(object sender, EventArgs e)
        {
            txt_date.Text = DateTime.Now.ToLongDateString();
            txt_time.Text = DateTime.Now.ToLongTimeString();
        }  
    }
}