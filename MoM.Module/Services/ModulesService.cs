using MoM.Module.Interfaces;
using System.Collections.Generic;
using System.Linq;
using MoM.Module.Dtos;
using System.Reflection;
using MoM.Module.Managers;

namespace MoM.Module.Services
{
    public class ModulesService : IModuleService
    {
        public static IEnumerable<Assembly> Assemblies { get; set; }
        public IEnumerable<ExtensionInfoDto> GetInstalledModules()
        {
            foreach (IModule module in AssemblyManager.GetModules)
            {
                var info = module.Info;
            }
            return AssemblyManager.GetModules.Select(m => m.Info);
        }
    }
}
