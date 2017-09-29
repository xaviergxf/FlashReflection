namespace FlashReflection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ReflectionMethodList : IEnumerable<ReflectionMethod>
    {
        private Dictionary<string, List<ReflectionMethod>> _methods = new Dictionary<string, List<ReflectionMethod>>();
        internal ReflectionMethodList(IEnumerable<MethodInfo> methods, Type parent)
        {
            foreach (var method in methods)
            {
                var methodReflection = new ReflectionMethod(method, parent);
                if (!_methods.ContainsKey(methodReflection.Name))
                    _methods.Add(methodReflection.Name, new List<ReflectionMethod>());
                _methods[methodReflection.Name].Add(methodReflection);
            }
        }

        public IEnumerable<ReflectionMethod> this[string name]
        {
            get
            {
                return this.Where(a => a.Name == name);
            }
        }

        public IEnumerator<ReflectionMethod> GetEnumerator()
        {
            return _methods.Values.SelectMany(s=>s).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _methods.Values.SelectMany(s => s).GetEnumerator();
        }
    }
}