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

        public List<Utilisateur> GetAll()
        {
            try
            {
                return _dataContext.Utilisateurs.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Utilisateur Get(int id)
        {
            
            try
            {
                return _dataContext.Utilisateurs.FirstOrDefault(x => x.Id == id)!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(Utilisateur user)
        {
            try
            {
                _dataContext.Utilisateurs.Add(user);
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
                _dataContext.Utilisateurs.Remove(user);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public void Update(Utilisateur user)
        {
            try
            {
                _dataContext.Utilisateurs.Update(user);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }

}
