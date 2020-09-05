using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RabbitService
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypesImplementingInterface(this IEnumerable<Assembly> assemblies, Type type)
        {
            return assemblies.SelectMany(s => s.GetTypes())
                             .Where(p => type.IsAssignableFrom(p));
        }

        public static IEnumerable<Type> GetTypesImplementingGenericInterface(this IEnumerable<Assembly> assemblies, Type type)
        {
            return assemblies.SelectMany(s => s.GetTypes())
                              .Where(p => p.GetInterfaces().Any(x =>
                                          x.IsGenericType &&
                                          x.GetGenericTypeDefinition() == type));
        }
    }
}
