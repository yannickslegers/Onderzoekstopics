using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC.UI.Web.MVC.Models.SignalR
{
    public class Message
    {
        public string UserName { get; set; }

        public string Text { get; set; }

        public string Time { get; set; }

        public string UserImage { get; set; }
    }
}