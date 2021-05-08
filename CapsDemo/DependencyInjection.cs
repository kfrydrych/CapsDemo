using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CapsDemo
{
    public static class DependencyInjection
    {
        public static void AddTransientTypesImplementing<TInterface>(this IServiceCollection services)
        {
            var type = typeof(TInterface);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) &&
                            p.IsInterface == false &&
                            p.IsAbstract == false
                );

            foreach (var t in types)
                services.AddTransient(t);
        }
    }
}