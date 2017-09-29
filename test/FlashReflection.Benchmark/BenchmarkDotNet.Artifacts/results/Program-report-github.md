``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 7 SP1 (6.1.7601)
Processor=Intel Core i7-3770 CPU 3.40GHz (Ivy Bridge), ProcessorCount=8
Frequency=3312949 Hz, Resolution=301.8459 ns, Timer=TSC
  [Host]     : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2114.0
  DefaultJob : .NET Framework 4.7 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2114.0


```
 |                        Method |          Mean |     Error |    StdDev | Scaled | ScaledSD |
 |------------------------------ |--------------:|----------:|----------:|-------:|---------:|
 |                GetViaProperty |     0.0058 ns | 0.0091 ns | 0.0085 ns |      ? |        ? |
 |                GetViaDelegate |     1.8663 ns | 0.0119 ns | 0.0112 ns |      ? |        ? |
 |                  GetViaILEmit |     2.9481 ns | 0.0174 ns | 0.0154 ns |      ? |        ? |
 | GetViaCompiledExpressionTrees |    11.2914 ns | 0.0271 ns | 0.0254 ns |      ? |        ? |
 |              GetViaFastMember |    33.8724 ns | 0.0973 ns | 0.0910 ns |      ? |        ? |
 |         GetViaFlashReflection |     6.4690 ns | 0.0160 ns | 0.0142 ns |      ? |        ? |
 |   GetViaReflectionWithCaching |   126.4189 ns | 0.2890 ns | 0.2703 ns |      ? |        ? |
 |              GetViaReflection |   192.1511 ns | 0.3489 ns | 0.3264 ns |      ? |        ? |
 |   GetViaDelegateDynamicInvoke |   954.5050 ns | 9.3122 ns | 8.7107 ns |      ? |        ? |
 |                SetViaProperty |     1.8824 ns | 0.0102 ns | 0.0091 ns |      ? |        ? |
 |                SetViaDelegate |     9.3983 ns | 0.0167 ns | 0.0157 ns |      ? |        ? |
 |                  SetViaILEmit |    10.3386 ns | 0.0643 ns | 0.0601 ns |      ? |        ? |
 | SetViaCompiledExpressionTrees |     9.9524 ns | 0.0418 ns | 0.0391 ns |      ? |        ? |
 |              SetViaFastMember |    35.6829 ns | 0.1239 ns | 0.1159 ns |      ? |        ? |
 |         SetViaFlashReflection |     6.7196 ns | 0.0153 ns | 0.0136 ns |      ? |        ? |
 |   SetViaReflectionWithCaching |   205.2057 ns | 0.9305 ns | 0.8704 ns |      ? |        ? |
 |              SetViaReflection |   277.5734 ns | 1.9667 ns | 1.8396 ns |      ? |        ? |
 |   SetViaDelegateDynamicInvoke | 1,010.4607 ns | 8.6607 ns | 8.1012 ns |      ? |        ? |
