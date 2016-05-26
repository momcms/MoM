//using MoM.Module.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Reflection;

//namespace MoM.Module.Managers
//{
//    public class AssemblyManager
//    {
//        private static IEnumerable<Assembly> Assemblies;
//        private static IEnumerable<IModule> Modules;

//        public static IEnumerable<Assembly> GetAssemblies
//        {
//            get
//            {
//                if (Assemblies == null)
//                    throw new InvalidOperationException("Assemblies not set");

//                return Assemblies;
//            }
//        }

//        public static IEnumerable<IModule> GetModules
//        {
//            get
//            {
//                if (Assemblies == null)
//                    throw new InvalidOperationException("Assemblies not set");

//                if (Modules == null)
//                {
//                    List<IModule> modules = new List<IModule>();
//                    foreach (Assembly assembly in Assemblies)
//                    {
//                        foreach (Type type in assembly.GetTypes())
//                        {
//                            if (typeof(IModule).IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
//                            {
//                                IModule module = (IModule)Activator.CreateInstance(type);
//                                modules.Add(module);
//                            }
//                        }
//                    }
//                    Modules = modules;
//                }
//                return Modules;
//            }
//        }

//        public static void SetAssemblies(IEnumerable<Assembly> assemblies)
//        {
//            Assemblies = assemblies;
//        }
//    }
//}
