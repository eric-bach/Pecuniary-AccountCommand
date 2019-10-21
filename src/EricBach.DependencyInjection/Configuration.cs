using System;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EricBach.DependencyInjection
{
    public static class Configuration
    {
        /// <summary>
        /// Returns the Type matching the className in the namespace for the application
        /// </summary>
        /// <param name="appDomain"></param>
        /// <param name="namespace"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static Type GetType(AppDomain appDomain, string @namespace, string className)
        {
            return appDomain.GetAssemblies()
                .SingleOrDefault(a => a.GetName().Name == @namespace)?.GetTypes()
                .FirstOrDefault(t => t.Name == className);
        }

        /// <summary>
        /// Automatically registers services for all classes that implement the specified baseInterface. This is acoomplished by using 
        /// the Scutor nuget package to scan for all classes that implement the specified baseInterface.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="baseClass">All classes within the same assembly as this base class will be considered for automatic configuration</param>
        /// <param name="baseInterface">Any classes that implement this interface will be automatically configured</param>
        public static void RegisterServices(IServiceCollection services, Type baseClass, Type baseInterface)
        {
            services.Scan(scan => scan
                .FromAssemblies(baseClass.GetTypeInfo().Assembly)
                .AddClasses(classes => classes.Where(x =>
                {
                    var allInterfaces = x.GetInterfaces();
                    return allInterfaces.Any(y => y.GetTypeInfo().IsGenericType &&
                                                  y.GetTypeInfo().GetGenericTypeDefinition() == baseInterface);
                }))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }

        public static IServiceCollection AddMediatorHandlers(this IServiceCollection services, Assembly assembly)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => t => sp.GetService(t));

            var classTypes = assembly.ExportedTypes.Select(t => IntrospectionExtensions.GetTypeInfo(t)).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<>)))
                {
                    services.AddTransient(handlerType.AsType(), type.AsType());
                }
                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                {
                    services.AddTransient(handlerType.AsType(), type.AsType());
                }
            }

            return services;
        }
    }
}
