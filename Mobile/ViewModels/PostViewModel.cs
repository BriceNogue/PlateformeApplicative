using Shareds.Modeles;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobile.ViewModels
{
    class PostViewModel : PosteModele, INotifyPropertyChanged
    {
        private bool isLabelVisible;
        private bool isContentVisible;

        private List<PosteModele> postes;

        public bool IsLabelVisible
        {
            get { return isLabelVisible; }
            set
            {
                if (isLabelVisible != value)
                {
                    isLabelVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsContentVisible
        {
            get { return isContentVisible; }
            set
            {
                if (isContentVisible != value)
                {
                    isContentVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<PosteModele> Postes
        {
            get { return postes; }
            set
            {
                if (postes != value)
                {
                    postes = value;
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
