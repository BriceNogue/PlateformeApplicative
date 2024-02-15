using Desktop.Presentation.Views;
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

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}