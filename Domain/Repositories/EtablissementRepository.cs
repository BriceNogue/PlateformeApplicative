using Domain.Entities;

namespace Domain.Repositories
{
    public class EtablissementRepository
    {
        private readonly DataContext _dataContext;

        public EtablissementRepository()
        {
            _dataContext = new DataContext();
        }

        public List<Etablissement> GetAll()
        {
            return _dataContext.Etablissements.ToList();
        }

        public Etablissement Get(int id)
        {
            return _dataContext.Etablissements.FirstOrDefault(e => e.Id == id)!;
        }

        public void Add(Etablissement e)
        {
            _dataContext.Etablissements.Add(e);
            _dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var etab = Get(id);
            _dataContext.Etablissements.Remove(etab);
            _dataContext.SaveChanges();
        }

        public void Update(Etablissement e)
        {
            _dataContext.Etablissements.Update(e);
            _dataContext.SaveChanges();
        }
    }
}
