using LuKaSo.Zonky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuKaSo.Zonky.Extesions
{
    public static class UriExtensions
    {
        /// <summary>
        /// Append relative path to base address
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static Uri Append(this Uri basePath, string relativePath)
        {
            relativePath = relativePath.TrimStart(new[] { '/', '\\' });
            var basePathString = basePath.ToString().TrimEnd(new[] { '/', '\\' });

            return new Uri(basePathString + '/' + relativePath);
        }

        /// <summary>
        /// Attach url ecoded parameters to address
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Uri AttachQueryParameters(this Uri uri, IReadOnlyDictionary<string, string> parameters)
        {
            var queryVars = HttpUtility.ParseQueryString(uri.Query);

            foreach (var kpv in parameters)
            {
                if (queryVars.AllKeys.Contains(kpv.Key))
                {
                    queryVars.Set(kpv.Key, kpv.Value);
                    continue;
                }

                queryVars.Add(kpv.Key, kpv.Value);
            }

            if (queryVars.Count > 0)
            {
                uri = new Uri(uri.GetLeftPart(UriPartial.Path) + "?" + queryVars.ToString());
            }

            return uri;
        }

        /// <summary>
        /// Attach filter options to query string
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static Uri AppendFilterOptions(this Uri uri, FilterOptions options)
        {
            if (options == null)
            {
                return uri;
            }

            return uri.AttachQueryParameters(options.ToDictionary());
        }
    }
}
