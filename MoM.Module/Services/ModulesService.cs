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
            IEnumerable<IModule> modules = ExtensionManager.Extensions;
            foreach (IModule module in modules)
            {
                var info = module.Info;
            }
            return modules.Select(m => m.Info);
        }
    }
}
