using System;

namespace LuKaSo.Zonky.Models.Login
{
    public class MfaCode
    {
        public Guid MfaToken { get; set; }

        public int SmsCode { get; set; }
    }
}
