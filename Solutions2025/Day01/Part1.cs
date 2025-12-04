namespace Solutions2025.Day01;

public static class Part1
{
    // Versão original (baseline), cada concatenação cria nova string
    public static string Run(string[] inputs)
    {
        var result = string.Empty;
        foreach (var input in inputs)
        {
            result += input;
        }
        return result;
    }

    // Versão 2 - Implementação otimizada, buffer interno cresce dinamicamente
    public static string RunV2(string[] inputs)
    {
        var sb = new System.Text.StringBuilder();
        foreach (var input in inputs)
        {
            sb.Append(input);
        }
        return sb.ToString();
    }

    // Versão 3 - Otimizações adicionais, calcula tamanho e aloca apenas uma vez
    public static string RunV3(string[] inputs)
    {
        return string.Concat(inputs);
    }
}