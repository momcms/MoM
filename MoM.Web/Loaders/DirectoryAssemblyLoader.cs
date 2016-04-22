using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;
using System.Reflection;

namespace MoM.Web.Loaders
{
    public class DirectoryAssemblyLoader : IAssemblyLoader
    {
        private readonly string DirectoryPath;
        private readonly IAssemblyLoadContext AssemblyLoadContext;

        public DirectoryAssemblyLoader(string path, IAssemblyLoadContext assemblyLoadContext)
        {
            DirectoryPath = path;
            AssemblyLoadContext = assemblyLoadContext;
        }

        public Assembly Load(AssemblyName assemblyName)
        {
            return AssemblyLoadContext.LoadFile(Path.Combine(DirectoryPath, assemblyName + ".dll"));
        }

        public IntPtr LoadUnmanagedLibrary(string name)
        {
            throw new NotImplementedException();
        }
    }
}
