using Shareds.Modeles;

namespace Web.Services
{
    public interface IPosteService
    {
        Task<List<PosteModele>> GetAll();
    }
}
