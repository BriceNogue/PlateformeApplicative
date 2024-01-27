
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    public class Etablissement
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Pays { get; set; } = string.Empty;
    }
}
