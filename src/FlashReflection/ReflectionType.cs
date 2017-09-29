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
        public ReflectionMethodList Methods { get; private set; }
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string AssemblyQualifiedName { get; private set; }

        internal ReflectionType(Type type)
        {
            this.Attributes = type.GetTypeInfo().GetCustomAttributes(true).OfType<Attribute>().ToList();
            this.Type = type;
            var propertyInfos = type.GetProperties();
            var propertyMethods = propertyInfos.SelectMany(a => new[] { a.GetGetMethod(), a.GetSetMethod() });
            this.Properties = new ReflectionPropertyList(propertyInfos);
            this.PropertiesDeclared = new ReflectionPropertyList(propertyInfos.Where(p => p.DeclaringType == type));
            this.Methods = new ReflectionMethodList(type.GetMethods().Except(propertyMethods), type);
            this.Name = type.Name;
            this.FullName = type.FullName;
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

    }
}