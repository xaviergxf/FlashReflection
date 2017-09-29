using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FlashReflection.Test
{
    public class PropertyTests
    {
        [Fact]
        public void CanGetValue()
        {
            var classAObject = new ClassA();
            classAObject.PropertyString1 = "123";

            var reflectionType = ReflectionCache.Instance.GetReflectionType<ClassA>();
            var propertyValue = reflectionType.Properties[nameof(ClassA.PropertyString1)].GetValue(classAObject);

            Assert.True(classAObject.PropertyString1.Equals(propertyValue));
        }

        [Fact]
        public void CanSetValue()
        {
            var classAObject = new ClassA();
            string strValue = "123";

            var reflectionType = ReflectionCache.Instance.GetReflectionType<ClassA>();
            reflectionType.Properties[nameof(ClassA.PropertyString1)].SetValue(classAObject, strValue);

            Assert.True(classAObject.PropertyString1 == strValue);
        }
    }
}
