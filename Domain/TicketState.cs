using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SC.BL.Domain
{
    [DataContract]
    [Flags]
    public enum TicketState : byte
    {
        [EnumMember]
        Open = 1,
        [EnumMember]
        Answered,
        [EnumMember]
        ClientAnswer,
        [EnumMember]
        Closed
    }
}
