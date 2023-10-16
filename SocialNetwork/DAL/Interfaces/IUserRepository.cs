using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.Interfaces
{
    public interface IUserRepository
    {
        public int Create(UserEntity userEntity);
        public UserEntity FindByEmail(string email);
        public IEnumerable<UserEntity> FindAll();
        public UserEntity FindById(int id);
        public int Update(UserEntity userEntity);
        public int DeleteById(int id);
    }
}
