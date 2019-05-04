using Newtonsoft.Json;
using System;

namespace LuKaSo.Zonky.Models
{
    public class ZonkyDateTime
    {
        /// <summary>
        /// Date 
        /// </summary>
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Format
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
