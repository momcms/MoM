using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using MoM.Module.Managers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MoM.Web.Start
{
    public class Mvc
    {
        public static void AddMvcServices(IServiceCollection services)
        {
            // Default authentication policy will Require Authenticated User's 
            IMvcBuilder mvcBuilder = services.AddMvc(config => {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            List<MetadataReference> metadataReferences = new List<MetadataReference>();

            foreach (Assembly assembly in ExtensionManager.Assemblies)
            {
                mvcBuilder.AddApplicationPart(assembly);
                metadataReferences.Add(MetadataReference.CreateFromFile(assembly.Location));
            }

            mvcBuilder.AddRazorOptions(
              o =>
              {
                  foreach (Assembly assembly in ExtensionManager.Assemblies)
                      o.FileProviders.Add(new EmbeddedFileProvider(assembly, assembly.GetName().Name));

                  Action<RoslynCompilationContext> previous = o.CompilationCallback;

                  o.CompilationCallback = c =>
                  {
                      previous?.Invoke(c);

                      c.Compilation = c.Compilation.AddReferences(metadataReferences);
                  };
              }
            );
        }
    }
}
