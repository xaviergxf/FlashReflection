using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections;

namespace FlashReflection.Extensions
{
    public static class ObjectExtensions
    {
        public static void MergeFrom(this Object primary, Object secondary, string[] pPropertyNames, out IEnumerable<string> changedProperties)
        {
            changedProperties = new List<string>();
            var prim_props = ReflectionCache.Instance.GetReflectionType(primary.GetType()).Properties.Where(w=>pPropertyNames.Contains(w.Name));
            var sec_props = ReflectionCache.Instance.GetReflectionType(secondary.GetType()).Properties.Where(w => pPropertyNames.Contains(w.Name)).ToDictionary(k => k.Name, v => v);
            foreach (var pp in prim_props)
            {
                var secValue = pp.GetValue(secondary);
                var priValue = pp.GetValue(primary);
                if (!object.Equals(priValue, secValue))
                    ((List<string>)changedProperties).Add(pp.Name);
                pp.SetValue(primary, secValue);
            }
        }

        public static void MergeFrom(this Object primary, Object secondary)
        {
            IEnumerable<string> changedProperties;
            MergeFrom(primary, secondary, out changedProperties);
        }

        public static void MergeFrom(this Object primary, Object secondary, out IEnumerable<string> changedProperties)
        {
            var prim_props = ReflectionCache.Instance.GetReflectionType(primary.GetType()).Properties;
            var sec_props = ReflectionCache.Instance.GetReflectionType(secondary.GetType()).Properties;
            var matched_props =
                (from pp in prim_props
                 join ps in sec_props on new { pp.Name, pp.PropertyType } equals new { ps.Name, ps.PropertyType }
                 where ps.HasGet && pp.HasSet
                 select pp.Name
                ).ToArray();
            MergeFrom(primary, secondary, matched_props, out changedProperties);
        }
    }
}
