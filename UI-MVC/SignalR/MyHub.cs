using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SC.UI.Web.MVC.Models.SignalR;

namespace SC.UI.Web.MVC.SignalR
{
    public class ChatHub : Hub
    {
        static List<User> ConnectedUsers = new List<User>();
        static List<Message> CurrentMessage = new List<Message>();
        static Dictionary<string, List<Message>> MessageCache = new Dictionary<string, List<Message>>();
        private readonly ConnClass _connC = new ConnClass();

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;
            if(ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                string userImg = GetUserImage(userName);
                string loginTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);

                User user = new User
                {
                    ConnectionId = id,
                    UserImage = userImg,
                    UserName = userName,
                    LoginTime = loginTime
                };
                ConnectedUsers.Add(user);

                if (!MessageCache.Keys.Contains(userName))
                {
                    MessageCache.Add(userName, new List<Message>());
                }
                
                // send to caller
                CurrentMessage = MessageCache[userName];
                Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, userName, userImg, loginTime);
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {

            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item == null) return base.OnDisconnected(stopCalled);
            ConnectedUsers.Remove(item);

            var id = Context.ConnectionId;
            Clients.All.onUserDisconnected(id, item.UserName);

            return base.OnDisconnected(stopCalled);
        }

        //Message broadcasting
        public void SendMessageToAll(string message, string time)
        {
            string userImg = GetUserImage("admin");
            //saving message in cache of all connected users
            foreach (var user in ConnectedUsers){
                AddMessageInCache(user.UserName,"admin", message, time, userImg);
            }
            //Broadcast message
            Clients.All.messageReceived("admin", message, time, userImg);
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
                AddMessageInCache(toUserName,userName, message, time, userImg);
                // send to 
                Clients.Client(toUserId).messageReceived(fromUser.UserName, message, time, userImg);
            }
        }

        public string GetUserImage(string userName)
        {
            string imgName = "/Images/dummy.jpg";

            try
            {
                string query = "select Image from tb_Users where UserName:'" + userName + "'";
                imgName = _connC.GetColumnVal(query, "Image");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return imgName;
        }

        private void AddMessageInCache(string toUserName, string fromUserName, string message, string time, string userImg)
        {
            //var user = Context.User.Identity.Name;
            if (fromUserName.Equals("admin"))
            {
                MessageCache[toUserName].Add(new Message { UserName = fromUserName, Text = message, Time = time, UserImage = userImg });
            }
            else
            {
                MessageCache[fromUserName].Add(new Message { UserName = fromUserName, Text = message, Time = time, UserImage = userImg });

            }

            if (MessageCache[toUserName].Count > 100)
                MessageCache[toUserName].RemoveAt(0);

        }

        public void GetMessageCache(string userName)
        {
            CurrentMessage = MessageCache[userName];
            Clients.Caller.reloadMessages(CurrentMessage);
        }
    }
}