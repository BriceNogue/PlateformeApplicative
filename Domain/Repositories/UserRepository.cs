using Domain.Entities;

namespace Domain.Repositories
{
    public class UserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository() 
        { 
            _dataContext = new DataContext();
        }

        public List<User> GetAll()
        {
            return _dataContext.Users.ToList();
        }

        public User Get(int id)
        {
            return _dataContext.Users.FirstOrDefault(x => x.Id == id)!;
        }

        public void Add(User user)
        {
            _dataContext.Users.Add(user);
            _dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = Get(id);
            _dataContext.Users.Remove(user);
            _dataContext.SaveChanges();
        }

        public void Update(User user)
        {
            _dataContext.Update(user);
            _dataContext.SaveChanges();
        }
    }

}
