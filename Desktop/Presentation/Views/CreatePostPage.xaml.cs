using Desktop.Infrastructure.Services;
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
        private readonly DeviceInfoService _deviceInfoService;

        public List<TypeModele> TypesList = new List<TypeModele>();
        public List<SalleModele> SallesList = new List<SalleModele>();
        public List<PosteModele> PostesList = new List<PosteModele>();

        public TypeModele typeModele = new TypeModele();

        public CreatePostPage()
        {
            InitializeComponent();

            _posteServiceDekstop = new PosteServiceDekstop();
            _deviceInfoService = new DeviceInfoService();

            GetTypesAndSalles();
            GetDeviceInnfo();
            GetPostes();

            cmb_salles.SelectionChanged += SetDeviceNumber;

        }

        

        public async void GetTypesAndSalles()
        {
            var types = await _posteServiceDekstop.GetTypes();
            var salles = await _posteServiceDekstop.GetSalles();

            if (types != null)
                cmb_types.ItemsSource = types;

            if (salles != null)
                cmb_salles.ItemsSource = salles;
        }

        public void GetDeviceInnfo()
        {
            txt_b_marque.Text = _deviceInfoService.GetComputerManufacturer();
            txt_b_adresseIp.Text = _deviceInfoService.GetIPAddress();
            txt_b_adresseMac.Text = _deviceInfoService.GetMACAddress();
            txt_b_SE.Text = _deviceInfoService.GetOperatingSystem();
        }

        public async void GetPostes()
        {
            var postes = await _posteServiceDekstop.GetPostes();
           PostesList = postes;
        }

        private void SetDeviceNumber(object sender, SelectionChangedEventArgs e)
        {
            // Recuperer et convertir l'element selectionne
            var selectedSalle = cmb_salles.SelectedItem as SalleModele;

            if (selectedSalle != null)
            {
                
                var postesSalle = PostesList.Where(p => p.IdSalle == selectedSalle.Id);

                txt_types.Text = postesSalle.Count().ToString();
            }
        }

    }
}
