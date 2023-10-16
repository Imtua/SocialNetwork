using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Interfaces;

namespace SocialNetwork.DAL.Repositories
{
    public class FriendRepository : BaseRepository, IFriendRepository
    {
        public int Create(FriendEntity friendEntity)
        {
            return Execute(@"INSERT INTO friends (user_id, friend_id)
                            VALUES(:user_id, :friend_id)", friendEntity);
        }

        public int Delete(int id)
        {
            return Execute(@"DELETE FROM friend WHERE id = :id_t", new { id_t = id });
        }

        public IEnumerable<FriendEntity> FindAllByUserId(int userId)
        {
            return Query<FriendEntity>(@"SELECT * FROM friends WHERE user_id = :user_id_t", new { user_id_t = userId });
        }
    }
}
