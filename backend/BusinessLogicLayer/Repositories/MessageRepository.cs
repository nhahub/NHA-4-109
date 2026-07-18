using DataAccessLayer.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(DataAccessLayer.AppContext context) : base(context) { }

        public IEnumerable<Message> GetMessagesBetween(int ownerId, int tenantId)
            => _dbSet.Where(m => m.Owner.UsserId == ownerId
                              && m.tentant.UsserId == tenantId
                              && !m.IsDeleted)
                     .OrderBy(m => m.ReciveDate)
                     .ToList();

        public IEnumerable<Message> GetUnreadMessages(int ownerId, int tenantId)
            => _dbSet.Where(m => m.Owner.UsserId == ownerId
                              && m.tentant.UsserId == tenantId
                              && m.ReadDate == null
                              && !m.IsDeleted)
                     .ToList();

        public void MarkAsRead(int messageId)
        {
            var msg = _dbSet.Find(messageId);
            if (msg != null)
            {
                 msg.ReadDate = DateTime.Now;
                _dbSet.Update(msg);
            }
        }
    }
}
