using System;

namespace Ticketing
{
    public class FlightNumber
    {
        private string _flightNumber;

        public string FlightNo
        {
            get
            {
                return this._flightNumber;
            }
        }

        public FlightNumber()
        {
            this._flightNumber = "";
        }

        public FlightNumber(string flightNo)
        {
            if(flightNo.Length < 5 || flightNo.Length > 6) throw new Exception("Flight number must 5 or 6 chars");

            this._flightNumber = flightNo;
        }

        public FlightNumber ChangeFN(string flightNo)
        {
            if(flightNo.Length < 5 || flightNo.Length > 6) throw new Exception("Flight number must 5 or 6 chars");

            return new FlightNumber(flightNo);
        }

        public override bool Equals(object obj)
        {
            var o = obj as FlightNumber;
            if(o == null) return false;

            return o._flightNumber == this._flightNumber;
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}