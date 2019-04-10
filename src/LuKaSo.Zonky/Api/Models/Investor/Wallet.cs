using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LuKaSo.Zonky.Api.Models.Investor
{
    public class Wallet
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id", Required = Required.Always)]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Bank account
        /// </summary>
        [JsonProperty("account", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public BankAccount BankAccount { get; set; }

        /// <summary>
        /// Available balance
        /// </summary>
        [JsonProperty("availableBalance", Required = Required.Always)]
        [Required]
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// Balance
        /// </summary>
        [JsonProperty("balance", Required = Required.Always)]
        [Required]
        public decimal Balance { get; set; }

        /// <summary>
        /// Blocked balance
        /// </summary>
        [JsonProperty("blockedBalance", Required = Required.Always)]
        [Required]
        public decimal BlockedBalance { get; set; }

        /// <summary>
        /// Credit sum
        /// </summary>
        [JsonProperty("creditSum", Required = Required.Always)]
        [Required]
        public decimal CreditSum { get; set; }

        /// <summary>
        /// Debit sum
        /// </summary>
        [JsonProperty("debitSum", Required = Required.Always)]
        [Required]
        public decimal DebitSum { get; set; }

        /// <summary>
        /// Variable symbol
        /// </summary>
        [JsonProperty("variableSymbol", Required = Required.Always)]
        [Required(AllowEmptyStrings = true)]
        public string VariableSymbol { get; set; }
    }
}
