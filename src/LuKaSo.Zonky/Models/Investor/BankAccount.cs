using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Models.Investor
{
    public class BankAccount
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Bank
        /// </summary>
        [JsonProperty("accountBank", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string AccountBank { get; set; }

        /// <summary>
        /// Account name
        /// </summary>
        [JsonProperty("accountName", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string AccountName { get; set; }

        /// <summary>
        /// Account number
        /// </summary>
        [JsonProperty("accountNo", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string AccountNo { get; set; }
    }
}
