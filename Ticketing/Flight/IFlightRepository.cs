using System;
using System.Collections.Generic;

namespace Ticketing
{
    public interface IFlightRepository
    {
        List<Flight> FindFlight(string departure, string arrival, DateTime departureDate, int ticket);
        Flight FlightInfo(string flightNumber);
    }
}