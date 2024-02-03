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
            try
            {
                return _dataContext.Etablissements.ToList();
            }
            catch(Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }
        public Etablissement Get(int id)
        {
            try
            {
                return _dataContext.Etablissements.FirstOrDefault(e => e.Id == id)!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(Etablissement e)
        {
            try
            {

                _dataContext.Etablissements.Add(e);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var etab = Get(id);
                _dataContext.Etablissements.Remove(etab);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Etablissement e)
        {
            try
            {
                _dataContext.Etablissements.Update(e);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
