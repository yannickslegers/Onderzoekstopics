
using System.Collections.Generic;
using System.Linq;
using SC.BL;
using SC.BL.Domain;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in both code and config file together.
    public class Service : IService
    {
        private ITicketManager mgr = new TicketManager();

        public List<Ticket> GetTickets()
        {
            return mgr.GetTickets();
        }

        public Ticket GetTicket(int ticketNumber)
        {
            return mgr.GetTicket(ticketNumber);
        }

        public Ticket AddTicket(int accountId, string question)
        {
            return mgr.AddTicket(accountId, question);
        }

        public Ticket AddHardwareTicket(int accountId, string device, string problem)
        {
            return mgr.AddTicket(accountId, device, problem);
        }

        public void ChangeTicket(Ticket ticket)
        {
            mgr.ChangeTicket(ticket);
        }

        public void ChangeTicketStateToClosed(int ticketNumber)
        {
            mgr.ChangeTicketStateToClosed(ticketNumber);
        }

        public void RemoveTicket(int ticketNumber)
        {
            mgr.RemoveTicket(ticketNumber);
        }

        public IEnumerable<TicketResponse> GetTicketResponses(int ticketNumber)
        {
            return mgr.GetTicketResponses(ticketNumber);
        }

        public TicketResponse AddResponses(int ticketNumber, string response, bool isClientResponse)
        {
            return mgr.AddTicketResponse(ticketNumber, response, isClientResponse);
        }

        
    }
}
