using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LuKaSo.Zonky.Common
{
    public class ZonkyResponseResolverFactory
    {
        /// <summary>
        /// Create resolver
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="isAuthorized"></param>
        /// <returns></returns>
        public virtual ZonkyResponseResolver<Task> Create(JsonSerializerSettings settings, bool isAuthorized = false)
        {
            return new ZonkyResponseResolver<Task>(settings, isAuthorized);
        }

        /// <summary>
        /// Create generic resolver
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings"></param>
        /// <param name="isAuthorized"></param>
        /// <returns></returns>
        public virtual ZonkyResponseResolver<T> Create<T>(JsonSerializerSettings settings, bool isAuthorized = false)
        {
            return new ZonkyResponseResolver<T>(settings, isAuthorized);
        }
    }
}
