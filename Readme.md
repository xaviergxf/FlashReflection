
FlashReflection - .NET reflection in a flash
=====================================
[![Build status](https://ci.appveyor.com/api/projects/status/o3yptfua91g3h6je/branch/master?svg=true)](https://ci.appveyor.com/project/xaviergxf/flashreflection/branch/master)

FlashReflection is a faster and easier way to use reflection in .NET. This library provides cache of types, properties, attributes and methods. 
It allows a much faster access to property values.


How to use:

```csharp
var classAReflected = ReflectionCache.Instance.GetReflectionType<ClassA>();
var valueOfProperty = classAReflected.Properties["Name"].GetValue(objectOfClassA);
```

Cache options can be set globally by doing the following before any call to ReflectionCache.Instance
```csharp
ReflectionCache.ReflectionCache = new MemoryCacheOptions();
```

When using ReflectionCache.Instance.GetReflectionType<ClassA> you can specify its cache entry options as well.
```csharp
ReflectionCache.Instance.GetReflectionType<ClassA>(new MemoryCacheEntryOptions());
```


Benchmark

<pre>
                        Method |          Mean |     Error |    StdDev | Scaled| ScaledSD |
------------------------------ |--------------:|----------:|----------:|------:|---------:|
                GetViaProperty |     0.0058 ns | 0.0091 ns | 0.0085 ns |      ?|        ? |
                GetViaDelegate |     1.8663 ns | 0.0119 ns | 0.0112 ns |      ?|        ? |
                  GetViaILEmit |     2.9481 ns | 0.0174 ns | 0.0154 ns |      ?|        ? |
 GetViaCompiledExpressionTrees |    11.2914 ns | 0.0271 ns | 0.0254 ns |      ?|        ? |
              GetViaFastMember |    33.8724 ns | 0.0973 ns | 0.0910 ns |      ?|        ? |
         <b>GetViaFlashReflection</b> |     <b>6.4690 ns</b> | <b>0.0160 ns</b> | <b>0.0142 ns</b> |      ?|        ? |
   GetViaReflectionWithCaching |   126.4189 ns | 0.2890 ns | 0.2703 ns |      ?|        ? |
              GetViaReflection |   192.1511 ns | 0.3489 ns | 0.3264 ns |      ?|        ? |
   GetViaDelegateDynamicInvoke |   954.5050 ns | 9.3122 ns | 8.7107 ns |      ?|        ? |
                SetViaProperty |     1.8824 ns | 0.0102 ns | 0.0091 ns |      ?|        ? |
                SetViaDelegate |     9.3983 ns | 0.0167 ns | 0.0157 ns |      ?|        ? |
                  SetViaILEmit |    10.3386 ns | 0.0643 ns | 0.0601 ns |      ?|        ? |
 SetViaCompiledExpressionTrees |     9.9524 ns | 0.0418 ns | 0.0391 ns |      ?|        ? |
              SetViaFastMember |    35.6829 ns | 0.1239 ns | 0.1159 ns |      ?|        ? |
         <b>SetViaFlashReflection</b> |     <b>6.7196 ns</b> | <b>0.0153 ns</b> | <b>0.0136 ns</b> |      ?|        ? |
   SetViaReflectionWithCaching |   205.2057 ns | 0.9305 ns | 0.8704 ns |      ?|        ? |
              SetViaReflection |   277.5734 ns | 1.9667 ns | 1.8396 ns |      ?|        ? |
   SetViaDelegateDynamicInvoke | 1,010.4607 ns | 8.6607 ns | 8.1012 ns |      ?|        ? |
</pre>