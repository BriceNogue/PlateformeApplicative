using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SalleService
    {
        private readonly SalleRepository _repository;

        public SalleService() 
        {
            _repository = new SalleRepository();
        }

        public List<Salle> GetAll() 
        { 
            return _repository.GetAll(); 
        }

        public Salle Get(int id) 
        {
            return _repository.Get(id);
        }

        public bool Delete(int id)
        {
            var salle = _repository.Get(id);
            if(salle is null)
                return false;
            _repository.Delete(id);
            return true;
        }
    }
}
