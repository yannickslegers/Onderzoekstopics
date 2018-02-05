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
  public class Ticket
  {
    //[Key] /* TOEGEVOEGD, nadien VERWIJDERD 'Fluent API' */
    [DataMember]
    public int TicketNumber { get; set; }
    [DataMember]
    public int AccountId { get; set; }
    [DataMember]
    [Required]
    [StringLength(100, ErrorMessage="Er zijn maximaal 100 tekens toegestaan")]
    public string Text { get; set; }
    [DataMember]
    public DateTime DateOpened { get; set; }
    //[Index] /* TOEGEVOEGD, nadien VERWIJDERD 'Fluent API' */
    [DataMember]
    public TicketState State { get; set; }
    [IgnoreDataMember]
    public virtual ICollection<TicketResponse> Responses { get; set; } /* TOEGEVOEGD 'virtual' for lazy-loading, if enabled on context (default) */
  }
}
