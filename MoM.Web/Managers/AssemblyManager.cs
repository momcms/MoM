using Microsoft.Extensions.PlatformAbstractions;
using MoM.Web.Loaders;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MoM.Web.Managers
{
    public static class AssemblyManager
    {
        public static IEnumerable<Assembly> GetAssemblies(string path, IAssemblyLoaderContainer assemblyLoaderContainer, IAssemblyLoadContextAccessor assemblyLoadContextAccessor, ILibraryManager libraryManager)
        {
            List<Assembly> assemblies = new List<Assembly>();

            IAssemblyLoadContext assemblyLoadContext = assemblyLoadContextAccessor.Default;

            using (assemblyLoaderContainer.AddLoader(new DirectoryAssemblyLoader(path, assemblyLoadContext)))
            {
                foreach (string extensionPath in Directory.EnumerateFiles(path, "*.dll"))
                {
                    string extensionFilename = Path.GetFileNameWithoutExtension(extensionPath);

                    assemblies.Add(assemblyLoadContext.Load(extensionFilename));
                }
            }

            // We must not load all of the assemblies
            //foreach (Library library in libraryManager.GetLibraries())
            //    if (IsCandidateLibrary(libraryManager, library))
            //        assemblies.AddRange(library.Assemblies.Select(an => assemblyLoadContext.Load(an)));

            return assemblies;
        }

        //private static bool IsCandidateLibrary(ILibraryManager libraryManager, Library library)
        //{
        //    if (library.Dependencies.Any(d => d.Contains("ExtCore.Infrastructure")))
        //        return true;

        //    return false;
        //}
    }
}
