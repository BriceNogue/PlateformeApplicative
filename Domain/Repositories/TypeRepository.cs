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
            try
            {
                _dataContext.Types.Add(type);
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
