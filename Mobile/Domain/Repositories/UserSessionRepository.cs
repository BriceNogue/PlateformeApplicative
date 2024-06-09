using Mobile.Domain.Entities;
using SQLite;

namespace Mobile.Domain.Repositories
{
    public class UserSessionRepository
    {
        private SQLiteAsyncConnection _db;

        public UserSessionRepository(){}

        async Task Init()
        {
            if (_db is not null)
                return;

            _db = new SQLiteAsyncConnection(LocalDB.DatabasePath, LocalDB.Flags);
            var result = await _db.CreateTableAsync<UserSession>();

            // Vérifier si la table UserSession existe déjà
            /*var tableInfo = await _db.GetTableInfoAsync("user_session");
            if (tableInfo.Any())
            {
                // Vérifier si le schéma correspond au modèle actuel
                var currentTableColumns = tableInfo.Select(t => t.Name).ToList();
                var modelColumns = typeof(UserSession).GetProperties().Select(p => p.Name).ToList();

                // Si le schéma est différent, supprimer et recréer la table
                if (!currentTableColumns.SequenceEqual(modelColumns))
                {
                    await _db.DropTableAsync<UserSession>();
                    var result = await _db.CreateTableAsync<UserSession>();
                }
            }
            else
            {
                var result = await _db.CreateTableAsync<UserSession>();
            }*/
        
        }

        public async Task<UserSession> GetUserSession()
        {
            await Init();
            return await _db.Table<UserSession>().FirstOrDefaultAsync();
        }

        public async Task CreateUserSession(UserSession userSession)
        {
            await Init();
            await _db.InsertAsync(userSession);
        }

        public async Task UpdateUserSession(UserSession userSession)
        {
            await Init();
            await _db.UpdateAsync(userSession);
        }

        public async Task DeleteUserSession()
        {
            await Init();
            await _db.DeleteAllAsync<UserSession>();
        }
    }
}
