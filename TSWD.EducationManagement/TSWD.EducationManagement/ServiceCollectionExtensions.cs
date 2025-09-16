using System.Reflection;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;

namespace TSWD.EducationManagement
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAllServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (!type.IsClass || type.IsAbstract)
                    continue;

                // Skip compiler-generated types (async state machines, closures, etc.)
                if (type.IsDefined(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false))
                    continue;

                var interfaces = type.GetInterfaces();
                foreach (var iface in interfaces)
                {
                    // Skip generic IRepository<> for separate registration
                    if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IRepository<>))
                        continue;

                    services.AddTransient(iface, type);
                }
            }
        }
    }
}
