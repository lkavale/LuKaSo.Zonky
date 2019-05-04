namespace LuKaSo.Zonky.Models.Login
{
    public enum AuthorizationScope
    {
        /// <summary>
        /// Allow all actions with account
        /// </summary>
        SCOPE_APP_WEB,

        /// <summary>
        /// Allow download files ( exports, contracts)
        /// </summary>
        SCOPE_FILE_DOWNLOAD,

        /// <summary>
        ///  Allow to show only basic info about the user's account (/users/me/basic)
        /// </summary>
        SCOPE_APP_BASIC_INFO
    }
}
