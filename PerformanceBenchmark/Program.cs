using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using PerformanceBenchmark;
using System.Linq;
using Utilities;

var year = Helper.Terminal.GetArgs(args, "--year", 2025);
var day = Helper.Terminal.GetArgs(args, "--day", 1);
var part = Helper.Terminal.GetArgs(args, "--part", 1);

Benchmarks.Year = year;
Benchmarks.Day = day;
Benchmarks.Part = part;

var benchmarkArgs = args.Where(a =>
            !a.StartsWith("--year") &&
            !a.StartsWith("--day") &&
            !a.StartsWith("--part")).ToArray();

BenchmarkRunner.Run<Benchmarks>(DefaultConfig.Instance, benchmarkArgs);