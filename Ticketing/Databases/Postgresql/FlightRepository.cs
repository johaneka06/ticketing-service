using System;
using System.Collections.Generic;
using Npgsql;

namespace Ticketing.Databases
{
    public class FlightRepository : IFlightRepository
    {
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;

        public FlightRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            this._connection = connection;
            this._transaction = transaction;
        }

        public List<Flight> FindFlight(string departure, string arrival, DateTime departureDate, int ticket)
        {
            int dayOfWeek = Convert.ToInt32(departureDate.DayOfWeek);
            List<Flight> listOfFlight = new List<Flight>();
            string query = @"SELECT flight_no, fleet, departure_apt, arrival_apt, dep_sched, arr_sched, price
                FROM flight_data f
                WHERE @slot = ANY (route_slot) AND departure_apt = @dept AND arrival_apt = @arr";

            using (var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("slot", dayOfWeek);
                cmd.Parameters.AddWithValue("dept", departure);
                cmd.Parameters.AddWithValue("arr", arrival);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FlightNumber fn = new FlightNumber(reader.GetString(0));
                        Fleet fl = new Fleet(reader.GetString(1));
                        string departure_apt = reader.GetString(2);
                        string arrival_apt = reader.GetString(3);
                        DateTime dept_time = reader.GetDateTime(4);
                        DateTime arr = reader.GetDateTime(5);
                        int price = reader.GetInt32(6);

                        Flight f = new Flight(fn, fl, departure_apt, arrival_apt, dept_time, arr, price);
                        listOfFlight.Add(f);
                    }
                }
            }

            listOfFlight = CorrectList(listOfFlight, ticket, departureDate);

            return listOfFlight;
        }

        private List<Flight> CorrectList(List<Flight> list, int ticket, DateTime departureDate)
        {
            string query = @"SELECT COUNT(booking_code), td.flight_no, departure_date, ar.fleet FROM ticket_detail td
	                JOIN flight_data fd ON td.flight_no = fd.flight_no
	                JOIN aircraft ar ON ar.fleet = fd.fleet
                WHERE departure_date = @date
                GROUP BY ar.fleet, departure_date, td.flight_no
                HAVING COUNT(booking_code) + @ticket > ar.capacity";

            using(var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("date", departureDate);
                cmd.Parameters.AddWithValue("ticket", ticket);
                
                using(var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        string flightNo = reader.GetString(1);
                        foreach(Flight f in list)
                        {
                            if(f.FlightNo == flightNo) list.Remove(f);
                        }
                    }
                }
            }

            return list;
        }
    }
}