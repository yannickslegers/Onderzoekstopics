using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SC.UI.Web.MVC.Startup))]

namespace SC.UI.Web.MVC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
