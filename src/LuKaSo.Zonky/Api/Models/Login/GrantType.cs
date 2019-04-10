namespace LuKaSo.Zonky.Api.Models.Login
{
    public enum GrantType
    {
        /// <summary>
        /// Password type for password token exchange
        /// </summary>
        password,

        /// <summary>
        /// Refresh token type for refreshing authorization type
        /// </summary>
        refresh_token
    }
}
