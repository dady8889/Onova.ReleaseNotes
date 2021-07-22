using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Onova.Services;

namespace Onova.ReleaseNotes
{
    public static class ReleaseNotesExtensions
    {
        /// <summary>
        /// Downloads the release notes (.rn) for the given version.
        /// Can only be used with <see cref="Onova.Services.WebPackageResolver"/>.
        /// </summary>
        public static async Task<string> GetReleaseNotes(this UpdateManager mgr, Version version, CancellationToken cancellationToken = default)
        {
            var resolver = mgr.GetFieldValue<IPackageResolver>("_resolver");

            if (!(resolver is WebPackageResolver webPackageResolver))
                throw new NotImplementedException("Release notes are available only for WebPackageResolver.");

            var httpClient = webPackageResolver.GetFieldValue<HttpClient>("_httpClient");

            var x = await webPackageResolver.InvokeMethod<Task<IReadOnlyDictionary<Version, string>>>("GetPackageVersionUrlMapAsync", cancellationToken);

            var targetUrl = x[version];
            var extensionPos = targetUrl.LastIndexOf('.');
            targetUrl = targetUrl.Substring(0, extensionPos) + ".rn";

            var releaseNotes = await httpClient.GetStringAsync(targetUrl, cancellationToken);
            return releaseNotes;
        }
    }
}
