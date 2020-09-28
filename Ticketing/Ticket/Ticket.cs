using System;
using System.Collections.Generic;

namespace Ticketing
{
    public class Ticket
    {
        private BookCode _code;
        private List<string> _passengerFName_List;
        private List<string> _passengerLName_List;
        private string _flightNo;
        private DateTime _departureDate;
        private int _price;

        public string BookingCode
        {
            get
            {
                return this._code.Code;
            }
        }

        public List<string> PassengersFName
        {
            get
            {
                return this._passengerFName_List;
            }
        }

        public List<string> PassengerLName
        {
            get
            {
                return this._passengerLName_List;
            }
        }

        public string FlightNo
        {
            get
            {
                return this._flightNo;
            }
        }

        public DateTime DepartureDate
        {
            get
            {
                return this._departureDate;
            }
        }

        public int Price
        {
            get
            {
                return this._price;
            }
        }

        public Ticket(BookCode code, List<string> passengerFName_List, List<string> passengerLName_List, string FlightNo, DateTime departureDate, int price)
        {
            this._code = code;
            this._passengerFName_List = passengerFName_List;
            this._passengerLName_List = passengerLName_List;
            this._flightNo = FlightNo;
            this._departureDate = departureDate;
            this._price = price;
        }

        public static Ticket IssueTicket(List<string> passengerFName_List, List<string> passengerLName_List, string FlightNo, DateTime departureDate, int price)
        {
            return new Ticket(new BookCode(), passengerFName_List, passengerLName_List, FlightNo, departureDate, price);
        }

        public void UpdateDate(DateTime newDate)
        {
            if(_departureDate > DateTime.Now) throw new Exception("Flight is alreade expired!");
            _departureDate = newDate;
        }

        public override bool Equals(object obj)
        {
            var o = obj as Ticket;
            if(o == null) return false;

            return this._code.Equals(o._code);
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}