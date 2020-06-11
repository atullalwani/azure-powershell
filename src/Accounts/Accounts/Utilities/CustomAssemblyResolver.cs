using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microsoft.Azure.Commands.Profile.Utilities
{
    public static class CustomAssemblyResolver
    {
        private static IDictionary<string, Version> NetFxPreloadAssemblies =
            new Dictionary<string, Version>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"System.Threading.Tasks.Extensions", new Version("4.2.0.0") },
                {"System.Runtime.CompilerServices.Unsafe", new Version("4.0.5.0") },
                {"System.Diagnostics.DiagnosticSource", new Version("4.0.4.0") }
            };
        public static void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        /// <summary>
        /// When the resolution of an assembly fails, if it's Newtonsoft.Json 9, redirect to 10
        /// </summary>
        public static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                assemblies
                AssemblyName name = new AssemblyName(args.Name);
                if (NetFxPreloadAssemblies.TryGetValue(args.Name, out Version version))
                {
                    if (version >= name.Version && version.Major == name.Version.Major)
                    {
                        string accountFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        string azureCorePath = Path.Combine(accountFolder, $"PreloadAssemblies/{args.Name}.dll");
                        return Assembly.LoadFrom(azureCorePath);
                    }
                }
            }
            catch
            {
            }
            return null;
        }
    }
}
