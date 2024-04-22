using Desktop.Services;
using Shareds.Modeles;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PosteServiceDekstop = Desktop.Services.PosteService;
using UserService = Desktop.Services.UserService;

namespace Desktop.Presentation.Views
{
    /// <summary>
    /// Logique d'interaction pour CreatePostPage.xaml
    /// </summary>
    public partial class CreatePostPage : Page
    {
        private readonly PosteServiceDekstop _posteServiceDekstop;
        private readonly DeviceInfoService _deviceInfoService;
        private readonly TypeService _typeService;
        private readonly SalleService _salleService;

        public List<TypeModele> TypesList = new List<TypeModele>();
        public List<SalleModele> SallesList = new List<SalleModele>();
        public List<PosteModele> PostesList = new List<PosteModele>();

        public TypeModele typeModele = new TypeModele();

        int salleCapacity = 0;
        int dispo = 0;
        int idSalle = 0;
        bool isNumberPostValid = false;

        MainWindow mainW; // Pour la fonction de retour

        public CreatePostPage(MainWindow mainW)
        {
            InitializeComponent();
            
            this.mainW = mainW;

            //CheckUserAuthorization();

            _posteServiceDekstop = new PosteServiceDekstop();
            _deviceInfoService = new DeviceInfoService();
            _typeService = new TypeService();
            _salleService = new SalleService();

            GetTypesAndSalles();
            GetDeviceInnfo();
            GetParcPostes();

            cmb_salles.SelectionChanged += HandleSelectedSalle;
            txt_num_post.TextChanged += OnSetPosteNumber;

        }
        

        public async void GetTypesAndSalles()
        {
            var types = await _typeService.GetAll();
            if (types != null)
                cmb_types.ItemsSource = types.Where(t => t.Objet == "Poste").ToList();

            if (ParcService.parcSession is not null)
            {
                var parcId = ParcService.parcSession.Id;
                var salles = await _salleService.GetAllByParc(parcId);

                if (salles != null)
                    cmb_salles.ItemsSource = salles;
            }   
        }

        public void GetDeviceInnfo()
        {
            txt_b_marque.Text = _deviceInfoService.GetComputerManufacturer();
            txt_b_adresseIp.Text = _deviceInfoService.GetIPAddress();
            txt_b_adresseMac.Text = _deviceInfoService.GetMACAddress();
            txt_b_SE.Text = _deviceInfoService.GetOperatingSystem();
        }

        public async void GetParcPostes()
        {
            try
            {
                int parcId = ParcService.parcSession!.Id;
                var postes = await _posteServiceDekstop.GetAllByParc(parcId);

                if (postes != null)
                {
                    PostesList = postes;
                }
                else
                {
                    PostesList = new List<PosteModele>();
                }
            }
            catch (Exception ex)
            {
                PostesList = null!;
                throw new Exception(ex.Message);
            }
        }

        #region // Manipulation et verification du formulaire

        // Récupère la capacité de la salle selectionné
        private void HandleSelectedSalle(object sender, SelectionChangedEventArgs e)
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

        // Récupère les postes de la salle selectionnée et vérifie leurs numéraux
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

                bool existingPost = postesSalle.Any(p => p.Numero == int.Parse(inputVerify.Value));
                int inputNumber = int.Parse(inputVerify.Value);

                if (inputNumber > this.salleCapacity)
                {
                    txt_error.Text = "Valeur superieur a la capacite de la salle.";
                    isNumberPostValid = false;
                }else if (inputNumber <= 0)
                {
                    txt_error.Text = "Valeur indisponible.";
                    isNumberPostValid = false;
                }
                else if (existingPost)
                {
                    txt_error.Text = "Valeur deja attribuee a un poste.";
                    isNumberPostValid = false;
                }
                else
                {
                    txt_error.Text = "";
                    isNumberPostValid = true;
                }
            }
            else
            {
                txt_error.Text = "Entrez une valeur valide.";
                isNumberPostValid = false;
            }

        }

        //Bloquer la saisie de caractere autre que nombres
        private void OnTextInput(object sender, TextCompositionEventArgs e)
        {
            // Vérifier si le caractère entré est un chiffre
            if (!char.IsDigit(e.Text, e.Text.Length - 1) || e.Text == " ")
            {
                e.Handled = true; // Ignorer la saisie du caractère non autorisé
            }
        }

        #endregion

        private async void SavePost(object sender, EventArgs e)
        {
            PosteModele poste = await _posteServiceDekstop.GetOne();
            if (poste is null)
            {
                int numeroPoste;
                string marque = txt_b_marque.Text;
                string adresseIP = txt_b_adresseIp.Text;
                string adresseMAC = txt_b_adresseMac.Text;
                string se = txt_b_SE.Text;
                double ROM = 0;
                double RAM = 0;
                int idSalle;
                int idType;

                if (cmb_salles.SelectedItem is null || cmb_types.SelectedItem is null || txt_num_post.Text == string.Empty)
                {
                    MessageBoxResult result = MessageBox.Show("Champ manquant.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!isNumberPostValid)
                {
                    MessageBoxResult result = MessageBox.Show("Numéro de poste invalide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    idSalle = ((SalleModele)cmb_salles.SelectedItem).Id;
                    idType = ((TypeModele)cmb_types.SelectedItem).Id;
                    numeroPoste = int.Parse(txt_num_post.Text);

                    PosteModele newPost = new PosteModele();

                    newPost.Numero = numeroPoste;
                    newPost.Marque = marque;
                    newPost.AdresseIP = adresseIP;
                    newPost.AdresseMAC = adresseMAC;
                    newPost.SE = se;
                    newPost.ROM = ROM;
                    newPost.RAM = RAM;
                    newPost.IdSalle = idSalle;
                    newPost.IdType = idType;

                    bool isAdded = await _posteServiceDekstop.Add(newPost);

                    if (isAdded)
                    {
                        MessageBoxResult result = MessageBox.Show("Poste cree avec suces.", $"Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        mainW.LoadPagesGrid.Navigate(new PostIndexPage());
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("un problème est survenu lors de l'enregistrement du poste.", $"Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }               
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Se Poste a deja ete cree.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void GoBackHome(object sender, RoutedEventArgs e)
        {
            mainW.LoadPagesGrid.Navigate(new PostIndexPage());
        }
    }
}
