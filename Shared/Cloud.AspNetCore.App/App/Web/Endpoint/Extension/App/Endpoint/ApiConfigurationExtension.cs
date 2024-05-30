namespace Cloud.Web.Endpoint.API;

using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

// ENTRY
public static partial class ApiConfigurationExtension
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection source, IEnumerable<string> assemblyNames)
    {
        source.AddControllers(); // to be able to have web api.
        source.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();/*.AddFluentValidation(). add fluent validation is deprecated.*/
        source.SetAppDependencies(assemblyNames);
        return source;
    }
}