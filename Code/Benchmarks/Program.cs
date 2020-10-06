using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                             .Run(args, CreateDefaultConfig());
        }

        private static IConfig CreateDefaultConfig() =>
            DefaultConfig.Instance
                         .AddJob(Job.Default.WithRuntime(CoreRuntime.Core31))
                         .AddDiagnoser(MemoryDiagnoser.Default);
    }
}