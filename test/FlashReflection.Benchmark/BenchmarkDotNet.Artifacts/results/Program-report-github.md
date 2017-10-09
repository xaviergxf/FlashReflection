``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 7 SP1 (6.1.7601)
Processor=Intel Core i7-3770 CPU 3.40GHz (Ivy Bridge), ProcessorCount=8
Frequency=3312949 Hz, Resolution=301.8459 ns, Timer=TSC
  [Host]     : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2114.0
  DefaultJob : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2114.0


```
 |                        Method |          Mean |     Error |    StdDev | Scaled | ScaledSD |
 |------------------------------ |--------------:|----------:|----------:|-------:|---------:|
 |                GetViaProperty |     0.0043 ns | 0.0094 ns | 0.0078 ns |      ? |        ? |
 |                GetViaDelegate |     1.8696 ns | 0.0128 ns | 0.0113 ns |      ? |        ? |
 |                  GetViaILEmit |     3.7570 ns | 0.0318 ns | 0.0282 ns |      ? |        ? |
 | GetViaCompiledExpressionTrees |    11.2991 ns | 0.0562 ns | 0.0526 ns |      ? |        ? |
 |              GetViaFastMember |    33.9544 ns | 0.0388 ns | 0.0324 ns |      ? |        ? |
 |         GetViaFlashReflection |     7.2982 ns | 0.0214 ns | 0.0190 ns |      ? |        ? |
 |   GetViaReflectionWithCaching |   125.9705 ns | 0.2682 ns | 0.2509 ns |      ? |        ? |
 |              GetViaReflection |   192.1607 ns | 0.2916 ns | 0.2585 ns |      ? |        ? |
 |   GetViaDelegateDynamicInvoke |   944.1348 ns | 2.5528 ns | 2.2630 ns |      ? |        ? |
 |                SetViaProperty |     1.9170 ns | 0.0256 ns | 0.0240 ns |      ? |        ? |
 |                SetViaDelegate |    10.0766 ns | 0.0164 ns | 0.0145 ns |      ? |        ? |
 |                  SetViaILEmit |    10.2032 ns | 0.0304 ns | 0.0254 ns |      ? |        ? |
 | SetViaCompiledExpressionTrees |    10.0388 ns | 0.1336 ns | 0.1185 ns |      ? |        ? |
 |              SetViaFastMember |    35.9679 ns | 0.2411 ns | 0.2256 ns |      ? |        ? |
 |         SetViaFlashReflection |     6.7015 ns | 0.0136 ns | 0.0114 ns |      ? |        ? |
 |   SetViaReflectionWithCaching |   204.5866 ns | 0.8273 ns | 0.7739 ns |      ? |        ? |
 |              SetViaReflection |   276.0748 ns | 0.6922 ns | 0.5404 ns |      ? |        ? |
 |   SetViaDelegateDynamicInvoke | 1,002.8511 ns | 5.1354 ns | 4.8037 ns |      ? |        ? |
