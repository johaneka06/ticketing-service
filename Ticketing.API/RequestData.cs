using System;
using System.Text.Json.Serialization;

namespace Ticketing.API
{
    public class RequestData
    {
        [JsonPropertyName("dep-airport")]
        public string departureAirport {get; set;}

        [JsonPropertyName("dest-airport")]
        public string arrivalAirport {get; set;}

        [JsonPropertyName("dep-date")]
        public DateTime departureDate {get; set;}

        [JsonPropertyName("total-pax")]
        public int paxTotal {get; set;}
    }
}