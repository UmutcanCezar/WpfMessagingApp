using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mesajprog1.Model
{
    public class MessageModel
    {
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }

        // XAML’deki triggerlar için
        public bool IsMessageReceived => !(SenderID != CurrentUser.Currentuser.Id);

        public string SentMessage => Content;
        public string ReceivedMessage => Content;

        public string MsgSentOn => SentAt.ToString("dd.MM.yyyy HH:mm");
        public string MsgReceivedOn => SentAt.ToString("dd.MM.yyyy HH:mm");
    }

}
