﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Shared
{
    public static class ReflectionHelper
    {
        public static object GetInstanceField(object instance, string fieldName)
        {
            const BindingFlags bindFlags =
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            FieldInfo field = instance.GetType().GetField(fieldName, bindFlags);
            return field != null ? field.GetValue(instance) : null;
        }

        public static IEnumerable<Type> GetDerivedTypes(this Assembly assembly, Type type)
        {
            return assembly.GetTypes().Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);
        }
    }
}