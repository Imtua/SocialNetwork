using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Interfaces;

namespace SocialNetwork.DAL.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public int Create(MessageEntity messageEntity)
        {
            return Execute(@"INSERT INTO messages (content, sender_id, recipient_id)
                            VALUES (:content, :sender_id, :recipient_id)", messageEntity);
        }

        public int DeleteById(int messageId)
        {
            return Execute(@"DELETE FROM messages WHERE id = :id_t", new { id_t = messageId });
        }

        public IEnumerable<MessageEntity> FindByRecipientId(int recipientId)
        {
            return Query<MessageEntity>(@"SELECT * FROM messages WHERE recipient_id = :recipient_id_t", new { recipient_id_t = recipientId});
        }

        public IEnumerable<MessageEntity> FindBySenderId(int senderId)
        {
            return Query<MessageEntity>(@"SELECT * FROM messages WHERE sender_id = :sender_id_t", new { sender_id_t = senderId });
        }
    }
}
