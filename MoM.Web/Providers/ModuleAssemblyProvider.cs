using Microsoft.AspNet.Mvc.Infrastructure;
using MoM.Module.Managers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MoM.Web.Providers
{
    public class ModuleAssemblyProvider : IAssemblyProvider
    {
        private readonly DefaultAssemblyProvider DefaultAssemblyProvider;

        public ModuleAssemblyProvider(DefaultAssemblyProvider defaultAssemblyProvider)
        {
            DefaultAssemblyProvider = defaultAssemblyProvider;
        }

        public IEnumerable<Assembly> CandidateAssemblies
        {
            get
            {
                return DefaultAssemblyProvider.CandidateAssemblies.Concat(ModuleManager.GetAssemblies).Distinct();
            }
        }
    }
}
