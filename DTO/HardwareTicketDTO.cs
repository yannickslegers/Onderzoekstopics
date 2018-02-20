using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DTO
{
    [DataContract]
    public class HardwareTicketDTO : TicketDTO
    {
        [DataMember]
        public string DeviceName { get; set; }
    }
}
