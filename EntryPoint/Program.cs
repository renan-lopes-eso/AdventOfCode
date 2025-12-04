using System.Diagnostics;
using Utilities;

while (ProcessUserInputs()) ;

// Processa comandos do usuário para executar soluções do Advent of Code.
// Formato aceito para dia e parte:
//   • Agrupado: 11 (dia 1, parte 1) ou 252 (dia 25, parte 2)
// Adicionar 'b' no início executa benchmark: b11
// Adicionar 'c' no início executa comparação entre versões: c11
// Adicionar versão no final executa implementação específica: 11 v2 ou 11 2
// Retorna true para continuar o loop, false para encerrar o programa
static bool ProcessUserInputs()
{
    Console.WriteLine("Advent of Code");
    var year = GetYear();
    while (year == 0)
    {
        year = GetYear();
    }

    Console.WriteLine("Entre com o Dia e a Parte do exercício (ex: 11, 11 v2, b11, c11) ou digite 'help' para mais detalhes:");
    Console.Write("> ");

    var input = Console.ReadLine()?.Trim().ToLower();
    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("Entrada inválida!");
        return true;
    }

    // Comandos especiais
    if (input == "sair" || input == "exit" || input == "quit")
    {
        return false;
    }

    if (input == "clear" || input == "cls")
    {
        Console.Clear();
        return true;
    }

    if (input == "help" || input == "?")
    {
        ShowHelp(year);
        return true;
    }

    var isBench = input.StartsWith("b");
    var isCompare = input.StartsWith("c");

    if (isBench || isCompare)
    {
        input = input.Substring(1).Trim(); // Remove o 'b'/'c' e possíveis espaços após a letra
    }

    // Se disponível, extrai versão se especificada (ex: "11 v2", "11 2")
    var version = 1;
    if (input.Contains(" "))
    {
        var splitted = input.Split(' ');
        if (splitted.Length > 2)
        {
            Console.WriteLine("Formato inválido! Use: diaparte (ex: 11 ou 252)");
            return true;
        }

        var versionStr = splitted[1];
        if (versionStr.StartsWith("v"))
            versionStr = versionStr.Substring(1);

        int.TryParse(versionStr, out version);
        input = splitted[0];
    }

    int day, part;

    // Validações para o formato agrupado (ex: 11, 252)
    if (!int.TryParse(input, out int combined) || combined < 11)
    {
        Console.WriteLine("Formato inválido! Use: diaparte (ex: 11 ou 252)");
        return true;
    }

    part = combined % 10; // O último dígito é a 'Parte'
    day = combined / 10;  // O 'resto' é o 'Dia'

    if (part < 1 || part > 2)
    {
        Console.WriteLine("Parte inválida! A parte deve ser 1 ou 2");
        return true;
    }

    if (isBench)
    {
        RunBenchmark(year, day, part);
        return true;
    }

    if (isCompare)
    {
        RunComparison(year, day, part);
        return true;
    }

    try
    {
        RunExerciseSolution(year, day, part, version);
        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro executando: {ex.Message}");
        return true;
    }
}

// Este projeto prevê a possibilidade de solucionar os puzzles do advent of code de múltiplos anos, como os de 2024, 2025, 2026, etc
// Portanto, solicitamos um input do usuário determinando o ano a ser executado
// No caso em que nossa solution tenha apenas a implementação de um ano (ex Solutions2025),
// skipamos a pergunta e retornamos o único ano disponível
static int GetYear()
{
    // Para definir os anos disponíveis é necessário utilizar reflexão para listar as dlls contidas
    // nessa solution que sigam o padrão Solutions{ano}
    var years = Helper.Reflect.GetSolutionDlls()
        .Select(a => Path.GetFileNameWithoutExtension(a))
        .Where(a => a.Contains("Solutions"))
        .Select(a => int.Parse(a.Replace("Solutions", "")))
        .ToList();

    if (years == null || years.Count == 0)
    {
        Console.WriteLine("Nenhuma implementação de solução do Advent of Code foi encontrada.");
        return 0;
    }

    if (years.Count == 1)
    {
        return years[0];
    }
    else
    {
        var availableYears = string.Join(", ", years);
        Console.WriteLine($"Informe um ano dentre as opções disponíveis: {availableYears}");
        Console.Write("> ");
        var inputYear = Console.ReadLine()?.Trim().ToLower();

        if (!int.TryParse(inputYear, out int selectedYear))
        {
            Console.WriteLine("Ano inválido!");
            return 0;
        }

        if (years.Contains(selectedYear) == false)
        {
            Console.WriteLine("O ano informado não possui solução implementada");
            return 0;
        }

        return selectedYear;
    }
}

