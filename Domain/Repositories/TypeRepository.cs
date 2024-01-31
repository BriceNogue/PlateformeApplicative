using Domain.Entities;

namespace Domain.Repositories
{
    public class TypeRepository
    {
        private readonly DataContext _dataContext;

        public TypeRepository() 
        {
            _dataContext = new DataContext();
        }

        public List<TypeE> GetAll() 
        {
            return _dataContext.Types.ToList();
        }

        public TypeE Get(int id)
        {
            return _dataContext.Types.FirstOrDefault(t => t.Id == id)!;
        }

        public void Add(TypeE type)
        {
            _dataContext.Types.Add(type);
            _dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var type = Get(id);
            _dataContext.Remove(type);
            _dataContext.SaveChanges();
        }

        public void Update(TypeE type)
        {
            _dataContext.Update(type);
            _dataContext.SaveChanges();
        }
    }
}
