using MoM.Module.Interfaces;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MoM.Module.Enums;

namespace MoM.Module.Managers
{
    public static class ExtensionManager
    {
        private static IEnumerable<Assembly> assemblies;
        private static IEnumerable<IModule> modules;
        private static InstallationStatus InstallStatus { get; set; }

        public static IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (ExtensionManager.assemblies == null)
                    throw new InvalidOperationException("Assemblies not set");

                return ExtensionManager.assemblies;
            }
        }

        public static IEnumerable<IModule> Extensions
        {
            get
            {
                if (ExtensionManager.modules == null)
                    ExtensionManager.modules = ExtensionManager.GetInstances<IModule>();

                return ExtensionManager.modules;
            }
        }

        public static void SetAssemblies(IEnumerable<Assembly> assemblies, InstallationStatus installStatus)
        {
            InstallStatus = installStatus;
            ExtensionManager.assemblies = assemblies;
        }

        public static Type GetImplementation<T>()
        {
            return ExtensionManager.GetImplementation<T>(null);
        }

        public static Type GetImplementation<T>(Func<Assembly, bool> predicate)
        {
            IEnumerable<Type> implementations = ExtensionManager.GetImplementations<T>(predicate);

            if (implementations.Count() == 0)
                throw new ArgumentException("Implementation of " + typeof(T) + " not found");

            return implementations.FirstOrDefault();
        }

        public static IEnumerable<Type> GetImplementations<T>()
        {
            return ExtensionManager.GetImplementations<T>(null);
        }

        public static IEnumerable<Type> GetImplementations<T>(Func<Assembly, bool> predicate)
        {
            List<Type> implementations = new List<Type>();

            foreach (Assembly assembly in ExtensionManager.GetAssemblies(predicate))
                foreach (Type type in GetLoadableTypes(assembly)) // assembly.GetTypes())
                    if (typeof(T).GetTypeInfo().IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
                        implementations.Add(type);

            return implementations;
        }

        public static T GetInstance<T>()
        {
            return ExtensionManager.GetInstance<T>(null);
        }

        public static T GetInstance<T>(Func<Assembly, bool> predicate)
        {
            IEnumerable<T> instances = ExtensionManager.GetInstances<T>(predicate);

            if (instances.Count() == 0)
                throw new ArgumentException("Instance of " + typeof(T) + " can't be created");

            return instances.FirstOrDefault();
        }

        public static IEnumerable<T> GetInstances<T>()
        {
            return ExtensionManager.GetInstances<T>(null);
        }

        public static IEnumerable<T> GetInstances<T>(Func<Assembly, bool> predicate)
        {
            List<T> instances = new List<T>();

            foreach (Type implementation in ExtensionManager.GetImplementations<T>())
            {
                T instance = (T)Activator.CreateInstance(implementation);

                instances.Add(instance);
            }

            return instances;
        }

        private static IEnumerable<Assembly> GetAssemblies(Func<Assembly, bool> predicate)
        {
            if (predicate == null)
                return ExtensionManager.Assemblies;

            return ExtensionManager.Assemblies.Where(predicate);
        }

        public static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            // TODO: Argument validation
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in e.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.ToString()))
                        {
                            sb.AppendLine("Error Log:");
                            sb.AppendLine(exFileNotFound.ToString());
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                return e.Types.Where(t => t != null);
            }
        }
    }
}
