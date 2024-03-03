using Shared.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PosteServiceDekstop = Desktop.Infrastructure.Services.PosteService;

namespace Desktop.Presentation.Views
{
    /// <summary>
    /// Logique d'interaction pour PostIndexPage.xaml
    /// </summary>
    public partial class PostIndexPage : Page
    {
        PosteModele posteModele = new PosteModele();

        private PosteServiceDekstop _posteServiceD;

        public PostIndexPage()
        {
            InitializeComponent();

            _posteServiceD = new PosteServiceDekstop();

            GetPost();
        }

        private async void GetPost()
        {
            PosteModele poste = await _posteServiceD.GetOne();
            if (poste is null)
            {       
                txt_post.Text = "Nonnnnnnn";
            }
            {
                this.posteModele = poste!;
                txt_post.Text = poste!.Id.ToString();
            }
        }
    }
}
