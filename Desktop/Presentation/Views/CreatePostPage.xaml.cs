using Microsoft.EntityFrameworkCore;
using Shared.Modeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour CreatePostPage.xaml
    /// </summary>
    public partial class CreatePostPage : Page
    {
        private readonly PosteServiceDekstop _posteServiceDekstop;

        public List<TypeModele> TypesList = new List<TypeModele>();
        public TypeModele typeModele = new TypeModele();

        public CreatePostPage()
        {
            InitializeComponent();

            _posteServiceDekstop = new PosteServiceDekstop();

            GetAllTypes();

            cmb_types.ItemsSource = TypesList; // typeof(Colors).GetProperties();

        }

        public async void GetAllTypes()
        {
            var types = await _posteServiceDekstop.GetTypes();
            if (types != null)
                TypesList = types;
                txt_types.Text = TypesList!.Count().ToString();
        }

    }
}
