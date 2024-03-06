using Desktop.Presentation.Views;
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

        public MainWindow()
        {
            InitializeComponent();

            LoadPagesGrid.Navigate(new PostIndexPage());

        }

        private void LoadIndexPage(object sender, RoutedEventArgs e)
        {
            LoadPagesGrid.Navigate(new PostIndexPage());
        }

        private void LoadCreatePage(object sender, RoutedEventArgs e)
        {
            LoadPagesGrid.Navigate(new CreatePostPage());
        }

        private void LoadInfoPage(object sender, RoutedEventArgs e)
        {
            LoadPagesGrid.Navigate(new DeviceInfoPage());
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}