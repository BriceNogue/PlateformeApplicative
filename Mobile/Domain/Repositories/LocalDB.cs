using Mobile.Domain.Entities;
using SQLite;

namespace Mobile.Domain.Repositories
{
    // Bade de données locale SQLite
    public class LocalDB
    {
        private const string DB_NAME = "it_m_db.db3";
        public readonly SQLiteAsyncConnection _connection;

        public LocalDB()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));

            _connection.CreateTableAsync<UserSession>();
        }
    }
}
