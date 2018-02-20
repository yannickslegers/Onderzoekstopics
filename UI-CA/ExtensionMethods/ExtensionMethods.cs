using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SC.UI.CA.ServiceReference1;

namespace SC.UI.CA.ExtensionMethods
{
  internal static class ExtensionMethods
  {
    internal static string GetInfo(this TicketDTO t)
    {
      return String.Format("[{0}] {1} ({2} antwoorden)", t.TicketNumber, t.Text, t.Responses == null ? 0 : t.Responses.Count);
    }

    internal static string GetInfo(this TicketResponseDTO r)
    {
      return String.Format("{0:dd/MM/yyyy} {1}{2}", r.Date, r.Text, r.IsClientResponse ? " (client)" : "");
    }
  }
}
