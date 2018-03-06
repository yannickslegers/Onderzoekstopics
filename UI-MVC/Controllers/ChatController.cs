using SC.UI.Web.MVC.Models.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SC.UI.Web.MVC.Controllers
{
    public class ChatController : Controller
    {

        public ActionResult Admin()
        {
            User user = new User();
            user.ConnectionId = "1";
            user.UserName = "admin";
            user.UserImage = "/Images/dummy.jpg";
            user.Messages = null;
            return View(user);
        }

        public ActionResult UserChat()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
