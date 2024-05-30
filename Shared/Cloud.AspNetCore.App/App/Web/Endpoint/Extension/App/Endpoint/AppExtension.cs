namespace Cloud.Web.Endpoint.API
{
    using Microsoft.Extensions.DependencyModel;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    // to set application dependencies
    public static partial class AppExtension
    {
        internal static IServiceCollection SetAppDependencies(this IServiceCollection source, IEnumerable<string> assemblyNames)
        {
            var assemblies = GetRequiredAssemblies(assemblyNames);
            source
            .SetAppServiceDependencies(assemblies)
            .SetDataAccessDependencies(assemblies)
            .SetCustomDependencies(assemblies);
            return source;
        }

        private static List<Assembly> GetRequiredAssemblies(IEnumerable<string> assemblyNames)
        {
            var result = new List<Assembly>();
            var libraries = DependencyContext.Default?.RuntimeLibraries;
            foreach (var item in libraries)
            {
                if (item.Name is "System.Composition")
                {

                }
                var isCandidate = IsCandidateAssemblyToLoad(item, assemblyNames);
                if (isCandidate)
                    result.Add(Assembly.Load(new AssemblyName(item.Name)));
            }
            return result;
        }

        private static bool IsCandidateAssemblyToLoad(RuntimeLibrary library, IEnumerable<string> assemblyNames)
        => assemblyNames.Any(library.Name.Contains) || library.Dependencies.Any(e => assemblyNames.Any(a => e.Name.Contains(a)));
    }
}