using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using FastMember;
using Sigil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlashReflection.Benchmark
{
    public class Program
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(new MemoryDiagnoser());
            }
        }

        private static BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        private static string propertyName = "Host";

        private static TestUri testUri;
        private static Object @object;
        private static Type @class;
        private static PropertyInfo property;
        private static TypeAccessor accessor; // FastMember
        private static ReflectionProperty flashProperty;
        private static Func<TestUri, object> expressionTreeGetter;
        private static Action<TestUri, object> expressionTreeSetter;
        private static ReflectionType flashReflectionType;
        private static Func<TestUri, string> getter;
        private static Action<TestUri, string> setter;

        public static Func<TestUri, string> getDelegate;
        public static Action<TestUri, string> setDelegate;
        public static Delegate getDelegateDynamic, setDelegateDynamic;

        //private static bool allowNonPublicFieldAccess = false;
        private static bool allowNonPublicFieldAccess = true;

        static Program()
        {
            testUri = new TestUri("SomeHost");
            @object = testUri;
            @class = testUri.GetType();
            property = @class.GetProperty(propertyName, bindingFlags);

            // Using FastMember - https://github.com/mgravell/fast-member
            accessor = TypeAccessor.Create(@class, allowNonPublicAccessors: allowNonPublicFieldAccess);

            flashReflectionType = ReflectionCache.Instance.GetReflectionType<TestUri>();
            flashProperty = flashReflectionType.Properties[propertyName];

            if (flashProperty == null)
                throw new NullReferenceException("Flash property is null");
            expressionTreeGetter = CompiledTreePropertyInfo.GetValueGetter<TestUri>(property);
            expressionTreeSetter = CompiledTreePropertyInfo.GetValueSetter<TestUri>(property);
            var funcType = typeof(Func<TestUri, string>);
            getDelegate = (Func<TestUri, string>)Delegate.CreateDelegate(funcType, property.GetMethod);
            getDelegateDynamic = Delegate.CreateDelegate(funcType, property.GetMethod);

            var actionType = typeof(Action<TestUri,string>);
            setDelegate = (Action<TestUri, string>)Delegate.CreateDelegate(actionType, property.SetMethod);
            setDelegateDynamic = Delegate.CreateDelegate(actionType, property.SetMethod);

            var setterEmiter = Emit<Action<TestUri, string>>
                .NewDynamicMethod("SetTestUriProperty")
                .LoadArgument(0)
                .LoadArgument(1)
                .Call(property.SetMethod)
                .Return();
            setter = setterEmiter.CreateDelegate();

            var getterEmiter = Emit<Func<TestUri, string>>
                .NewDynamicMethod("GetTestUriProperty")
                .LoadArgument(0)
                .Call(property.GetMethod)
                .Return();
            getter = getterEmiter.CreateDelegate();
        }

        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        [Benchmark(Baseline = true)]
        public string GetViaProperty()
        {
            return testUri.PublicHost;
        }

        [Benchmark]
        public string GetViaDelegate()
        {
            return getDelegate(testUri);
        }

        [Benchmark]
        public string GetViaILEmit()
        {
            return getter(testUri);
        }

        [Benchmark]
        public string GetViaCompiledExpressionTrees()
        {
            return (string) expressionTreeGetter(testUri);
        }

        [Benchmark]
        public string GetViaFastMember()
        {
            return (string)accessor[testUri, "PublicHost"];
        }

        [Benchmark]
        public string GetViaFlashReflection()
        {
            return (string)flashProperty.GetValue(testUri);
        }

        [Benchmark]
        public string GetViaReflectionWithCaching()
        {
            return (string)property.GetValue(testUri, null);
        }

        [Benchmark]
        public string GetViaReflection()
        {
            Type @class = testUri.GetType();
            PropertyInfo property = @class.GetProperty(propertyName, bindingFlags);
            return (string)property.GetValue(testUri, null);
        }

        [Benchmark]
        public string GetViaDelegateDynamicInvoke()
        {
            return (string)getDelegateDynamic.DynamicInvoke(testUri);
        }

        [Benchmark]
        public void SetViaProperty()
        {
            testUri.PublicHost = "Testing";
        }

        [Benchmark]
        public void SetViaDelegate()
        {
            setDelegate(testUri, "Testing");
        }

        [Benchmark]
        public void SetViaILEmit()
        {
            setter(testUri, "Testing");
        }

        [Benchmark]
        public void SetViaCompiledExpressionTrees()
        {
            expressionTreeSetter(testUri, "Testing");
        }

        [Benchmark]
        public void SetViaFastMember()
        {
            accessor[testUri, "PublicHost"] = "Testing";
        }

        [Benchmark]
        public void SetViaFlashReflection()
        {
            flashProperty.SetValue(testUri, "Testing");
        }

        [Benchmark]
        public void SetViaReflectionWithCaching()
        {
            property.SetValue(testUri, "Testing", null);
        }

        [Benchmark]
        public void SetViaReflection()
        {
            Type @class = testUri.GetType();
            PropertyInfo property = @class.GetProperty(propertyName, bindingFlags);
            property.SetValue(testUri, "Testing", null);
        }

        [Benchmark]
        public void SetViaDelegateDynamicInvoke()
        {
            setDelegateDynamic.DynamicInvoke(testUri, "Testing");
        }
    }
}
