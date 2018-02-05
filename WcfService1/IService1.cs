using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SC.BL.Domain;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        // TODO: Add your service operations here
        [OperationContract]
        IEnumerable<Ticket> GetTickets();
        [OperationContract]
        Ticket GetTicket(int ticketNumber);
        [OperationContract]
        Ticket AddTicket(int accountId, string question);
        [OperationContract]
        Ticket AddHardwareTicket(int accountId, string device, string problem);
        [OperationContract]
        void ChangeTicket(Ticket ticket);
        [OperationContract]
        void ChangeTicketStateToClosed(int ticketNumber);
        [OperationContract]
        void RemoveTicket(int ticketNumber);

        [OperationContract]
        IEnumerable<TicketResponse> GetTicketResponses(int ticketNumber);
        [OperationContract]
        TicketResponse AddResponses(int ticketNumber, string response, bool isClientResponse);
    }



    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
