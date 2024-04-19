using Desktop.Services;
using Domain.Entities;
using Shareds.Modeles;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Views.LoginPages
{
    /// <summary>
    /// Logique d'interaction pour SelectParcPage.xaml
    /// </summary>
    public partial class SelectParcPage : Page
    {
        private ParcService parcService;
        private UserService userService;
        List<EtablissementModele> etabs; 
        Login loginW;

        public SelectParcPage(List<EtablissementModele> etabs, Login loginW)
        {
            InitializeComponent();

            this.parcService = new ParcService();
            this.userService = new UserService();

            this.etabs = etabs;
            this.loginW = loginW;

            cmb_parcs.ItemsSource = etabs;

            txt_user_name.Text = UserService.userSession!.Name;
        }

        private void ConnectToParc(object sender, RoutedEventArgs e)
        {
            if (cmb_parcs.SelectedItem is null)
            {
                MessageBoxResult result = MessageBox.Show("Veuillez sélectionner un établissement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int parcId = ((EtablissementModele)cmb_parcs.SelectedItem).Id;

                SetSelectedParcSession(parcId);
            }

        }

        private void SetSelectedParcSession(int parcId)
        {
            if (parcId != 0)
            {
                parcService!.SetParcSession(parcId);
            }

            if (ParcService.parcSession is not null)
            {
                userService.SetUserSession();

                MainWindow mainWindow = new MainWindow();
                loginW.Close();
                mainWindow.Show();
            }
        }
    }
}
