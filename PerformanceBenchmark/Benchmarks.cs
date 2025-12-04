namespace PerformanceBenchmark;

using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using Utilities;

// Wrapper para cada método Run* encontrado na solução
public class RunMethodWrapper
{
    public string Name { get; }
    public Func<string[], string> Method { get; }

    public RunMethodWrapper(string name, Func<string[], string> method)
    {
        Name = name;
        Method = method;
    }

    // Override ToString para ter nomes legíveis na tabela de resultados do benchmark
    public override string ToString() => Name;
}

[MemoryDiagnoser]
[RankColumn]
[HideColumns("Method")]
public class Benchmarks
{
    public static int Year { get; set; } = 2025;
    public static int Day { get; set; } = 1;
    public static int Part { get; set; } = 1;

    private string[] inputs;

    [GlobalSetup]
    public void Setup()
    {
        // É possível que as soluções que estamos fazendo os benchmarks possuam Console.WriteLine esquecidos no código,
        // afetando severamente o resultado dos testes.
        // Para aliviar o impacto, desabilitamos a escrita no console para a execução
        Console.SetOut(TextWriter.Null);

        this.inputs = Helper.Files.LoadInputFromFile(Year, Day, Part);
    }

    // Descobre dinamicamente todos os métodos Run* (Run, RunV2, RunV3, etc) das classes de solução
    public IEnumerable<RunMethodWrapper> GetRunMethods()
    {
        var runMethods = Helper.Reflect.GetAllRunMethodsFromSolutionsAssembly(Year, Day, Part);

        foreach (var method in runMethods)
        {
            var del = (Func<string[], string>)Delegate.CreateDelegate(typeof(Func<string[], string>), method);
            yield return new RunMethodWrapper(method.Name, del);
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetRunMethods))]
    public void RunMethod(RunMethodWrapper RunVersion)
    {
        RunVersion.Method(inputs);
    }
}
