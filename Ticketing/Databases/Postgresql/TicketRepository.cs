using System;
using System.Collections.Generic;
using Npgsql;

namespace Ticketing.Databases
{
    public class TicketRepository : ITicketRepository
    {
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;

        public TicketRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public void IssueTicket(Ticket t)
        {
            Guid id = Guid.NewGuid();
            string query = "INSERT INTO ticket_header (booking_code, entry_id, passenger_lname, price) VALUES (@code, @id, @pax, @price)";
            using(var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("code", t.BookingCode);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("pax", t.PassengerLName[0]);
                cmd.Parameters.AddWithValue("price", t.Price);
                cmd.ExecuteNonQuery();
            }

            query = "INSERT INTO ticket_detail VALUES (@code, @id, @flightNo, @paxFirst, @paxLast, @dep_sched)";
            for(int i = 0; i < t.PassengerLName.Count; i++)
            {
                using(var cmd = new NpgsqlCommand(query, _connection, _transaction))
                {
                    cmd.Parameters.AddWithValue("code", t.BookingCode);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("flightNo", t.FlightNo);
                    cmd.Parameters.AddWithValue("paxFirst", t.PassengersFName[i]);
                    cmd.Parameters.AddWithValue("paxLast", t.PassengerLName[i]);
                    cmd.Parameters.AddWithValue("dep_sched", t.DepartureDate);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Ticket RetrieveTicketInfo(string BookingId, string LastName)
        {
            Guid id = Guid.Empty;
            int price = 0;
            string query = "SELECT booking_code, entry_id, price FROM ticket_header WHERE booking_code = @code AND passenger_lname = @name";

            using(var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("code", BookingId);
                cmd.Parameters.AddWithValue("name", LastName);

                using(var reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        id = reader.GetGuid(1);
                        price = reader.GetInt32(2);
                    }
                }
            }

            query = "SELECT booking_code, flight_no, passenger_fname, passenger_lname, departure_date FROM ticket_detail WHERE booking_code = @code AND entry_id = @id";
            List<string> FName = new List<string>();
            List<string> LName = new List<string>();
            string bookingCode = "";
            string flightNo = "";
            DateTime departureDate = DateTime.Now;

            using(var cmd = new NpgsqlCommand(query, _connection, _transaction))
            {
                cmd.Parameters.AddWithValue("code", BookingId);
                cmd.Parameters.AddWithValue("id", id);

                using(var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        bookingCode = reader.GetString(0);
                        flightNo = reader.GetString(1);
                        FName.Add(reader.GetString(2));
                        LName.Add(reader.GetString(3));
                        departureDate = reader.GetDateTime(4);
                    }
                }
            }

            Ticket t = new Ticket(new BookCode(bookingCode), FName, LName, flightNo, departureDate, price);
            return t;
        }
    }
}