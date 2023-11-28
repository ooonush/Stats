using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Stats
{
    public static class TypeUtils
    {
        private class SimpleTypeComparer : IEqualityComparer<Type>
        {
            public bool Equals(Type x, Type y)
            {
                return x.Assembly == y.Assembly &&
                       x.Namespace == y.Namespace &&
                       x.Name == y.Name;
            }

            public int GetHashCode(Type obj)
            {
                return obj.GetHashCode();
            }
        }
        
        public static MethodInfo GetGenericMethod(this Type type, string name, BindingFlags bindingFlags, Type[] parameterTypes)
        {
            MethodInfo[] methods = type.GetMethods(bindingFlags);
            
            
            foreach (MethodInfo method in methods.Where(m => m.Name == name))
            {
                Type[] methodParameterTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();
            
                if (methodParameterTypes.SequenceEqual(parameterTypes, new SimpleTypeComparer()))
                {
                    return method;
                }
            }

            return null;
        }
    }
}