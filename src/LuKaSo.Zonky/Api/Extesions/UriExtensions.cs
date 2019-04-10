using System;
using System.Collections.Generic;
using System.Text;

namespace LuKaSo.Zonky.Api.Extesions
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

            return new Uri(basePathString + "/" + relativePath);
        }

        /// <summary>
        /// Attach url ecoded parameters to address
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Uri AttachQueryParameters(this Uri uri, Dictionary<string, string> parameters)
        {
            var stringBuilder = new StringBuilder();
            string str = "?";

            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                stringBuilder.Append(str + parameter.Key + "=" + parameter.Value);
                str = "&";
            }
            return new Uri(uri.ToString() + stringBuilder);
        }
    }
}
