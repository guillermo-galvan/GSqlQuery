using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using GSqlQuery.SqliteTest.Data;
using System;

namespace GSqlQuery.Sqlite.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var _connectionOptions = CreateTable.GetConnectionOptions();

            //var entities = Test2.Select(_connectionOptions).Build().Execute();

            //entities = Test2.Select(_connectionOptions).Build().Execute();


            //CreateTable.Create();

            //int count = Test2.Select(_connectionOptions, x => new { x.Id }).Count().Build().Execute();

            //if (count < 100000)
            //{
            //    Test2.Delete(_connectionOptions).Build().Execute();
            //    for (int i = 0; i < 100; i++)
            //    {
            //        CreateTable.CreateData();
            //    }
            //}

            //count = Test2.Select(_connectionOptions, x => new { x.Id }).Count().Build().Execute();
            //Console.WriteLine("Init Initialize test 2 {0}", count);

            IConfig config = DefaultConfig.Instance;

            config = config
                .AddDiagnoser(MemoryDiagnoser.Default)
                .AddColumn(StatisticColumn.OperationsPerSecond);

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
        }
    }
}