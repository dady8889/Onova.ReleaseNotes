using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Onova.ReleaseNotes
{
    internal static class HttpUtils
    {
        public static async Task<string> GetStringAsync(
            this HttpClient client,
            string requestUri,
            CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(
                requestUri,
                HttpCompletionOption.ResponseContentRead,
                cancellationToken
            ))
            {

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
