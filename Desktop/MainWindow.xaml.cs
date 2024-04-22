﻿using Desktop.Presentation.Views;
using Desktop.Services;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

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

        private NotificationService notificationS;

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper(); // Pour la gestion des themes

        Login loginW = default!;

        public MainWindow()
        {
            InitializeComponent();

            notificationS = new NotificationService(this);
            notificationS.LoadNotificationIcon();

            userService = new UserService();
            posteService = new PosteService();

            LoadPagesGrid.Navigate(new PostIndexPage());

            SetBtnConnectText();
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

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
    }
}