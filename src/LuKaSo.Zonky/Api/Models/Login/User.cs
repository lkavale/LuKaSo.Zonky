namespace LuKaSo.Zonky.Api.Models.Login
{
    public class User
    {
        /// <summary>
        /// User
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
