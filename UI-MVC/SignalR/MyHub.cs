using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using SC.UI.Web.MVC.Models.SignalR;
using System.Threading.Tasks;

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
                ConnectedUsers.Add(new User
                {
                    ConnectionId = id,
                    UserImage = userImg,
                    UserName = userName,
                    LoginTime = loginTime
                });

                // send to caller
                Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, userName, userImg, loginTime);
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);

            }

            return base.OnDisconnected(stopCalled);
        }

        public void SendMessageToAll(string userName, string message, string time)
        {
            string userImg = GetUserImage(userName);
            //AddMessageinCache(userName, message, time, userImg);
            //Broadcast message
            Clients.All.messageReceived(userName, message, time, userImg);
        }

        public void SendPrivateMessage(string toUserName, string message, string time)
        {
            string fromUserId = Context.ConnectionId;

            var toUser = ConnectedUsers.FirstOrDefault(x => x.UserName.Equals(toUserName));
            var toUserId = toUser.ConnectionId;
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);
            var userName = fromUser.UserName;
            if (toUser != null && fromUser != null)
            {
                string userImg = GetUserImage(fromUser.UserName);
                AddMessageinCache(userName, message, time, userImg);
                // send to 
                //Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message, userImg, CurrentDateTime);
                Clients.Client(toUserId).messageReceived(fromUser.UserName, message, time, userImg);

                // send to caller user
                //Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message, userImg, CurrentDateTime);
                //Clients.Caller.messageReceived(fromUser.UserName, message, CurrentDateTime, userImg);
            }
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

        private void AddMessageinCache(string userName, string message, string time, string UserImg)
        {
            CurrentMessage.Add(new Message { UserName = userName, Text = message, Time = time, UserImage = UserImg });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);

        }
    }
}