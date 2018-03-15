using System.Collections.Generic;
using System.ServiceModel;
using SC.BL.Domain;
using DTO;

namespace WCFService
{
    [ServiceContract]
    public interface IService
    {
 
        
        [OperationContract]
        List<TicketDTO> GetTickets();
        [OperationContract]
        TicketDTO GetTicket(int ticketNumber);
        [OperationContract]
        TicketDTO AddTicket(int accountId, string question);
        [OperationContract]
        TicketDTO AddHardwareTicket(int accountId, string device, string problem);
        [OperationContract]
        void ChangeTicket(TicketDTO ticket);
        [OperationContract]
        void ChangeTicketStateToClosed(int ticketNumber);
        [OperationContract]
        void RemoveTicket(int ticketNumber);

        [OperationContract]
        IEnumerable<TicketResponseDTO> GetTicketResponses(int ticketNumber);
        [OperationContract]
        TicketResponseDTO AddResponse(int ticketNumber, string response, bool isClientResponse);
    }

   
}
