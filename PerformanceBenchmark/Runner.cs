namespace PerformanceBenchmark;

using System;
using Utilities;

public class Runner
{
    public static string Run(int year, int day, int part)
    {
        var inputs = Helper.Files.LoadInputFromFile(year, day, part);
        var run = Helper.Reflect.GetRunMethodFromSolutionsAssembly(year, day, part);
        var del = (Func<string[], string>)Delegate.CreateDelegate(typeof(Func<string[], string>), run);
        return del(inputs);
    }
}