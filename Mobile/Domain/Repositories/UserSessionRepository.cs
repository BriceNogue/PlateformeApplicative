using Mobile.Domain.Entities;

namespace Mobile.Domain.Repositories
{
    public class UserSessionRepository
    {
        private LocalDB _db;
        public UserSessionRepository()
        {
            _db = new LocalDB();
        }

        public async Task<UserSession> GetUserSession()
        {
            return await _db._connection.Table<UserSession>().FirstOrDefaultAsync();
        }

        public async Task CreateUserSession(UserSession userSession)
        {
            await _db._connection.InsertAsync(userSession);
        }

        public async Task UpdateUserSession(UserSession userSession)
        {
            await _db._connection.UpdateAsync(userSession);
        }

        public async Task DeleteUserSession(UserSession userSession)
        {
            await _db._connection.DeleteAsync(userSession);
        }
    }
}
