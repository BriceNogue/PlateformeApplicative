using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Presentation.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        private string _email {  get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        private string _password { get; set; } = string.Empty;

        public LoginViewModel() { }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(_password));
            }
        }
    }
}
