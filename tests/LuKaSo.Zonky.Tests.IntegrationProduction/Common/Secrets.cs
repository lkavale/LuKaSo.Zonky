using LuKaSo.Zonky.Models.Login;

namespace LuKaSo.Zonky.Tests.IntegrationProduction.Common
{
    public class Secrets
    {
        /// <summary>
        /// Login Ok
        /// </summary>
        public User LoginOk { get; set; }

        /// <summary>
        /// Login wrong
        /// </summary>
        public User LoginWrong { get; set; }
    }
}
