using System;
using Npgsql;

namespace Ticketing.Databases
{
    public class UnitOfWork : IUnitOfWork
    {
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;
        private IFlightRepository _flightRepo;
        private ITicketRepository _ticketRepo;

        public UnitOfWork(string Host, string database, string username, string password, string port)
        {
            string conenctionStr = Host + username + password + database + port;
            _connection = new NpgsqlConnection(conenctionStr);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IFlightRepository FlightRepository
        {
            get
            {
                if(_flightRepo == null) _flightRepo = new FlightRepository(_connection, _transaction);
                return _flightRepo;
            }
        }

        public ITicketRepository TicketRepository
        {
            get
            {
                if(_ticketRepo == null) _ticketRepo = new TicketRepository(_connection, _transaction);
                return _ticketRepo;
            }
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        private bool _dispose = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!this._dispose)
            {
                if(disposing)
                {
                    _connection.Close();
                }
                _dispose = true;
            }
        }
    }
}