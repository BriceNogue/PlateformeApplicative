using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private bool isBorderOneVisible;
        private bool isBorderTwoVisible;

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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
