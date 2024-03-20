
using System.ComponentModel.DataAnnotations;

namespace Shareds.Modeles
{
    public class UserLoginModele
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password, ErrorMessage ="1Az#")]
        public string Password { get; set; } = string.Empty;
    }
}
