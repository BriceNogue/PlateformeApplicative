using SQLite;
using Mobile.Domain.Entities;

namespace Mobile.Domain.Repositories
{
    public class ParcSessionRepository
    {
        private SQLiteAsyncConnection _db;

        public ParcSessionRepository() { }

        async Task Init()
        {
            if (_db is not null)
                return;

            _db = new SQLiteAsyncConnection(LocalDB.DatabasePath, LocalDB.Flags);
            var result = await _db.CreateTableAsync<ParcSession>();
        }

        public async Task<ParcSession> Get()
        {
            await Init();
            return await _db.Table<ParcSession>().FirstOrDefaultAsync();
        }

        public async Task Create(ParcSession parcSession)
        {
            await Init();
            await _db.InsertAsync(parcSession);
        }

        public async Task Update(ParcSession parcSession)
        {
            await Init();
            await _db.UpdateAsync(parcSession);
        }

        public async Task Delete(ParcSession parcSession)
        {
            await Init();
            await _db.DeleteAsync(parcSession);
        }
    }
}
