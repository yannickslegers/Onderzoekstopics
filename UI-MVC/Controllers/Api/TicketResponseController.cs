using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SC.UI.Web.MVC.ServiceReference1;
using SC.UI.Web.MVC.Models;

namespace SC.UI.Web.MVC.Controllers.Api
{
    public class TicketResponseController : ApiController
    {
        
        private static readonly ServiceReference1.ServiceClient _client = new ServiceClient();

        public IHttpActionResult Get(int ticketNumber)
        {
            IEnumerable<TicketResponseDTO> responses = _client.GetTicketResponses(ticketNumber);

            if (responses == null || responses.Count() == 0)
                return StatusCode(HttpStatusCode.NoContent);
            
            return Ok(responses);
        }

        public IHttpActionResult Post(NewTicketResponseDTO response)
        {
            TicketResponseDTO createdResponse = _client.AddResponse(response.TicketNumber, response.ResponseText, response.IsClientResponse);

            if (createdResponse == null)
                return BadRequest("Er is iets misgelopen bij het registreren van het antwoord!");

            //// Circulaire referentie!! (TicketResponse <-> Ticket) -> can't be serialized!!
            //return CreatedAtRoute("DefaultApi",
            //                      new { Controller = "TicketResponse", id = createdResponse.Id },
            //                      createdResponse);

            // Gebruik DTO (Data Transfer Object)
            TicketResponseDTO responseData = new TicketResponseDTO()
            {
                Id = createdResponse.Id,
                Text = createdResponse.Text,
                Date = createdResponse.Date,
                IsClientResponse = createdResponse.IsClientResponse
            };

            return CreatedAtRoute("DefaultApi",
                                  new { Controller = "TicketResponse", id = responseData.Id },
                                  responseData);
        }
    }
}