static void ShowHelp(int year)
{
    Console.WriteLine();
    Console.WriteLine("=== Advent of Code - Help ===");
    Console.WriteLine();
    Console.WriteLine("Comandos:");
    Console.WriteLine("  <diaparte>            Rodar uma solução (ex: 11, 12, 252)");
    Console.WriteLine("  <diaparte> <ver>      Rodar versão específica (ex: 11 v2, 11 2)");
    Console.WriteLine("  b<diaparte>           Rodar benchmark (ex: b11, b12, b252)");
    Console.WriteLine("  c<diaparte>           Comparar versões (ex: c11, c12, c252)");
    Console.WriteLine("  help ou ?             Mostrar esta mensagem de ajuda");
    Console.WriteLine("  clear ou cls          Limpar a tela");
    Console.WriteLine("  sair ou exit          Sair do programa");
    Console.WriteLine();
    Console.WriteLine("Exemplos:");
    Console.WriteLine("  > 11                  Executar Dia 1, Parte 1 (versão Run)");
    Console.WriteLine("  > 11 v2               Executar Dia 1, Parte 1 (versão RunV2)");
    Console.WriteLine("  > 11 2                Executar Dia 1, Parte 1, versão 2");
    Console.WriteLine("  > 252                 Executar Dia 25, Parte 2");
    Console.WriteLine("  > b11                 Benchmark do Dia 1, Parte 1 (todas versões)");
    Console.WriteLine("  > c11                 Comparar todas versões do Dia 1, Parte 1");
    Console.WriteLine();
}

static void RunExerciseSolution(int year, int day, int part, int? version = null)
{
    if (version == 1)
        version = null;

    var versionText = version.HasValue && version.Value > 0 ? $" (V{version.Value})" : "";

    Console.WriteLine();
    Console.WriteLine(Helper.Terminal.Bold($"╔═══════════════════════════════════════════════════════╗"));
    Console.WriteLine(Helper.Terminal.Bold($"║  {Helper.Terminal.Cyan($"Advent of Code {year}")} - {Helper.Terminal.Yellow($"Dia {day}, Parte {part}{versionText}")}".PadRight(76) + "║"));
    Console.WriteLine(Helper.Terminal.Bold($"╚═══════════════════════════════════════════════════════╝"));
    Console.WriteLine();

    // A partir dos inputs fornecidos ao terminal (ano, dia, parte), acessamos o projeto da solução, ex: Solutions2025
    // Buscamos pela respectiva classe e input de dados, ex: Day01/Part1.cs e Day01/input1.txt
    // E por reflexão, rodamos o método Run() (ou RunV2, RunV3, etc) o qual deverá conter a solução implementada
    var inputs = Helper.Files.LoadInputFromFile(year, day, part);
    var method = Helper.Reflect.GetRunMethodFromSolutionsAssembly(year, day, part, version);

    Console.WriteLine($"{Helper.Terminal.Red(">")} Aguarde, execução em andamento...");

    var stopwatch = Stopwatch.StartNew();
    var solution = method.Invoke(null, [inputs]) as string;
    stopwatch.Stop();

    Console.WriteLine($"{Helper.Terminal.Green("✓")} Código executado com sucesso");
    Console.WriteLine();

    // Mostramos o output retornado pela função Run (que contém a solução do puzzle)
    // Também mostramos o tempo de execução, que apesar de não ser tão preciso como o benchmark, serve como uma base rápida de referência
    Console.WriteLine(Helper.Terminal.Bold($"┌─────────────────────────────────────────────────────┐"));
    Console.WriteLine(Helper.Terminal.Bold($"│  Output: {Helper.Terminal.Green(solution)}".PadRight(64) + "│"));
    Console.WriteLine(Helper.Terminal.Bold($"│  Tempo de execução: {Helper.Terminal.Cyan(FormatExecutionTime(stopwatch.Elapsed))}".PadRight(64) + "│"));
    Console.WriteLine(Helper.Terminal.Bold($"└─────────────────────────────────────────────────────┘"));
    Console.WriteLine();
}

static string FormatExecutionTime(TimeSpan elapsed)
{
    if (elapsed.TotalMilliseconds < 1)
        return $"{elapsed.TotalMicroseconds:F2} μs";
    else if (elapsed.TotalMilliseconds < 1000)
        return $"{elapsed.TotalMilliseconds:F2} ms";
    else if (elapsed.TotalSeconds < 60)
        return $"{elapsed.TotalSeconds:F2} s";
    else
        return $"{elapsed.TotalMinutes:F2} min";
}

