using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using static Shareds.Modeles.ResponsesModels;

namespace Domain.Repositories
{
    public class TypeRepository(RoleManager<TypeE> roleManager, DataContext _dataContext)
    {
        public List<TypeE> GetAll() 
        {
            return _dataContext.Types.ToList();

        }

        public TypeE Get(int id)
        {
            return _dataContext.Types.FirstOrDefault(t => t.Id == id)!;
        }

        public async Task<GeneralResponse> Add(TypeE type)
        {
            try
            {
                var createType = await roleManager.CreateAsync(type);
                if (!createType.Succeeded) return new GeneralResponse(false, "Une erreur est survenue.. veuillez réessayer s'il vous plait.");

                else
                {
                    return new GeneralResponse(true, "Compte crée avec succès");
                }

                //_dataContext.Types.Add(type);
                //_dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponse(false, "Une erreur est survenue.. veuillez réessayer s'il vous plait.");
            }
        }

        public void Delete(int id)
        {
            try
            {
                var type = Get(id);
                _dataContext.Types.Remove(type);
                _dataContext.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Update(TypeE type)
        {
            try
            {
                _dataContext.Types.Update(type);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
