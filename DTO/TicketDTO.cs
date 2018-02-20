using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using SC.BL.Domain;

namespace DTO
{
    [DataContract]
    [KnownType(typeof(HardwareTicketDTO))]
    public class TicketDTO
    {
        public TicketDTO() { }

        public TicketDTO(Ticket ticket)
        {
            this.TicketNumber = ticket.TicketNumber;
            this.AccountId = ticket.AccountId;
            this.Text = ticket.Text;
            this.DateOpenend = ticket.DateOpened;
            this.State = ticket.State;
            List<TicketResponseDTO> responses = new List<TicketResponseDTO>();
            if (ticket.Responses != null)
            {
                foreach (var response in ticket.Responses)
                {
                    responses.Add(new TicketResponseDTO(response));
                }
                this.Responses = responses;
            }
            else
            {
                this.Responses = new List<TicketResponseDTO>();
            }
        }
        [DataMember]
        public int TicketNumber { get; set; }
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public DateTime DateOpenend { get; set; }
        [DataMember]
        public TicketState State { get; set; }
        [DataMember]
        public ICollection<TicketResponseDTO> Responses { get; set; }
    }
}
