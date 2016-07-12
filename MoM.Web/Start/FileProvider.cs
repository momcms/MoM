using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using MoM.Module.Managers;
using System.Collections.Generic;
using System.Linq;

namespace MoM.Web.Start
{
    public class FileProvider
    {
        public static IFileProvider CreateCompositeFileProvider(IHostingEnvironment hostingEnvironment)
        {
            IEnumerable<IFileProvider> fileProviders = new IFileProvider[] {
                hostingEnvironment.WebRootFileProvider
            };

            return new Providers.CompositeFileProvider(
              fileProviders.Concat(
                ExtensionManager.Assemblies.Select(a => new EmbeddedFileProvider(a, a.GetName().Name))
              )
            );
        }
    }
}
