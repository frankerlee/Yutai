﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Shared
{
    /// <summary>
    /// Extension methods for Attribute class.
    /// </summary>
    public static class AttributeHelper
    {
        /// <summary>
        /// Gets value of the specified attribute for type.
        /// </summary>
        /// <remarks>From http ://stackoverflow.com/questions/2656189/how-do-i-read-an-attribute-on-a-class-at-runtime</remarks>
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;

            if (att != null)
            {
                return valueSelector(att);
            }

            return default(TValue);
        }

        public static T GetAttribute<T>(this PropertyInfo prop)
            where T : Attribute
        {
            return Attribute.GetCustomAttribute(prop, typeof(T)) as T;
        }

        public static T GetAttribute<T>(Type type)
            where T : Attribute
        {
            return Attribute.GetCustomAttribute(type, typeof(T)) as T;
        }

        public static bool HasAttribute<T>(Type type)
            where T : Attribute
        {
            return GetAttribute<T>(type) != null;
        }
    }
}