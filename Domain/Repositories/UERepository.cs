using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class UERepository
    {
        private readonly DataContext _dataContext;

        public UERepository()
        {
            _dataContext = new DataContext();
        }

        public List<UtilisateurEtablissement> GetAll()
        {
            return _dataContext.UtilisateurEtablissements.ToList();

        }

        public UtilisateurEtablissement Get(int id)
        {
            return _dataContext.UtilisateurEtablissements.FirstOrDefault(t => t.Id == id)!;
        }

        public void Add(UtilisateurEtablissement ue)
        {
            try
            {
                _dataContext.UtilisateurEtablissements.Add(ue);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var ue = Get(id);
                _dataContext.UtilisateurEtablissements.Remove(ue);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Update(UtilisateurEtablissement ue)
        {
            try
            {
                _dataContext.UtilisateurEtablissements.Update(ue);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