// Rodamos por debaixo dos panos o projeto do benchmark dot net passando como 
// parâmetro as informações para que ele possa encontrar a solução necessária
// e executar as aferições de tempo em cima do código
static bool RunBenchmark(int year, int day, int part)
{
    try
    {
        var benchmarkProject = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "PerformanceBenchmark", "PerformanceBenchmark.csproj"));
        if (!File.Exists(benchmarkProject))
        {
            Console.WriteLine($"Não foi possível achar o projeto do Benchmark em: {benchmarkProject}");
            return false;
        }

        var psi = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"run -c Release --project \"{benchmarkProject}\" -- --year {year} --day {day} --part {part}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };

        using var startedProcess = Process.Start(psi);
        if (startedProcess == null)
        {
            Console.WriteLine("Erro: não foi possível iniciar o processo de benchmark");
            return false;
        }
        
        // Repassamos adiante os logs fornecidos pelo projeto do benchmark
        Console.WriteLine("Executando benchmark...");
        while (!startedProcess.StandardOutput.EndOfStream)
            Console.WriteLine(startedProcess.StandardOutput.ReadLine());       

        var error = startedProcess.StandardError.ReadToEnd();
        startedProcess.WaitForExit();

        if (!string.IsNullOrWhiteSpace(error))
            Console.Error.WriteLine(error);

        return startedProcess.ExitCode == 0;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro rodando o benchmark: {ex.Message}");
        return false;
    }
}

static void RunComparison(int year, int day, int part)
{
    try
    {
        Console.WriteLine();
        Console.WriteLine(Helper.Terminal.Bold($"╔═══════════════════════════════════════════════════════╗"));
        Console.WriteLine(Helper.Terminal.Bold($"║  {Helper.Terminal.Cyan($"Advent of Code {year}")} - {Helper.Terminal.Yellow($"Comparação Dia {day}, Parte {part}")}".PadRight(76) + "║"));
        Console.WriteLine(Helper.Terminal.Bold($"╚═══════════════════════════════════════════════════════╝"));
        Console.WriteLine();

        // Carrega input do arquivo
        var inputs = Helper.Files.LoadInputFromFile(year, day, part);

        // Descobre todos os métodos Run* usando reflexão
        var methods = Helper.Reflect.GetAllRunMethodsFromSolutionsAssembly(year, day, part);

        if (methods == null || methods.Count == 0)
        {
            Console.WriteLine(Helper.Terminal.Red("Nenhuma implementação encontrada!"));
            return;
        }

        Console.WriteLine($"{Helper.Terminal.Cyan("ℹ")} Executando {methods.Count} versão(ões)...");
        Console.WriteLine();

        var results = new List<(string MethodName, string Result)>();

        // Executa cada versão e coleta resultados
        foreach (var method in methods)
        {
            var result = method.Invoke(null, [inputs]) as string ?? "";
            var methodName = method.Name == "Run" ? "Run" : method.Name;
            results.Add((methodName, result));
        }

        // Verifica se todos os resultados são iguais
        var distinctResults = results.Select(r => r.Result).Distinct().ToList();
        var allEqual = distinctResults.Count == 1;

        // Exibe tabela de resultados
        Console.WriteLine(Helper.Terminal.Bold("┌────────────┬──────────────────────────────────────────────────┐"));
        Console.WriteLine(Helper.Terminal.Bold("│ Versão     │ Resultado                                        │"));
        Console.WriteLine(Helper.Terminal.Bold("├────────────┼──────────────────────────────────────────────────┤"));

        foreach (var (methodName, result) in results)
        {
            var displayResult = result.Length > 48 ? result.Substring(0, 45) + "..." : result;
            var resultColor = allEqual ? Helper.Terminal.Green(displayResult) : Helper.Terminal.Yellow(displayResult);

            Console.WriteLine($"│ {methodName,-10} │ {resultColor,-58} │");
        }

        Console.WriteLine(Helper.Terminal.Bold("└────────────┴──────────────────────────────────────────────────┘"));
        Console.WriteLine();

        // Mostra status da comparação
        if (allEqual)
        {
            Console.WriteLine($"{Helper.Terminal.Green("✓")} Todas as versões retornaram o mesmo resultado: {Helper.Terminal.Bold(distinctResults[0])}");
        }
        else
        {
            Console.WriteLine($"{Helper.Terminal.Red("✗")} ATENÇÃO: Versões retornaram resultados diferentes!");
            Console.WriteLine();
            Console.WriteLine(Helper.Terminal.Yellow("Resultados únicos encontrados:"));
            foreach (var result in distinctResults)
            {
                var count = results.Count(r => r.Result == result);
                var versions = string.Join(", ", results.Where(r => r.Result == result).Select(r => r.MethodName));
                Console.WriteLine($"  • {Helper.Terminal.Bold(result)} ({count}x) - {versions}");
            }
        }
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{Helper.Terminal.Red("✗")} Erro na comparação: {ex.Message}");
    }
}