using SC.UI.Web.MVC.Models.SignalR;
using System.Web.Mvc;

namespace SC.UI.Web.MVC.Controllers
{
    public class ChatController : Controller
    {

        public ActionResult Admin()
        {
            User user = new User();
            user.UserName = Session["UserName"].ToString();
            user.UserImage = "/Images/dummy.jpg";
            return View(user);
        }

        public ActionResult UserChat()
        {
            User user = new User();
            user.UserName = Session["UserName"].ToString();
            user.UserImage = "/Images/dummy.jpg";
            return View(user);
        }
    }
}
