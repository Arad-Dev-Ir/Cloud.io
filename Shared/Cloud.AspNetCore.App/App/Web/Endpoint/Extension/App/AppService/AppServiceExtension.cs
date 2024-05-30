namespace Cloud.Web.Endpoint.API
{
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;
    using Web.Core.AppService;
    using Web.Core.Contract;

    // application service dependencies
    public static partial class AppServiceExtension
    {
        internal static IServiceCollection SetAppServiceDependencies(this IServiceCollection source, IEnumerable<Assembly> assemblies)
        => source
            .SetCommandHandlers(assemblies)
            .SetCommandDispatcherValidators()
            .SetQueryHandlers(assemblies)
            .SetQueryDispatcherValidators()
            .SetEventHandlers(assemblies)
            .SetEventDispatcherValidators()
            .SetFluentVaidators(assemblies);

        private static IServiceCollection SetCommandHandlers(this IServiceCollection source, IEnumerable<Assembly> assemblies)
        => source.SetWithLifetime(assemblies, Lifetime.Transient, typeof(ICommandHandler<>), typeof(ICommandHandler<,>));

        private static IServiceCollection SetCommandDispatcherValidators(this IServiceCollection source)
        {
            source.AddTransient<CommandDispatcher>();
            source.AddTransient<CommandModelValidator>();
            source.AddTransient<CommandRequestValidator>();

            source.AddTransient<CommandPipeline>(e =>
            {
                var dispatcher = e.GetRequiredService<CommandDispatcher>();
                var modelValidator = e.GetRequiredService<CommandModelValidator>();
                var requestValidator = e.GetRequiredService<CommandRequestValidator>();

                requestValidator
                .Set(modelValidator)
                .Set(dispatcher);

                var result = requestValidator;
                return result;
            });

            return source;
        }

        private static IServiceCollection SetQueryHandlers(this IServiceCollection source, IEnumerable<Assembly> assemblies)
        => source.SetWithLifetime(assemblies, Lifetime.Transient, typeof(IQueryHandler<,>));

        private static IServiceCollection SetQueryDispatcherValidators(this IServiceCollection source)
        {
            source.AddTransient<QueryDispatcher>();
            source.AddTransient<QueryModelValidator>();
            source.AddTransient<QueryRequestValidator>();

            source.AddTransient<QueryPipeline>(e =>
            {
                var dispatcher = e.GetRequiredService<QueryDispatcher>();
                var modelValidator = e.GetRequiredService<QueryModelValidator>();
                var requestValidator = e.GetRequiredService<QueryRequestValidator>();

                requestValidator
                .Set(modelValidator)
                .Set(dispatcher);

                var result = requestValidator;
                return result;
            });

            return source;
        }

        private static IServiceCollection SetEventHandlers(this IServiceCollection source, IEnumerable<Assembly> assemblies)
        => source.SetWithLifetime(assemblies, Lifetime.Transient, typeof(IEventHandler<>));

        private static IServiceCollection SetEventDispatcherValidators(this IServiceCollection source)
        {
            source.AddTransient<EventDispatcher>();
            source.AddTransient<EventModelValidator>();
            source.AddTransient<EventRequestValidator>();

            source.AddTransient<EventPipeline>(e =>
            {
                var dispatcher = e.GetRequiredService<EventDispatcher>();
                var modelValidator = e.GetRequiredService<EventModelValidator>();
                var requestValidator = e.GetRequiredService<EventRequestValidator>();

                requestValidator
                .Set(modelValidator)
                .Set(dispatcher);

                var result = requestValidator;
                return result;
            });

            return source;
        }

        private static IServiceCollection SetFluentVaidators(this IServiceCollection source, IEnumerable<Assembly> assemblies)
        => source.AddValidatorsFromAssemblies(assemblies);
    }
}