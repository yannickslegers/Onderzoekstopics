using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

using SC.BL;
using SC.BL.Domain;
using SC.UI.Web.MVC.Models;
using SC.UI.Web.MVC.ServiceReference1;

namespace SC.UI.Web.MVC.Controllers
{
    public class TicketController : Controller
    {

        private static readonly ServiceReference1.ServiceClient _client = new ServiceClient();
        // GET: Ticket
        public ActionResult Index()
        {
            IEnumerable<TicketDTO> tickets = _client.GetTickets();
            return View(tickets);
        }

        // GET: Ticket/Details/5
        public ActionResult Details(int id)
        {
            TicketDTO ticket = _client.GetTicket(id);
            var responses = _client.GetTicketResponses(id);
            ticket.Responses = responses;


            return View(ticket);
        }

        // GET: Ticket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        /* public ActionResult Create(TicketDTO ticket)
        {
          if (ModelState.IsValid)
          {
            ticket = _client.AddTicket(ticket.AccountId, ticket.Text);

            return RedirectToAction("Details", new { id = ticket.TicketNumber });
          }

          return View();
        } */
        // ADHV view-model 'CreateTicketVM'
        public ActionResult Create(CreateTicketVM newTicket)
        {
            if (ModelState.IsValid)
            {
                TicketDTO ticket = _client.AddTicket(newTicket.AccId, newTicket.Problem);
                return RedirectToAction("Details", new { id = ticket.TicketNumber });
            }

            return View();
        }

        // GET: Ticket/Edit/5
        public ActionResult Edit(int id)
        {
            TicketDTO ticket = _client.GetTicket(id);
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TicketDTO ticket)
        {
            if (ModelState.IsValid)
            {
                _client.ChangeTicket(ticket);
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Ticket/Delete/5
        public ActionResult Delete(int id)
        {
            TicketDTO ticket = _client.GetTicket(id);
            return View(ticket);
        }

        // POST: Ticket/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _client.RemoveTicket(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
