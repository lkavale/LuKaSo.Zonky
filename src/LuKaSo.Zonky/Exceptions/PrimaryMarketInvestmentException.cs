using LuKaSo.Zonky.Models.Markets;
using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace LuKaSo.Zonky.Exceptions
{
    [Serializable]
    public class PrimaryMarketInvestmentException : Exception
    {
        /// <summary>
        /// Primary market investment failed
        /// The input is invalid.
        /// Possible error codes are:
        /// CAPTCHA_REQUIRED - Captcha verification is required
        /// insufficientBalance - The user cannot invest because of a low wallet balance
        /// cancelled - The loan has been canceled
        /// withdrawn - The loan has been withdrawn by the borrower
        /// reservedInvestmentOnly - The user cannot invest without a reservation. The whole remaining investment is reserved for other users.
        /// overInvestment - The amount for investment is too high
        /// multipleInvestment - The user has already invested to this loan. Try increasing the investment instead
        /// alreadyCovered - The whole loan amount has been already covered
        /// </summary>
        /// <param name="investment"></param>
        /// <param name="message"></param>
        public PrimaryMarketInvestmentException(PrimaryMarketInvestment investment, HttpResponseMessage message) : base($"Buy primary market participation {investment.ToString()} failed \r\nServer return \r\n {message.ToString()}")
        { }

        /// <summary>
        /// Primary market increase investment failed
        /// The input is invalid.
        /// Possible error codes are:
        /// CAPTCHA_REQUIRED - Captcha verification is required
        /// insufficientBalance - The user cannot invest because of a low wallet balance
        /// cancelled - The loan has been canceled
        /// withdrawn - The loan has been withdrawn by the borrower
        /// reservedInvestmentOnly - The user cannot invest without a reservation. The whole remaining investment is reserved for other users.
        /// overInvestment - The amount for investment is too high
        /// multipleInvestment - The user has already invested to this loan. Try increasing the investment instead
        /// alreadyCovered - The whole loan amount has been already covered
        /// </summary>
        /// <param name="investmentId"></param>
        /// <param name="increaseInvestment"></param>
        /// <param name="message"></param>
        public PrimaryMarketInvestmentException(int investmentId, IncreasePrimaryMarketInvestment increaseInvestment, HttpResponseMessage message) : base($"Increase primary market participation id {investmentId} - {increaseInvestment.ToString()} failed \r\n Server return \r\n {message.ToString()}")
        { }

        protected PrimaryMarketInvestmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
