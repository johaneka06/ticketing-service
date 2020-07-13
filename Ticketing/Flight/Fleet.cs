using System;

namespace Ticketing
{
    public class Fleet
    {
        private string _fleet;
        
        public string FleetInfo
        {
            get
            {
                return this._fleet;
            }
        }

        public Fleet(string fleet)
        {
            if(fleet.Length != 4) throw new Exception("Fleet info isn't correct!");

            this._fleet = fleet;
        }

        public override bool Equals(object obj)
        {
            var o = obj as Fleet;
            if(o == null) return false;

            return this._fleet == o._fleet;
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}