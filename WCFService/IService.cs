using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SC.BL;
using SC.BL.Domain;
using DTO;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract]
    [ServiceKnownType(typeof(List<Ticket>))]
    [ServiceKnownType(typeof(List<TicketResponse>))]
    public interface IService
    {
 
        // TODO: Add your service operations here
        [OperationContract]
        List<TicketDTO> GetTickets();
        [OperationContract]
        TicketDTO GetTicket(int ticketNumber);
        [OperationContract]
        TicketDTO AddTicket(int accountId, string question);
        [OperationContract]
        TicketDTO AddHardwareTicket(int accountId, string device, string problem);
        [OperationContract]
        void ChangeTicket(Ticket ticket);
        [OperationContract]
        void ChangeTicketStateToClosed(int ticketNumber);
        [OperationContract]
        void RemoveTicket(int ticketNumber);

        [OperationContract]
        IEnumerable<TicketResponseDTO> GetTicketResponses(int ticketNumber);
        [OperationContract]
        TicketResponseDTO AddResponse(int ticketNumber, string response, bool isClientResponse);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WCFService.ContractType".
   
}
