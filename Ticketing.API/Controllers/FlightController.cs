using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ticketing;
using Ticketing.Databases;

namespace Ticketing.API
{
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;
        private UnitOfWork uow;

        public FlightController(ILogger<FlightController> logger)
        {
            _logger = logger;
            DotNetEnv.Env.Load();
            string host = System.Environment.GetEnvironmentVariable("HOST");
            string username = System.Environment.GetEnvironmentVariable("USER_NAME");
            string password = System.Environment.GetEnvironmentVariable("PASSWORD");
            string db = System.Environment.GetEnvironmentVariable("DB");
            string port = System.Environment.GetEnvironmentVariable("PORT");

            uow = new UnitOfWork(host, db, username, password, port);
        }

        [HttpGet("api/v1/flight")]
        public ActionResult<IEnumerable<Flight>> AllFlight([FromBody] RequestData data)
        {
            List<Flight> flights = uow.FlightRepository.FindFlight(data.departureAirport, data.arrivalAirport, data.departureDate, data.paxTotal);
            return (flights.Count != 0) ? new ActionResult<IEnumerable<Flight>>(flights) : NoContent(); 
        }
    }
}