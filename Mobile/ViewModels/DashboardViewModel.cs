using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.ViewModels
{
    class DashboardViewModel : INotifyPropertyChanged
    {
        private bool isLabelVisible;
        private bool isRoomsVisible;
        private bool isPostsVisible;
        private bool isUsersVisible;

        private bool isContentVisible;

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

        public bool IsRoomsVisible
        {
            get { return isRoomsVisible; }
            set
            {
                if (isRoomsVisible != value)
                {
                    isRoomsVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsPostsVisible
        {
            get { return isPostsVisible; }
            set
            {
                if (isRoomsVisible != value)
                {
                    isRoomsVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsUsersVisible
        {
            get { return isUsersVisible; }
            set
            {
                if (isUsersVisible != value)
                {
                    isUsersVisible = value;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
