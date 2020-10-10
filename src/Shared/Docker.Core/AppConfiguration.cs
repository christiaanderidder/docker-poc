using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Docker.Core
{
    public static class AppConfiguration
    {
        public static void ConfigureJsonConfig(IHostEnvironment env, IConfigurationBuilder config)
        {
#if DEBUG
            // For local builds, the config file will be loaded directly from the shared location
            // as it is linked virtually in the project file.
            // Debug builds running in docker have the solution dir mounted to /src

            // Detect docker. See: https://www.hanselman.com/blog/DetectingThatANETCoreAppIsRunningInADockerContainerAndSkippableFactsInXUnit.aspx
            var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

            var sharedFolder = isDocker ? "/src/" : Path.Combine(env.ContentRootPath, "../../");

            // Disable reload on change, for some reason this is very slow on WSL
            // https://github.com/dotnet/runtime/issues/27272
            config.AddJsonFile(Path.Combine(sharedFolder, "appsettings.json"), optional: false, reloadOnChange: !isDocker);
            config.AddJsonFile(Path.Combine(sharedFolder, $"appsettings.{env.EnvironmentName}.json"), optional: false, reloadOnChange: !isDocker);
#endif
        }
    }
}
