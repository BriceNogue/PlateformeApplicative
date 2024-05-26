using Shareds.Modeles;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobile.ViewModels
{
    public class LoginViewModel : UserLoginModele, INotifyPropertyChanged
    {
        private bool isBorderOneVisible;
        private bool isBorderTwoVisible;
        private List<EtablissementModele> parks;

        public string _Email
        {
            get { return Email; }
            set 
            { 
                Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string _Password
        {
            get { return Password; }
            set
            {
                Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public bool IsBorderOneVisible
        {
            get { return isBorderOneVisible; }
            set
            {
                if (isBorderOneVisible != value)
                {
                    isBorderOneVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsBorderTwoVisible
        {
            get { return isBorderTwoVisible; }
            set
            {
                if (isBorderTwoVisible != value)
                {
                    isBorderTwoVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<EtablissementModele> Parks
        {
            get { return parks; }
            set
            {
                if (parks != value)
                {
                    parks = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
