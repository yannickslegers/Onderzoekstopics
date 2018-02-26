using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SC.UI.Web.MVC.Controllers.SignalR.Startup))]

namespace SC.UI.Web.MVC.Controllers.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
