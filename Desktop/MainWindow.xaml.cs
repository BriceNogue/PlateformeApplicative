using Desktop.Presentation.Views;
using Desktop.Services;
using Shareds.Modeles;
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

        public MainWindow()
        {
            InitializeComponent();

            posteService = new PosteService();

            LoadPagesGrid.Navigate(new PostIndexPage());

        }

        private void LoadIndexPage(object sender, RoutedEventArgs e)
        {
            LoadPagesGrid.Navigate(new PostIndexPage());
        }

        private async void LoadCreatePage(object sender, RoutedEventArgs e)
        {
            bool isAuthorized = CheckUserAuthorization();

            if (isAuthorized)
            {
                bool isPostExist = await CheckExistPost();

                if (!isPostExist)
                {
                    LoadPagesGrid.Navigate(new CreatePostPage(this));
                }
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

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}