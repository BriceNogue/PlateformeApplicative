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
        }

        private async void ConnectToParc(object sender, RoutedEventArgs e)
        {
            if (cmb_parcs.SelectedItem is null)
            {
                MessageBoxResult result = MessageBox.Show("Veuillez sélectionner un établissement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int parcId = ((EtablissementModele)cmb_parcs.SelectedItem).Id;

                bool isSeted = await SetSelectedParcSession(parcId);

                if (isSeted)
                {
                    MainWindow mainWindow = new MainWindow();
                    loginW.Close();
                    mainWindow.Show();
                }                
            }
        }

        private async Task<bool> SetSelectedParcSession(int parcId)
        {
            try
            {
                if (parcId != 0)
                {
                    var res = await parcService!.SetParcSession(parcId);

                    if (res)
                    {
                        userService.SetUserSession();
                    }
                    return res;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
