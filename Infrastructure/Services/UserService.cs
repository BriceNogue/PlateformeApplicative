using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Modeles;

namespace Infrastructure.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        public bool Add(UserModele user)
        {
            if (user.Id > 0)
            {
                return false;
            }
            else
            {
                var newUser = new User()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Password = user.Password,
                    IdType = user.IdType
                };
                return true;
            }
        }

        public bool Delete(int id)
        {
            var existingUser = _userRepository.Get(id);
            if (existingUser is null)
            {
                return false;
            }
            else
            {
                _userRepository.Delete(id);
                return true;
            }
        }

        public bool Update(UserModele user)
        {
            var oldUser = _userRepository.Get(user.Id);
            if (oldUser is null)
            {
                return false;
            }
            else
            {
                oldUser.FirstName = user.FirstName;
                oldUser.LastName = user.LastName;
                oldUser.Email = user.Email;
                oldUser.PhoneNumber = user.PhoneNumber;
                oldUser.Password = user.Password;
                oldUser.IdType = user.IdType;

                _userRepository.Update(oldUser);
                return true;
            }
        }
    }
}
