using System;
using System.Linq;
using System.Reflection;

namespace AInspector
{
    public static class TypeUtils
    {
        private const string ArrayPropertySubstring = ".Array.data[";

        public static Type ExtractTypeFromString(string typeName)
        {
            if (string.IsNullOrEmpty(typeName)) return null;

            string[] splitFieldTypename = typeName.Split(' ');
            return Assembly.Load(splitFieldTypename[0]).GetType(splitFieldTypename[1]);
        }

        public static bool IsFinalNonGenericAssignableType(Type type)
        {
            return !type.IsAbstract && !type.IsInterface && !type.IsGenericType && type.GetConstructors()
                .Any(c => c.IsPublic && c.GetParameters().Length == 0 && !c.IsStatic);
        }
    }
}