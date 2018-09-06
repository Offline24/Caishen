using System.Reflection;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

namespace Caishen.Benchmarks
{
    internal static class Program
    {
        private static void Main()
        {
            var config = ManualConfig.Create(DefaultConfig.Instance)
                .With(Job.Core)
                .With(TargetMethodColumn.Method, StatisticColumn.Max, RankColumn.Arabic)
                .With(CsvExporter.Default, HtmlExporter.Default)
                .With(ConsoleLogger.Default)
                .With(ExecutionValidator.FailOnError);
            
            BenchmarkRunner.Run(Assembly.GetExecutingAssembly(), config);
        }
    }
}