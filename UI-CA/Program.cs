﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SC.BL;
using SC.UI.CA.ExtensionMethods;
using SC.UI.CA.ServiceReference1;

namespace SC.UI.CA
{
    class Program
    {
        private static bool quit = false;
        private static readonly ServiceReference1.ServiceClient _client = new ServiceClient();

        static void Main(string[] args)
        {
            while (!quit)
                ShowMenu();
        }

        private static void ShowMenu()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("=== HELPDESK - SUPPORT CENTER ===");
            Console.WriteLine("=================================");
            Console.WriteLine("1) Toon alle tickets");
            Console.WriteLine("2) Toon details van een ticket");
            Console.WriteLine("3) Toon de antwoorden van een ticket");
            Console.WriteLine("4) Maak een nieuw ticket");
            Console.WriteLine("5) Geef een antwoord op een ticket");
            Console.WriteLine("6) Markeer ticket als 'Closed'");
            Console.WriteLine("0) Afsluiten");
            try
            {
                DetectMenuAction();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("Er heeft zich een onverwachte fout voorgedaan!");
                Console.WriteLine(e.Message);
                Console.WriteLine();
            }
        }

        private static void DetectMenuAction()
        {
            bool inValidAction = false;
            do
            {
                Console.Write("Keuze: ");
                string input = Console.ReadLine();
                int action;
                if (Int32.TryParse(input, out action))
                {
                    switch (action)
                    {
                        case 1:
                            PrintAllTickets(); break;
                        case 2:
                            ActionShowTicketDetails(); break;
                        case 3:
                            ActionShowTicketResponses(); break;
                        case 4:
                            ActionCreateTicket(); break;
                        case 5:
                            ActionAddResponseToTicket(); break;
                        case 6:
                            ActionCloseTicket(); break;
                        case 0:
                            quit = true;
                            return;
                        default:
                            Console.WriteLine("Geen geldige keuze!");
                            inValidAction = true;
                            break;
                    }
                }
            } while (inValidAction);
        }

        private static void ActionCloseTicket()
        {
            Console.Write("Ticketnummer: ");
            int input = Int32.Parse(Console.ReadLine());


            _client.ChangeTicketStateToClosed(input);
        }

        private static void PrintAllTickets()
        {

            foreach (var t in _client.GetTickets())
                Console.WriteLine(t.GetInfo());

        }

        private static void ActionShowTicketDetails()
        {
            Console.Write("Ticketnummer: ");
            int input = Int32.Parse(Console.ReadLine());

            TicketDTO t = _client.GetTicket(input);

            PrintTicketDetails(t);

        }

        private static void PrintTicketDetails(TicketDTO ticket)
        {
            Console.WriteLine("{0,-15}: {1}", "Ticket", ticket.TicketNumber);
            Console.WriteLine("{0,-15}: {1}", "Gebruiker", ticket.AccountId);
            Console.WriteLine("{0,-15}: {1}", "Datum", ticket.DateOpenend.ToString("dd/MM/yyyy"));
            Console.WriteLine("{0,-15}: {1}", "Status", ticket.State);

            if (ticket is HardwareTicketDTO)
                Console.WriteLine("{0,-15}: {1}", "Toestel", ((HardwareTicketDTO)ticket).DeviceName);

            Console.WriteLine("{0,-15}: {1}", "Vraag/probleem", ticket.Text);
        }

        private static void ActionShowTicketResponses()
        {
            Console.Write("Ticketnummer: ");
            int input = Int32.Parse(Console.ReadLine());



            IEnumerable<TicketResponseDTO> responses = _client.GetTicketResponses(input);

            if (responses != null) PrintTicketResponses(responses);
        }

        private static void PrintTicketResponses(IEnumerable<TicketResponseDTO> responses)
        {
            foreach (var r in responses)
                Console.WriteLine(r.GetInfo());
        }

        private static void ActionCreateTicket()
        {
            int accountNumber = 0;
            string problem = "";
            string device = "";

            Console.Write("Is het een hardware probleem (j/n)? ");
            bool isHardwareProblem = (Console.ReadLine().ToLower() == "j");
            if (isHardwareProblem)
            {
                Console.Write("Naam van het toestel: ");
                device = Console.ReadLine();
            }

            Console.Write("Gebruikersnummer: ");
            accountNumber = Int32.Parse(Console.ReadLine());
            Console.Write("Probleem: ");
            problem = Console.ReadLine();

            if (!isHardwareProblem)
                _client.AddTicket(accountNumber, problem);
            else
                _client.AddHardwareTicket(accountNumber, device, problem);

        }

        private static void ActionAddResponseToTicket()
        {
            Console.Write("Ticketnummer: ");
            int ticketNumber = Int32.Parse(Console.ReadLine());
            Console.Write("Antwoord: ");
            string response = Console.ReadLine();



            _client.AddResponse(ticketNumber, response, false);

        }
    }
}