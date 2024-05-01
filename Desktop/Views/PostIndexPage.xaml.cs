using Shareds.Modeles;
using System.Windows;
using System.Windows.Controls;
using Desktop.Services;

namespace Desktop.Presentation.Views
{
    /// <summary>
    /// Logique d'interaction pour PostIndexPage.xaml
    /// </summary>
    public partial class PostIndexPage : Page
    {
        PosteModele posteModele = new PosteModele();

        private PosteService posteServiceD;
        private DeviceManagerService deviceMS;

        public PostIndexPage()
        {
            InitializeComponent();

            posteServiceD = new PosteService();
            deviceMS = new DeviceManagerService();

            _ = GetPost();
        }

        private async Task GetPost()
        {
            PosteModele poste = await posteServiceD.GetOne();
            if (poste is null)
            {       
                txt_post.Text = "Poste inconnu..";
            }
            {
                this.posteModele = poste!;
                txt_post.Text = "Poste : " + poste?.Numero.ToString();
            }
        }
    }
}
