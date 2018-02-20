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
    public class TicketResponseDTO
    {
        public TicketResponseDTO(TicketResponse response)
        {
            this.Id = response.Id;
            this.Text = response.Text;
            this.Date = response.Date;
            this.IsClientResponse = response.IsClientResponse;
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public bool IsClientResponse { get; set; }
        
    }
}
