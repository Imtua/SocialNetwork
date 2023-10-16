using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Interfaces;

namespace SocialNetwork.DAL.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public int Create(UserEntity userEntity)
        {
            return Execute(@"INSERT INTO users (firstname, lastname, password, email, photo, favorite_movie, favorite_book)
                        VALUES(:first_name, :last_name, :password, :email, :photo, :favorite_movie, :favorite_book)", userEntity);
        }

        public int DeleteById(int id)
        {
            return Execute(@"DELETE FROM users WHERE id = :id_t", new { id_t = id });
        }

        public IEnumerable<UserEntity> FindAll()
        {
            return Query<UserEntity>(@"SELECT * FROM users");
        }

        public UserEntity FindByEmail(string email)
        {
            return QueryFirstOrDefault<UserEntity>(@"SELECT * FROM users WHERE email = :email_t", new { email_t = email });
        }

        public UserEntity FindById(int id)
        {
            return QueryFirstOrDefault<UserEntity>(@"SELECT * FROM users WHERE id = :id_t", new { id_t = id });
        }

        public int Update(UserEntity userEntity)
        {
            return Execute(@"UPDATE users SET firstname = :first_name, lastname = :last_name, email = :email,
                        password = :password, photo = :photo, favorite_movie = :favorite_movie, favorite_book = :favorite_book
                        WHERE id = :Id", userEntity);
        }
    }
}
