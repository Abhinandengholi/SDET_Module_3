using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CaseStudy
{
    public class BookingID
    {
        [JsonProperty("bookingid")]
        public string? BookingId { get; set; }

     
    }
}
