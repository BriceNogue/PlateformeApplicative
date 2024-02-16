using Desktop.Presentation.Views;
using Shared.Modeles;
using System.Windows;
using System.Windows.Controls;
using PosteServiceDekstop = Desktop.Infrastructure.Services.PosteService;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PosteServiceDekstop _posteServiceD;

        public PosteModele posteModele = new PosteModele();

        private Page currentPage = default!;

        public MainWindow()
        {
            InitializeComponent();

            _posteServiceD = new PosteServiceDekstop();

            LoadPagesGrid.Navigate(new PostIndexPage());

            GetPost();
        }

        private async void GetPost()
        {
            PosteModele poste = await _posteServiceD.GetOne();

            if (poste != null)
                this.posteModele = poste;
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoadCreatePage(object sender, RoutedEventArgs e)
        {
            LoadPagesGrid.Navigate(new CreatePostPage());
        }

        private void LoadInfoPage(object sender, RoutedEventArgs e)
        {
            LoadPagesGrid.Navigate(new DeviceInfoPage());
        }
    }
}