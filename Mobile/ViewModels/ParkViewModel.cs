using Shareds.Modeles;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobile.ViewModels
{
    public class ParkViewModel : EtablissementModele, INotifyPropertyChanged
    {
        public string _Nom
        {
            get { return Nom; }
            set
            {
                Nom = value;
                OnPropertyChanged(nameof(Nom));
            }
        }

        public string _Telephone
        {
            get { return Telephone; }
            set
            {
                Telephone = value;
                OnPropertyChanged(nameof(Telephone));
            }
        }

        public string _Email
        {
            get { return Email; }
            set
            {
                Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string _Pays
        {
            get { return Pays; }
            set
            {
                Pays = value;
                OnPropertyChanged(nameof(Pays));
            }
        }

        public string _CodePostal
        {
            get { return CodePostal; }
            set
            {
                CodePostal = value;
                OnPropertyChanged(nameof(CodePostal));
            }
        }

        public string _Ville
        {
            get { return Ville; }
            set
            {
                Ville = value;
                OnPropertyChanged(nameof(Ville));
            }
        }

        public int _NumeroRue
        {
            get { return NumeroRue; }
            set
            {
                NumeroRue = value;
                OnPropertyChanged(nameof(NumeroRue));
            }
        }

        public string _LibelleRue
        {
            get { return LibelleRue; }
            set
            {
                LibelleRue = value;
                OnPropertyChanged(nameof(LibelleRue));
            }
        }

        public DateTime _DateCreation
        {
            get { return DateCreation; }
            set
            {
                DateCreation = value;
                OnPropertyChanged(nameof(DateCreation));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
