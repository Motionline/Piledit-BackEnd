using PileditBackend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PileditBackendServer
{
    public static class ServerData
    {
        public static bool IsPublic { get; }

        internal static bool IsAccessable(IPAddress address)
            => address != null && (IsPublic || address.ToString() == "::1");

        internal static Dictionary<string, Project> ProjectList = new();
    }
}
