using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.DAL.Repositories;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.BLL.Services
{
    public class UserService
    {
        private IUserRepository _repository;

        public UserService()
        {
            _repository = new UserRepository();
        }

        public void Register(UserRegistrationData userRegistrationData)
        {
            #region [Проверка входных параметров]

            if (string.IsNullOrEmpty(userRegistrationData.FirstName))
            {
                throw new ArgumentNullException("Имя при регистрации не может быть пустым полем", nameof(userRegistrationData.FirstName));
            }

            if (string.IsNullOrEmpty(userRegistrationData.LastName))
            {
                throw new ArgumentNullException("Фамилия при регистрации не может быть пустым полем", nameof(userRegistrationData.LastName));
            }

            if (string.IsNullOrEmpty(userRegistrationData.Email))
            {
                throw new ArgumentNullException("Email при регистрации не может быть пустым полем", nameof(userRegistrationData.Email));
            }

            if (string.IsNullOrEmpty(userRegistrationData.Password))
            {
                throw new ArgumentNullException("Пароль при регистрации не может быть пустым полем", nameof(userRegistrationData.Password));
            }

            if (userRegistrationData.Password.Length < 8)
            {
                throw new ArgumentException("Длинна пароля должна быть больше 8 символов", nameof(userRegistrationData.Password));
            }

            if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
            {
                throw new ArgumentException("Некоректный формат Email", nameof(userRegistrationData.Email));
            }

            if (_repository.FindByEmail(userRegistrationData.Email) != null)
            {
                throw new ArgumentException("Пользователь с таким Email уже существует", nameof(userRegistrationData.Email));
            }

            #endregion

            var userEntity = new UserEntity()
            {
                first_name = userRegistrationData.FirstName,
                last_name = userRegistrationData.LastName,
                email = userRegistrationData.Email.ToLower(),
                password = userRegistrationData.Password,
            };

            if (_repository.Create(userEntity) == 0)
            {
                throw new Exception(nameof(userEntity));
            }          
        }

        public User Authenticate(UserAuthenticationData userAuthenticationData)
        {
            var user = _repository.FindByEmail(userAuthenticationData.Email);

            #region [Проверка возвращаемых параметров]

            // Не совсем понял зачем здесь вторая проверка, метод FindByEmail выкидывает Exception при null.

            //if (user == null)
            //{
            //    throw new UserNotFoundException("Пользователь с таким Email не найден.");
            //}

            if (userAuthenticationData.Password != user.password)
            {
                throw new WrongPasswordException("Неверный пароль.");
            }

            #endregion

            return ConstructUserModel(user);
        }

        public UserEntity FindByEmail(string email)
        {
            var user = _repository.FindByEmail(email.ToLower());

            #region [Проверка возвращаемых параметров]

            if (user == null)
            {
                throw new UserNotFoundException("Пользователь с таким Email не найден.");
            }

            #endregion 

            return user;
        }

        public void Update(User user)
        {
            var newUserEntity = new UserEntity()
            {
                Id= user.Id,
                first_name =user.FirstName,
                last_name =user.LastName,
                email = user.Email.ToLower(),
                password = user.Password,
                photo = user.Photo,
                favorite_movie = user.FavoriteMovie,
                favorite_book = user.FavoriteBook,
            };

            if (_repository.Update(newUserEntity) == 0)
            {
                throw new Exception("Ошибка при изменении");
            }
            
        }

        private User ConstructUserModel(UserEntity userEntity)
        {
            return new User(userEntity.Id,
                            userEntity.first_name,
                            userEntity.last_name,
                            userEntity.email,
                            userEntity.password,
                            userEntity.photo,
                            userEntity.favorite_movie,
                            userEntity.favorite_book);
        }

    }
}
