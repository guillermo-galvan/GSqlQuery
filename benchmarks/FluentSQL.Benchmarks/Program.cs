using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;

namespace MyBenchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfig config = DefaultConfig.Instance;

            config = config
                .AddDiagnoser(MemoryDiagnoser.Default)
                .AddColumn(StatisticColumn.OperationsPerSecond);

            var summary = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);

            Console.WriteLine(summary);
        }
    }
}