using SQLite;

namespace Mobile.Domain.Entities
{
    [Table("user_session")]
    public class UserSession
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("token")]
        public string Token { get; set; }

        public UserSession() { }
    }
}
