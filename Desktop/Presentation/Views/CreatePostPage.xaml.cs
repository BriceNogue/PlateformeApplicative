using Desktop.Infrastructure.Services;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Modeles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        int salleCapacity = 0;
        int dispo = 0;

        public CreatePostPage()
        {
            InitializeComponent();

            _posteServiceDekstop = new PosteServiceDekstop();
            _deviceInfoService = new DeviceInfoService();

            GetTypesAndSalles();
            GetDeviceInnfo();
            GetPostes();

            cmb_salles.SelectionChanged += GetSelectedSalleCapacity;
            //txt_num_post.TextChanged += OnSetPosteNumber;

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

        private void GetSelectedSalleCapacity(object sender, SelectionChangedEventArgs e)
        {
            // Recuperer et convertir l'element selectionne
            var selectedSalle = cmb_salles.SelectedItem as SalleModele;

            if (selectedSalle != null)
            {
                this.salleCapacity = selectedSalle.Capacite;
                txt_capacite.Text = "Capacite: "+ this.salleCapacity.ToString();

                var postesSalle = PostesList.Count(p => p.IdSalle == selectedSalle.Id);

                this.dispo = (selectedSalle.Capacite - postesSalle);
                txt_dispo.Text = "Disponible: "+ this.dispo.ToString();
            }
        }


        /*
        private void OnSetPosteNumber(object sender, TextChangedEventArgs e)
        {
            string inputText = txt_num_post.Text;
            
            var inputVerify = Regex.Match(inputText, "[0-9]+");

            if (inputVerify.Success)
            {
                List<PosteModele> postesSalle = [];

                if (cmb_salles.SelectedItem != null)
                {
                    var selectedSalle = cmb_salles.SelectedItem as SalleModele;
                    postesSalle = PostesList.Where(p => p.IdSalle == selectedSalle!.Id).ToList();
                }

                bool existingPost = postesSalle.Any(p => p.LibellePoste == inputVerify.Value);
                int inputNumber = int.Parse(inputVerify.Value);

                if (inputNumber > this.salleCapacity)
                {
                    txt_error.Text = "Valeur superieur a la capacite de la salle.";
                }else if (inputNumber > this.dispo || inputNumber == 0)
                {
                    txt_error.Text = "Valeur indisponible.";
                }
                else if (existingPost)
                {
                    txt_error.Text = "Valeur deja attribuee a un poste.";
                }
                else
                {
                    txt_error.Text = "";
                }
            }
            else
            {
                txt_error.Text = "Entrez une valeur valide.";
            }

        }
        */


        private void OnTextInput(object sender, TextCompositionEventArgs e)
        {
            // Vérifier si le caractère entré est un chiffre
            if (!char.IsDigit(e.Text, e.Text.Length - 1) || e.Text == " ")
            {
                e.Handled = true; // Ignorer la saisie du caractère non autorisé
            }
        }

        private async void SavePost(object sender, EventArgs e)
        {
            PosteModele poste = await _posteServiceDekstop.GetOne();
            if (poste is null)
            {
                string marque = txt_b_marque.Text;
                string adresseIp = txt_b_adresseIp.Text;
                string adresseMac = txt_b_adresseMac.Text;
                string se = txt_b_SE.Text;
                string libelePoste;
                int idSalle;
                int idType;

                if (cmb_salles.SelectedItem is null || cmb_types.SelectedItem is null || txt_num_post.Text == string.Empty)
                {
                    MessageBoxResult result = MessageBox.Show("Champ manquant.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    idSalle = ((SalleModele)cmb_salles.SelectedItem).Id;
                    idType = ((TypeModele)cmb_types.SelectedItem).Id;
                    libelePoste = txt_num_post.Text;

                    PosteModele newPost = new PosteModele();

                    newPost.Marque = marque;
                    newPost.AdresseIp = adresseIp;
                    newPost.AdresseMAC = adresseMac;
                    newPost.SE = se;
                    newPost.IdSalle = idSalle;
                    newPost.IdType = idType;
                    newPost.LibellePoste = libelePoste;

                    MessageBoxResult result = MessageBox.Show("Poste cree avec suces.", $"{newPost.IdSalle + " " + newPost.IdType}", MessageBoxButton.OK, MessageBoxImage.Information);
                }               
            }
            else //if (poste.LibellePoste == string.Empty || poste.IdSalle == 0 || poste.IdType == 0)
            {
                MessageBoxResult result = MessageBox.Show("Se Poste a deja ete cree.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
