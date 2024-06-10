using Shareds.Modeles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.ViewModels
{
    class RoomViewModel : SalleModele, INotifyPropertyChanged
    {
        private List<SalleModele> rooms;

        public int _Numero
        {
            get { return Numero; }
            set
            {
                if (Numero != value)
                {
                    Numero = value;
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
