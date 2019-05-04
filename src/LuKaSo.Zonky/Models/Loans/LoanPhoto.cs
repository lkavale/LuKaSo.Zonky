using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Models.Loans
{
    public class LoanPhoto
    {
        /// <summary>
        /// Name of the photo
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string Name { get; set; }

        /// <summary>
        /// Relative URL to the photo
        /// </summary>
        [JsonProperty("url", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string Url { get; set; }
    }
}
