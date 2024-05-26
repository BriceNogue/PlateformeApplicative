using Mobile.Domain.Entities;
using SQLite;

namespace Mobile.Domain.Repositories
{
    // Bade de données locale SQLite
    public static class LocalDB
    {
        private const string DB_NAME = "it_m_db.db3";

        public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
    }
}
