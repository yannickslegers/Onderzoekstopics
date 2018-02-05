using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SC.BL.Domain
{
    [DataContract]
    public class TicketResponse : IValidatableObject
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required]
        public string Text { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public bool IsClientResponse { get; set; }
        [DataMember]
        [Required]
        public Ticket Ticket { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Date < Ticket.DateOpened)
            {
                errors.Add(new ValidationResult("Can't be before the date the ticket is created!", new string[] { "Date" }));
            }

            return errors;
        }
    }
}
