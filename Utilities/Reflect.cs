namespace Utilities;

using System;
using System.Reflection;

public static partial class Helper
{
    public class Reflect
    {
        public static IEnumerable<string> GetSolutionDlls()
        {
            return Directory.EnumerateFiles(AppContext.BaseDirectory, "Solutions*.dll");
        }

        public static MethodInfo GetRunMethodFromSolutionsAssembly(int year, int day, int exercise, int? version = null)
        {
            var type = GetSolutionType(year, day, exercise);
            var methodName = version.HasValue && version.Value > 0 ? $"RunV{version.Value}" : "Run";

            return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static)
                ?? throw new InvalidOperationException($"Método público estático {methodName}() não encontrado em '{type.FullName}'.");
        }

        // Busca todos os métodos que começam com "Run" (ex: Run, RunV2, RunV3) para comparação de implementações
        public static List<MethodInfo> GetAllRunMethodsFromSolutionsAssembly(int year, int day, int exercise)
        {
            var type = GetSolutionType(year, day, exercise);

            var runMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(m => m.Name.StartsWith("Run") &&
                           m.ReturnType == typeof(string) &&
                           m.GetParameters().Length == 1 &&
                           m.GetParameters()[0].ParameterType == typeof(string[]))
                .OrderBy(m => m.Name == "Run" ? 0 : int.TryParse(m.Name.Replace("Run", "").Replace("V", ""), out int v) ? v : 999)
                .ToList();

            if (runMethods.Count == 0)
                throw new InvalidOperationException($"Nenhum método público estático Run*() encontrado em '{type.FullName}'.");

            return runMethods;
        }

        // Método auxiliar privado que carrega o assembly e retorna o tipo da solução
        private static Type GetSolutionType(int year, int day, int exercise)
        {
            var assemblyName = $"Solutions{year}";
            var typeFullName = $"{assemblyName}.Day{day:D2}.Part{exercise}";

            // Tenta encontrar assembly já carregado, senão carrega do disco
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name?.Equals(assemblyName, StringComparison.OrdinalIgnoreCase) == true);

            if (assembly == null)
            {
                var assemblyPath = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.dll");
                assembly = Assembly.LoadFrom(assemblyPath); // Deixa lançar exceção se não existir
            }

            return assembly.GetType(typeFullName, throwOnError: true)!;
        }
    }
}
