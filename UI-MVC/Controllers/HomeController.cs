using SC.UI.Web.MVC.Models.SignalR;
using System.Web.Mvc;
using System.Web.Security;

namespace SC.UI.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            if(Session["LoggedIn"] == null)
            {
                Session["LoggedIn"] = "false";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            Session["UserName"] = model.UserName.ToLower();
            Session["LoggedIn"] = "true";
            return RedirectToAction("Index", "Home");

        }

        public ActionResult LogOut()
        {
            Session["LoggedIn"] = "false";
            return RedirectToAction("Index", "Home");
        }
    }
}