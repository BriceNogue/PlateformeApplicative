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

        public async Task DeleteUserSession(UserSession userSession)
        {
            await Init();
            await _db.DeleteAsync(userSession);
        }
    }
}
