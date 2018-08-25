using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadInstalledModules(this IServiceCollection services, string contentRootPath)
        {
            var modules = new List<ModuleInfo>();
            var moduleRootFolder = new DirectoryInfo(Path.Combine(contentRootPath, "Modules"));
            var moduleFolders = moduleRootFolder.GetDirectories();

            foreach (var moduleFolder in moduleFolders)
            {
                Assembly moduleMainAssembly = null;
                var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.FullName, "bin"));
                if (binFolder.Exists)
                {
                    moduleMainAssembly = LoadModule(moduleFolder, binFolder);
                }

                if (moduleMainAssembly == null)
                {
                    moduleMainAssembly = Assembly.Load(new AssemblyName(moduleFolder.Name));
                }

                modules.Add(new ModuleInfo
                {
                    Name = moduleFolder.Name,
                    Assembly = moduleMainAssembly,
                    Path = moduleFolder.FullName
                });
            } 

            return services;
        }

        private static Assembly LoadModule(DirectoryInfo moduleFolder, DirectoryInfo binFolder)
        {
            Assembly moduleMainAssembly = null;

            foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
            {
                Assembly assembly;
                try
                {
                    assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                }
                catch (FileLoadException)
                { 
                    assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));

                    if (assembly == null)
                    {
                        throw;
                    }

                    var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                    string loadedAssemblyVersion = fvi.FileVersion;

                    fvi = FileVersionInfo.GetVersionInfo(file.FullName);
                    string tryToLoadAssemblyVersion = fvi.FileVersion;
                     
                    if (tryToLoadAssemblyVersion != loadedAssemblyVersion)
                    {
                        throw new Exception($"Failed to load {file.FullName} {tryToLoadAssemblyVersion}");
                    }
                }

                if (assembly.FullName.Contains(moduleFolder.Name))
                {
                    moduleMainAssembly = assembly;
                }
            }

            return moduleMainAssembly;
        }
    }
}
