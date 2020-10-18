using System;
using System.IO;
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
            // Docker compose mounts this folder under /root/.config
            var isDocker = IsDocker();
            var sharedFolder = GetConfigPath(env);

            // Disable reload on change, for some reason this is very slow on WSL
            // https://github.com/dotnet/runtime/issues/27272
            config.AddJsonFile(Path.Combine(sharedFolder, "appsettings.json"), optional: false, reloadOnChange: !isDocker);
            config.AddJsonFile(Path.Combine(sharedFolder, $"appsettings.{env.EnvironmentName}.json"), optional: false, reloadOnChange: !isDocker);
#endif
        }

        public static bool IsDocker()
        {
            // Detect docker. See: https://www.hanselman.com/blog/DetectingThatANETCoreAppIsRunningInADockerContainerAndSkippableFactsInXUnit.aspx
            return Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        }

        public static string GetConfigPath(IHostEnvironment env)
        {
            if(env.IsDevelopment()) return IsDocker() ? "/root/.config" : Path.Combine(env.ContentRootPath, "../../../config");
            
            return env.ContentRootPath;
        }
    }
}
