using Domain.Entities;

namespace Domain.Repositories
{
    public class SalleRepository
    {
        private readonly DataContext _dataContext;

        public SalleRepository() 
        {
            _dataContext = new DataContext();
        }

        public List<Salle> GetAll()
        {
            return _dataContext.Salles.ToList();
        }

        public Salle Get(int id)
        {
            return _dataContext.Salles.FirstOrDefault(s => s.Id == id)!;
        }

        public void Delete(int id)
        {
            var salle = Get(id);
            _dataContext.Salles.Remove(salle);
            _dataContext.SaveChanges();
        }

        public void Update(Salle salle)
        {
            _dataContext.Salles.Update(salle);
            _dataContext.SaveChanges();
        }
    }
}
