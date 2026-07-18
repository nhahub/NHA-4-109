using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.DTOs;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Tenant,Owner")]
    [Route("api/MessageApis")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }


        [HttpGet("GetMessagesBetween")]
        public IActionResult GetMessagesBetween(int ownerId, int tenantId)
        {
            var messages = _messageRepository.GetMessagesBetween(ownerId, tenantId).Where(m => !m.IsDeleted)
                .Select(m => new SearchMessageDTO
                {
                    MessageID = m.MessageID,
                    Content = m.Content,
                    SenderFlag = m.SenderFlag,
                    ReciveDate = m.ReciveDate,
                    ReadDate = m.ReadDate,
                    OwnerId = m.OwnerUsserId,
                    TentantId = m.tentantUsserId
                });

            if (!messages.Any())
                return NotFound("No messages found.");

            return Ok(messages);
        }


        [HttpGet("GetUnreadMessages")]
        public IActionResult GetUnreadMessages(int ownerId, int tenantId)
        {
            var messages = _messageRepository.GetUnreadMessages(ownerId, tenantId).Where(m => !m.IsDeleted)
                .Select(m => new SearchMessageDTO
                {
                    MessageID = m.MessageID,
                    Content = m.Content,
                    SenderFlag = m.SenderFlag,
                    ReciveDate = m.ReciveDate,
                    ReadDate = m.ReadDate,
                    OwnerId = m.OwnerUsserId,
                    TentantId = m.tentantUsserId
                });

            if (!messages.Any())
                return NotFound("No unread messages found.");

            return Ok(messages);
        }





        [HttpGet("GetAllMessages")]
        public IActionResult GetAll()
        {
            var messages = _messageRepository.GetAll().Where(m => !m.IsDeleted)
                .Select(m => new SearchMessageDTO
                {
                    MessageID = m.MessageID,
                    Content = m.Content,
                    SenderFlag = m.SenderFlag,
                    ReciveDate = m.ReciveDate,
                    ReadDate = m.ReadDate,
                    OwnerId = m.OwnerUsserId,
                    TentantId = m.tentantUsserId
                });

            return Ok(messages);
        }


        [HttpGet("GetMessageByID/{id}")]
        public IActionResult GetById(int id)
        {
            var m = _messageRepository.GetById(id);

            if (m == null)
                return NotFound();

            var dto = new SearchMessageDTO
            {
                MessageID = m.MessageID,
                Content = m.Content,
                SenderFlag = m.SenderFlag,
                ReciveDate = m.ReciveDate,
                ReadDate = m.ReadDate, 
                OwnerId = m.OwnerUsserId,
                TentantId = m.tentantUsserId
            };

            return Ok(dto);
        }


        [HttpPost("AddMessage")]
        public IActionResult Add(MessageDTO dto)
        {
            var message = new Message
            {
                Content = dto.Content,
                SenderFlag = dto.SenderFlag,
                ReciveDate = dto.ReciveDate,
                ReadDate = dto.ReadDate,
                OwnerUsserId = dto.OwnerId,
                tentantUsserId = dto.TentantId
            };

            _messageRepository.Add(message);
            _messageRepository.Save();

            dto.MessageID = message.MessageID;

            return CreatedAtAction(nameof(GetById), new { id = dto.MessageID }, dto);
        }


        [HttpPut("UpdateMessage/{id}")]
        public IActionResult Update(int id, MessageDTO dto)
        {
            if (id != dto.MessageID)
                return BadRequest("Id mismatch.");

            var message = _messageRepository.GetById(id);

            if (message == null)
                return NotFound();

            message.Content = dto.Content;
            message.SenderFlag = dto.SenderFlag;
            message.ReciveDate = dto.ReciveDate;
            message.ReadDate = dto.ReadDate;
            
            message.OwnerUsserId = dto.OwnerId;
            message.tentantUsserId = dto.TentantId;

            _messageRepository.Update(message);
            _messageRepository.Save();

            return Ok(dto);
        }

        [HttpPut("MarkAsRead/{messageId}")]
        public IActionResult MarkAsRead(int messageId)
        {
            _messageRepository.MarkAsRead(messageId);
            _messageRepository.Save();

            return Ok("Message marked as read successfully.");
        }

        [HttpDelete("SoftDeleteMessage/{id}")]
        public IActionResult Delete(int id)
        {
            var message = _messageRepository.GetById(id);

            if (message == null)
                return NotFound();

            _messageRepository.SoftDelete(message);
            _messageRepository.Save();

            return Ok("Message soft deleted successfully.");
        }
    }
}