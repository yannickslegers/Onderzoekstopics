
using System.Collections.Generic;
using System.Linq;
using SC.BL;
using SC.BL.Domain;
using DTO;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in both code and config file together.
    public class Service : IService
    {
        private ITicketManager mgr = new TicketManager();

        public List<TicketDTO> GetTickets()
        {
            var tickets = mgr.GetTickets();
            var ticketsDTO = new List<TicketDTO>();
            foreach(var ticket in tickets)
            {
                ticketsDTO.Add(new TicketDTO(ticket));
            }
            return ticketsDTO;
        }

        public TicketDTO GetTicket(int ticketNumber)
        {
            return new TicketDTO(mgr.GetTicket(ticketNumber));
        }

        public IEnumerable<TicketResponseDTO> GetTicketResponses(int ticketNumber)
        {
            var responses = new List<TicketResponseDTO>();
            foreach (var response in mgr.GetTicketResponses(ticketNumber))
            {
                responses.Add(new TicketResponseDTO(response));
            }
            return responses.AsEnumerable();
        }

        public TicketDTO AddTicket(int accountId, string question)
        {
            return new TicketDTO(mgr.AddTicket(accountId, question));
        }

        public TicketDTO AddHardwareTicket(int accountId, string device, string problem)
        {
            return new TicketDTO(mgr.AddTicket(accountId, device, problem));
        }

        public TicketResponseDTO AddResponse(int ticketNumber, string response, bool isClientResponse)
        {
            return new TicketResponseDTO(mgr.AddTicketResponse(ticketNumber, response, isClientResponse));
        }

        public void ChangeTicket(TicketDTO ticket)
        {
            Ticket t = new Ticket();
            t.AccountId = ticket.AccountId;
            t.DateOpened = ticket.DateOpened;
            t.State = ticket.State;
            t.TicketNumber = ticket.TicketNumber;
            t.Text = ticket.Text;
            if (ticket.Responses != null)
            {
                foreach (var response in ticket.Responses)
                {
                    TicketResponse rsp = new TicketResponse();
                    rsp.Id = response.Id;
                    rsp.IsClientResponse = response.IsClientResponse;
                    rsp.Date = response.Date;
                    rsp.Text = response.Text;
                    rsp.Ticket = t;
                    t.Responses.Add(rsp);
                }
            }
            else
                t.Responses = new List<TicketResponse>();
            
            mgr.ChangeTicket(t);
        }

        public void ChangeTicketStateToClosed(int ticketNumber)
        {
            mgr.ChangeTicketStateToClosed(ticketNumber);
        }

        public void RemoveTicket(int ticketNumber)
        {
            mgr.RemoveTicket(ticketNumber);
        }
        

        

        
    }
}
