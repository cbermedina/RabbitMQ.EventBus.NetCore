﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TypeExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetAssemblies(this Type type)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(type)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfalceType"></param>
        /// <param name="makeType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetMakeGenericType(this Type interfalceType, Type makeType)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(interfalceType.MakeGenericType(makeType))));
        }
    }
}
