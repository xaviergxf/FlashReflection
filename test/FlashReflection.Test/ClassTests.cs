using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FlashReflection.Test
{
    public class ClassTests
    {
        public ClassTests()
        { }

        [Fact]
        public void TestProperties()
        {
            var classAReflected = ReflectionCache.Instance.GetReflectionType<ClassA>();

            Assert.True(classAReflected.Properties.Where(p => p.Name == nameof(ClassA.PropertyString1)).Count() == 1);
            Assert.True(classAReflected.Properties.Where(p => p.Name == nameof(ClassA.PropertyString2)).Count() == 1);
            Assert.True(classAReflected.Properties.Where(p => p.Name == nameof(ClassA.PropertyInt1)).Count() == 1);
            Assert.True(classAReflected.Properties.Where(p => p.Name == nameof(ClassA.PropertyInt2)).Count() == 1);
            Assert.True(classAReflected.Properties.Where(p => p.Name == nameof(ClassA.PropertyGuid1)).Count() == 1);
            Assert.True(classAReflected.Properties.Where(p => p.Name == nameof(ClassA.PropertyGuid2)).Count() == 1);
            Assert.True(classAReflected.Properties.Where(p => p.Name == nameof(ClassA.PropertyDateTime1)).Count() == 1);
        }

        [Fact]
        public void TestPropertiesDeclared()
        {
            var classAReflected = ReflectionCache.Instance.GetReflectionType<ClassA>();

            Assert.True(classAReflected.PropertiesDeclared.Where(p => p.Name == nameof(ClassA.PropertyString1)).Count() == 1);
            Assert.True(classAReflected.PropertiesDeclared.Where(p => p.Name == nameof(ClassA.PropertyString2)).Count() == 1);
            Assert.True(classAReflected.PropertiesDeclared.Where(p => p.Name == nameof(ClassA.PropertyInt1)).Count() == 1);
            Assert.True(classAReflected.PropertiesDeclared.Where(p => p.Name == nameof(ClassA.PropertyInt2)).Count() == 1);
            Assert.True(classAReflected.PropertiesDeclared.Where(p => p.Name == nameof(ClassA.PropertyGuid1)).Count() == 1);
            Assert.True(classAReflected.PropertiesDeclared.Where(p => p.Name == nameof(ClassA.PropertyGuid2)).Count() == 1);
            Assert.True(classAReflected.PropertiesDeclared.Where(p => p.Name == nameof(ClassA.PropertyDateTime1)).Count() == 1);
        }

        [Fact]
        public void TestName()
        {
            var classAReflected = ReflectionCache.Instance.GetReflectionType<ClassA>();

            Assert.True(classAReflected.Name == nameof(ClassA));
        }

        [Fact]
        public void TestAssemblyFullName()
        {
            var classAReflected = ReflectionCache.Instance.GetReflectionType<ClassA>();

            Assert.True(classAReflected.FullName == typeof(ClassA).FullName);
        }

        [Fact]
        public void TestAssemblyQualifiedName()
        {
            var classAReflected = ReflectionCache.Instance.GetReflectionType<ClassA>();

            Assert.True(classAReflected.AssemblyQualifiedName == typeof(ClassA).AssemblyQualifiedName);
        }

        [Fact]
        public void TestAttributes()
        {
            var classAReflected = ReflectionCache.Instance.GetReflectionType<ClassA>();

            Assert.True(classAReflected.Attributes.Count() == 2);
        }

        [Fact]
        public void TestMethods()
        {
            var classAReflected = ReflectionCache.Instance.GetReflectionType<ClassA>();

            Assert.True(classAReflected.Methods.Where(p => p.Name == nameof(ClassA.MethodReturnInt)).Count() == 1);
            Assert.True(classAReflected.Methods.Where(p => p.Name == nameof(ClassA.MethodVoid)).Count() == 1);
        }

    }
}
