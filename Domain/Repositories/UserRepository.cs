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
            try
            {
                return _dataContext.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User Get(int id)
        {
            
            try
            {
                return _dataContext.Users.FirstOrDefault(x => x.Id == id)!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(User user)
        {
            try
            {
                _dataContext.Users.Add(user);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public void Delete(int id)
        {
            try
            {
                var user = Get(id);
                _dataContext.Users.Remove(user);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public void Update(User user)
        {
            try
            {
                _dataContext.Users.Update(user);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }

}
