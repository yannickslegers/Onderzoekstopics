using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SC.BL.Domain;
using SC.BL;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private ITicketManager mgr = new TicketManager();
        public IEnumerable<Ticket> GetTickets()
        {
            return mgr.GetTickets();
        }

        public Ticket GetTicket(int ticketNumber)
        {
            throw new NotImplementedException();
        }

        public Ticket AddTicket(int accountId, string question)
        {
            throw new NotImplementedException();
        }

        public Ticket AddHardwareTicket(int accountId, string device, string problem)
        {
            throw new NotImplementedException();
        }

        public void ChangeTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void ChangeTicketStateToClosed(int ticketNumber)
        {
            throw new NotImplementedException();
        }

        public void RemoveTicket(int ticketNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TicketResponse> GetTicketResponses(int ticketNumber)
        {
            throw new NotImplementedException();
        }

        public TicketResponse AddResponses(int ticketNumber, string response, bool isClientResponse)
        {
            throw new NotImplementedException();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
