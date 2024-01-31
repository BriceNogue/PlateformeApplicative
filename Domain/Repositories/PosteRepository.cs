using Domain.Entities;

namespace Domain.Repositories
{
    public class PosteRepository
    {
        private readonly DataContext _dataContext;

        public PosteRepository() 
        {
            _dataContext = new DataContext();
        }

        public List<Poste> GetAll()
        {
            return _dataContext.Postes.ToList();
        }

        public Poste Get(int id)
        {
            return _dataContext.Postes.FirstOrDefault(p => p.Id == id)!;
        }

        public void Add(Poste post)
        {
            _dataContext.Postes.Add(post);
            _dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var poste = Get(id);
            _dataContext?.Postes.Remove(poste);
        }

        public void Update(Poste post)
        {
            _dataContext.Postes.Update(post);
            _dataContext.SaveChanges();
        }
    }
}
