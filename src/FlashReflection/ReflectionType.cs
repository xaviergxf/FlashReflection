using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlashReflection
{
    public class ReflectionType
    {
        public IEnumerable<Attribute> Attributes { get; private set; }
        public ReflectionPropertyList Properties { get; private set; }

        public ReflectionPropertyList PropertiesDeclared { get; private set; }
        public Type Type { get; private set; }
        public IEnumerable<Type> BaseTypes { get; private set; }
        public ReflectionMethodList Methods { get; private set; }
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string AssemblyQualifiedName { get; private set; }

        internal ReflectionType(Type type)
        {
            Attributes = type.GetTypeInfo().GetCustomAttributes(true).OfType<Attribute>().ToList();
            Type = type;
            BaseTypes = GetBaseTypes(type);
            var propertyInfos = type.GetProperties();
            var propertyMethods = propertyInfos.SelectMany(a => new[] { a.GetGetMethod(), a.GetSetMethod() });
            Properties = new ReflectionPropertyList(propertyInfos);
            PropertiesDeclared = new ReflectionPropertyList(propertyInfos.Where(p => p.DeclaringType == type));
            Methods = new ReflectionMethodList(type.GetMethods().Except(propertyMethods), type);
            Name = type.Name;
            FullName = type.FullName;
            AssemblyQualifiedName = type.AssemblyQualifiedName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var objCastted = obj as ReflectionType;
            if (objCastted == null)
                return false;
            return Type.Equals(objCastted.Type);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        private IEnumerable<Type> GetBaseTypes(Type type, Type untilBaseType = null)
        {
            // is there any base type?
            if ((type == null) || (type.GetTypeInfo().BaseType == null))
            {
                yield break;
            }
            // return all inherited types
            var currentBaseType = type.GetTypeInfo().BaseType;
            while (currentBaseType != null && currentBaseType != typeof(object))
            {
                yield return currentBaseType;
                if (untilBaseType != null && untilBaseType == currentBaseType)
                    break;
                currentBaseType = currentBaseType.GetTypeInfo().BaseType;
            }
        }

    }
}