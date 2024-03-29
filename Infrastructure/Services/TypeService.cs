using Domain.Repositories;
using Domain.Entities;
using Shareds.Modeles;
using static Shareds.Modeles.ResponsesModels;

namespace Infrastructure.Services
{
    public class TypeService(TypeRepository _typeRepository)
    {
        public List<TypeE> GetAll()
        {
            return _typeRepository.GetAll();
        }

        public TypeE Get(int id)
        {
            return _typeRepository.Get(id);
        }

        public async Task<GeneralResponse> Add(TypeModele type)
        {
            var types = GetAll();
            if (type.Id > 0 || types.Any(e => e.Libelle == type.Libelle))
            {
                return new GeneralResponse(false, "Une erreur est survenue.. veuillez réessayer s'il vous plait.");
            }
            else
            {
                var typeE = new TypeE()
                {
                    Libelle = type.Libelle,
                    Description = type.Description,
                    Objet = type.Objet,
                    Name = type.Libelle
                };

                var res = await _typeRepository.Add(typeE);
                return res;  
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
