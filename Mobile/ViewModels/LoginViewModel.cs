using Shareds.Modeles;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobile.ViewModels
{
    public class LoginViewModel : UserLoginModele, INotifyPropertyChanged
    {
        public string _Email
        {
            get { return Email; }
            set 
            { 
                _Email = value;
                OnPropertyChanged(nameof(_Email));
            }
        }

        public string _Password
        {
            get { return Password; }
            set
            {
                _Password = value;
                OnPropertyChanged(nameof(_Password));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
