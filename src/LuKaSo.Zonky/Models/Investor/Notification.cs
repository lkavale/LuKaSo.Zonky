using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Models.Investor
{
    public class Notification
    {
        /// <summary>
        /// Id of a transaction
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Date of the transaction
        /// </summary>
        [JsonProperty("date", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Text of the notification
        /// </summary>
        [JsonProperty("text", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        /// <summary>
        /// Notification has been already visited
        /// </summary>
        [JsonProperty("visited", Required = Required.Always)]
        [Required]
        public bool Visited { get; set; }

        /// <summary>
        /// Notification link
        /// </summary>
        [JsonProperty("link", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public NotificationLink Link { get; set; }
    }
}
