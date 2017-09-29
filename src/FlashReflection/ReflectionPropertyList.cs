using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlashReflection
{
    public class ReflectionPropertyList: IEnumerable<ReflectionProperty>
    {
        private Dictionary<string, ReflectionProperty> _properties = new Dictionary<string, ReflectionProperty>();
        internal ReflectionPropertyList(IEnumerable<PropertyInfo> properties)
        {
            foreach (var prop in properties)
            {
                var reflectionProperty = new ReflectionProperty(prop);
                _properties.Add(reflectionProperty.Name, reflectionProperty);
            }
        }

        public ReflectionProperty this[string name]
        {
            get
            {
                ReflectionProperty result;
                if (!_properties.TryGetValue(name, out result))
                    return null;
                return result;
            }
        }

        public IEnumerator<ReflectionProperty> GetEnumerator()
        {
            return _properties.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _properties.Values.GetEnumerator();
        }
    }
}