using api1.Entities;
using api1.repository;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace api1.Hubs
{
    public class ChatHub :Hub
    {
        private readonly IMessageRepository _messageRepository;
        public ChatHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;

        }

        public async Task SendMessage(int senderID, int receiverID, string message)
        {
            var msgEntity = new Message
            {
                SenderID = senderID,
                ReceiverID = receiverID,
                Content = message,
                SentAt = DateTime.UtcNow
            };

            await _messageRepository.SaveMessageAsync(msgEntity);

            await Clients.Users(senderID.ToString(), receiverID.ToString())
                .SendAsync("ReceiveMessage", senderID, receiverID, message, msgEntity.SentAt);

            await Clients.Users(senderID.ToString(), receiverID.ToString())
                .SendAsync("LastMessage", senderID, receiverID, message,msgEntity.SentAt);

        }
    }
}
