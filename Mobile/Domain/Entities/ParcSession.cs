using SQLite;

namespace Mobile.Domain.Entities
{
    [Table("user_session")]
    public class ParcSession
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("parc_id")]
        public int ParcId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public ParcSession() { }
    }
}
