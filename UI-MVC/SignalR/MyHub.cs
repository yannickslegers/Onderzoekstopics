using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SC.UI.Web.MVC.Models.SignalR;

namespace SC.UI.Web.MVC
{
    public class ChatHub : Hub
    {
        static List<User> ConnectedUsers = new List<User>();
        static List<Message> CurrentMessage = new List<Message>();
        ConnClass ConnC = new ConnClass();

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;
            if(ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                string userImg = GetUserImage(userName);
                string loginTime = DateTime.Now.ToString();
                ConnectedUsers.Add(new User { ConnectionId = id, UserName = userName, UserImage = userImg, LoginTime = loginTime });

                //send to Caller
                Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);

                //send to All except caller client
                Clients.AllExcept(id).onNewUserConnected(id, userName, userImg, loginTime);
            }
        }

        public void SendMessageToAll(string userName, string message, string time)
        {
            string userImg = GetUserImage(userName);

            //store last 100 messages in cache
            AddMessageInCache(userName, message, time, userImg);

            //Broadcast message
            Clients.All.messageReceived(userName, message, time, userImg);
        }

        public void AddMessageInCache(string userName, string message, string time, string userImg)
        {
            CurrentMessage.Add(new Message
            {
                UserName = userName,
                Text = message,
                Time = time,
                UserImage = userName
            });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }

        public string GetUserImage(string userName)
        {
            string imgName = "/Images/dummy.jpg";

            try
            {
                string query = "select Image from tb_Users where UserName:'" + userName + "'";
                imgName = ConnC.GetColumnVal(query, "Image");
            }
            catch(Exception e)
            {
                //Exception handling
            }
            return imgName;
        }
    }
}