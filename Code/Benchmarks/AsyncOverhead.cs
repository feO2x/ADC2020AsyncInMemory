using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Benchmarks
{
    public class AsyncOverhead
    {
        public int Counter;

        [Benchmark(Baseline = true)]
        public int Increment() => Counter++;

        [Benchmark]
        public int IncrementAsync() => IncrementAsyncInternal().Result;

        private async Task<int> IncrementAsyncInternal()
        {
            return await Task.Run(Increment);
        }
    }
}