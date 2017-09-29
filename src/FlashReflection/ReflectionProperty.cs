using FlashReflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlashReflection
{
    public class ReflectionProperty
    {
        public IEnumerable<Attribute> Attributes { get; private set; }
        public string Name { get; private set; }
        public Type PropertyType { get; private set; }
        public bool HasGet { get; private set; } = false;
        public bool HasSet { get; private set; } = false;

        public PropertyInfo PropertyInfo { get; private set; }

        public Type GenericTypeDefinition { get; private set; }
        public Type[] GenericTypeArguments { get; private set; }

        private GetDelegate getHandler;
        private SetDelegate setHandler;

        internal ReflectionProperty(PropertyInfo property)
        {
            this.PropertyInfo = property;
            Attributes = this.PropertyInfo.GetCustomAttributes(true).OfType<Attribute>().ToList();
            Name = this.PropertyInfo.Name;
            PropertyType = PropertyInfo.PropertyType;
            if (property.PropertyType.GetTypeInfo().IsGenericType)
            {
                this.GenericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();
                this.GenericTypeArguments = property.PropertyType.GetGenericArguments();
            }

            var method = this.PropertyInfo.GetGetMethod() ?? this.PropertyInfo.GetGetMethod(true);
            if (method != null)
            {
                HasGet = true;
                getHandler = DelegateFactory.CreateGet(property);
            }

            method = this.PropertyInfo.GetSetMethod() ?? this.PropertyInfo.GetSetMethod(true);
            if (method != null)
            {
                HasSet = true;
                setHandler = DelegateFactory.CreateSet(property);
            }
        }

        public object GetValue(object from)
        {
            return getHandler(from);
        }

        public void SetValue(object to, object value)
        {
            setHandler(to, value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var objCastted = obj as ReflectionProperty;
            if (objCastted == null)
                return false;
            return PropertyInfo.Equals(objCastted.PropertyInfo);
        }

        public override int GetHashCode()
        {
            return PropertyInfo.GetHashCode();
        }

    }
}