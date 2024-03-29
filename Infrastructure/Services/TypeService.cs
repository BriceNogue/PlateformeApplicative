﻿using Domain.Repositories;
using Domain.Entities;
using Shareds.Modeles;

namespace Infrastructure.Services
{
    public class TypeService
    {
        private readonly TypeRepository _typeRepository;

        public TypeService() 
        {
            _typeRepository = new TypeRepository();
        }

        public List<TypeE> GetAll()
        {
            return _typeRepository.GetAll();
        }

        public TypeE Get(int id)
        {
            return _typeRepository.Get(id);
        }

        public bool Add(TypeModele type)
        {
            var types = GetAll();
            if (type.Id > 0 || types.Any(e => e.Libelle == type.Libelle))
            {
                return false;
            }
            else
            {
                var typeE = new TypeE()
                {
                    //Libelle = type.Libelle,
                    Description = type.Description,
                    Objet = type.Objet,
                };

                _typeRepository.Add(typeE);
                return true;  
            }
        }

        public bool Delete(int id)
        {
            var existingType = _typeRepository.Get(id);
            if (existingType is null)
                return false;
            _typeRepository.Delete(id);
                return true;
        }

        public bool Update(TypeModele type)
        {  
            var oldType = _typeRepository.Get(type.Id);
            if (oldType is null)
            {
                return false;
            }
            else
            {
                //oldType.Libelle = type.Libelle;
                oldType.Description = type.Description;
                oldType.Objet = type.Objet;

                _typeRepository.Update(oldType);
                return true;
            }
        }
    }
}
