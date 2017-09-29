using FlashReflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace FlashReflection
{
    public class ReflectionMethod
    {
        private readonly MethodInfo _method;
        public Type ReturnType { get; private set; }
        public Type ParentType { get; private set; }
        public string Name { get; private set; }
        public string FullName
        {
            get
            {
                return this.ParentType.FullName + "." + Name;
            }
        }
        public ParameterInfo[] Parameters { get; private set; }
        public IEnumerable<Attribute> Attributes { get; private set; }

        internal ReflectionMethod(MethodInfo method, Type parent)
        {
            _method = method??throw new ArgumentNullException(nameof(method));
            ParentType = parent??throw new ArgumentNullException(nameof(parent));
            ReturnType = _method.ReturnType;
            Attributes = _method.GetCustomAttributes(true).OfType<Attribute>();
            Name = _method.Name;
            Parameters = method.GetParameters();
        }

        public object Invoke(object obj, params object[] parameters)
        {
            return _method.Invoke(obj, parameters);
        }

        public override bool Equals(object obj)
        {
            return _method.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _method.GetHashCode();
        }
    }
}
