using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SC.BL.Domain
{
    [DataContract]
    public class HardwareTicket : Ticket
    {
        [RegularExpression("^(PC-)[0-9]+")]
        [DataMember]
        public string DeviceName { get; set; }
    }
}
