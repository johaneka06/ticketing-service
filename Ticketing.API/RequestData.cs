using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ticketing.API
{
    public class RequestData
    {
        [JsonPropertyName("dep-airport")]
        public string departureAirport { get; set; }

        [JsonPropertyName("dest-airport")]
        public string arrivalAirport { get; set; }

        [JsonPropertyName("dep-date")]
        public DateTime departureDate { get; set; }

        [JsonPropertyName("total-pax")]
        public int paxTotal { get; set; }
    }

    public class IssueData
    {
        [JsonPropertyName("FirstNames")]
        public string[] FNames { set; get; }

        [JsonPropertyName("LastNames")]
        public string[] LName { set; get; }

        [JsonPropertyName("Flight")]
        public string flightNo { set; get; }

        [JsonPropertyName("Departure-Date")]
        public DateTime date { set; get; }

        [JsonPropertyName("Price")]
        public int price { set; get; }
    }

    public class RetrieveInfo
    {
        [JsonPropertyName("LastName")]
        public string LName { set; get; }

        [JsonPropertyName("BookingCode")]
        public string code { set; get; }
    }
}