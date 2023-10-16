using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.Interfaces
{
    public interface IMessageRepository
    {
        public int Create(MessageEntity messageEntity);
        public IEnumerable<MessageEntity> FindBySenderId(int senderId);
        public IEnumerable<MessageEntity> FindByRecipientId(int recipientId);
        public int DeleteById(int messageId);
    }
}
