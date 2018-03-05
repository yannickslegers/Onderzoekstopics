using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SC.BL;
using SC.BL.Domain;
using SC.UI.Web.MVC.ServiceReference1;

namespace SC.UI.Web.MVC.Controllers.Api
{
    public class TicketController : ApiController
    {
        private ITicketManager mgr = new TicketManager();
        private static readonly ServiceReference1.ServiceClient _client = new ServiceClient();

        [HttpPut]
        [Route("api/Ticket/{id}/State/Closed")]
        public IHttpActionResult PutTicketStateToClosed(int id)
        {
            //mgr.ChangeTicketStateToClosed(id);
            _client.ChangeTicketStateToClosed(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
