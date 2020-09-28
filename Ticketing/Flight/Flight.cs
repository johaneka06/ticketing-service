using System;

namespace Ticketing
{
    public class Flight
    {
        private FlightNumber _fn;
        private Fleet _fleet;
        private string _departure;
        private string _arrival;
        private DateTime _departure_sched;
        private DateTime _arrival_sched;
        private int _price;

        public string FlightNo
        {
            get
            {
                return this._fn.FlightNo;
            }
        }
        public string Fleet
        {
            get
            {
                return this._fleet.FleetInfo;
            }
        }

        public string DepartureAirport
        {
            get
            {
                return this._departure;
            }
        }

        public Time DepartureSchedule
        {
            get
            {
                return new Time(this._departure_sched.Hour, this._departure_sched.Minute, 7);
            }
        }

        public string ArrivalAirport
        {
            get
            {
                return this._arrival;
            }
        }

        public Time ArrivalSchedule
        {
            get
            {
                return new Time(this._arrival_sched.Hour, this._arrival_sched.Minute, 7);
            }
        }

        public int Price
        {
            get
            {
                return this._price;
            }
        }

        public Flight(FlightNumber fn, Fleet fleet, string departure, string arrival, DateTime departureSched, DateTime arrivalSched, int price)
        {
            this._fn = fn;
            this._fleet = fleet;
            this._departure = departure;
            this._arrival = arrival;
            this._departure_sched = departureSched;
            this._arrival_sched = arrivalSched;
            this._price = price;
        }

        public void ChangeSchedule(DateTime newDepature, DateTime newArrival)
        {
            this._departure_sched = newDepature;
            this._arrival_sched = newArrival;
        }

        public void ChangeFleet(Fleet newFleet)
        {
            this._fleet = newFleet;
        }

        public void ChangePrice(int newPrice)
        {
            this._price = newPrice;
        }

        public override bool Equals(object obj)
        {
            var o = obj as Flight;
            if(o == null) return false;

            return o._fn.Equals(this._fn);
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Time
    {
        public int Hour {get; private set;}
        public int Min {get; private set;}
        public int TimeZone {get; private set;}

        public Time(int hour, int min, int tz)
        {
            this.Hour = hour;
            this.Min = min;
            this.TimeZone = tz;
        }
    }
}