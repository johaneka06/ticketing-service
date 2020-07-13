using System;

namespace Ticketing
{
    public interface ITicketRepository
    {
        void IssueTicket(Ticket t);
        Ticket RetrieveTicketInfo(string BookingId, string LastName);
    }
}