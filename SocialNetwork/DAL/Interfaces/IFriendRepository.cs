using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.Interfaces
{
    public interface IFriendRepository
    {
        public int Create(FriendEntity friendEntity);
        public IEnumerable<FriendEntity> FindAllByUserId(int userId);
        public int Delete(int id);
    }
}
