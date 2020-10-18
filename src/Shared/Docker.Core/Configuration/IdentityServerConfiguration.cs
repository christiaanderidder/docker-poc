using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Core.Configuration
{
    public class IdentityServerConfiguration
    {
        public const string Section = "IdentityServer";

        public string? Host { get; set; }
    }
}
