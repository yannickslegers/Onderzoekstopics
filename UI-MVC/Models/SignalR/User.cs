using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC.UI.Web.MVC.Models.SignalR
{
    public class User
    {
        public string ConnectionId { get; set; }

        public string UserName { get; set; }

        public string UserImage { get; set; }

        public string LoginTime { get; set; }

        public List<Message> Messages { get; set; }

        public void AddMessage(Message message)
        {
            this.Messages.Add(message);
        }
    }
}