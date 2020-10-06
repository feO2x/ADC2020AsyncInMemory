``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]     : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  Job-TRQNIW : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT

Runtime=.NET Core 3.1  

```
|         Method |          Mean |      Error |     StdDev |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |--------------:|-----------:|-----------:|---------:|--------:|-------:|------:|------:|----------:|
|      Increment |     0.2666 ns |  0.0204 ns |  0.0190 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| IncrementAsync | 1,181.3835 ns | 18.4261 ns | 20.4805 ns | 4,465.72 |  356.49 | 0.0534 |     - |     - |     256 B |
