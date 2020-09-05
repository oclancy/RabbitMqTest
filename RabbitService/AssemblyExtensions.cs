using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RabbitService
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypesImplementingInterface( this IEnumerable<Assembly> assemblies, Type type )
        {
            return  assemblies.SelectMany(s => s.GetTypes())
                              .Where(p => type.IsAssignableFrom(p));
    }
    }
}
