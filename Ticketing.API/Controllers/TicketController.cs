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
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private UnitOfWork uow;

        public TicketController(ILogger<TicketController> logger)
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

        [HttpPost("api/v1/ticket")]
        public ActionResult<Ticket> IssueTicket([FromBody] IssueData data)
        {
            Ticket t = Ticket.IssueTicket(data.FNames.ToList(), data.LName.ToList(), data.flightNo, data.date, data.price);

            uow.TicketRepository.IssueTicket(t);
            uow.Commit();

            return (t != null) ? new ActionResult<Ticket>(t) : BadRequest();
        }

        [HttpGet("api/v1/ticket/{bookingCode}")]
        public ActionResult<Ticket> RetrieveTicket(string bookingCode, [FromBody] RetrieveInfo data)
        {
            Ticket t = uow.TicketRepository.RetrieveTicketInfo(data.code, data.LName);

            return (t != null) ? new ActionResult<Ticket>(t) : NoContent();
        }
    }
}