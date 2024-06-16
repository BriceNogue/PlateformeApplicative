using Shareds.Modeles;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobile.ViewModels
{
    class PostViewModel : PosteModele, INotifyPropertyChanged
    {
        private bool isLabelVisible;
        private bool isContentVisible;
        private int roomNumber;

        private List<PosteModele> posts;
        private List<SalleModele> rooms;

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

        public int RoomNumber
        {
            get { return roomNumber; }
            set
            {
                if (roomNumber != value)
                {
                    roomNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<PosteModele> Posts
        {
            get { return posts; }
            set
            {
                if (posts != value)
                {
                    posts = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<SalleModele> Rooms
        {
            get { return rooms; }
            set
            {
                if (rooms != value)
                {
                    rooms = value;
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
