# Advent of Code - C# .NET 10.0 🎄

O [Advent of Code](https://adventofcode.com/) é um calendário do advento de pequenos desafios de programação criado por [Eric Wastl](http://was.tl/). A cada dia do início de dezembro, um novo desafio é liberado. Cada dia contém duas partes (Parte 1 e Parte 2), sendo que a segunda parte geralmente é uma extensão ou complicação da primeira.

Os desafios abrangem diversos conceitos de programação, incluindo:

- Estruturas de dados e algoritmos
- Manipulação de strings e parsing
- Problemas de otimização
- Busca e grafos
- Matemática computacional

É uma excelente oportunidade para:

- Melhorar habilidades de resolução de problemas
- Praticar diferentes linguagens de programação
- Competir com outros desenvolvedores

## Propósito deste Repositório

Oferecer uma **estrutura pronta** para você focar em otimizar e testar a performance dos códigos do desafio, sem perder tempo configurando o ambiente e a plataforma de benchmark.

Recursos incluídos:

- **Execução interativa via CLI** para testar soluções rapidamente
- **Sistema de benchmarking integrado** para análise de performance
- **Medição precisa de tempo de execução**

## Estrutura do Projeto

```
AdventOfCode/
├── EntryPoint/              # CLI interativo para executar soluções
├── PerformanceBenchmark/    # Testa a performance com o BenchmarkDotNet
├── Solutions{Year}/         # Soluções organizadas por ano (ex: Solutions2025)
│   └── Day{Day}/            # Pasta para cada dia
│       ├── Part{Part}.cs    # ⚠️ O código da sua solução do puzzle vai aqui
│       └── input{Part}.txt  # Arquivo com o input fornecido pelo Advent of Code
└── Utilities/               # Utilitários compartilhados
    ├── Files.cs             # I/O de arquivos
    ├── Reflect.cs           # Descoberta de soluções via reflexão
    └── Terminal.cs          # Formatação de terminal (cores ANSI)
```

# Primeiros Passos

## 1. Clone o repositório

```bash
git clone https://github.com/renan-lopes-eso/AdventOfCode
```

## 2. Execute a Aplicação

#### Opção A - Visual Studio Community

- Clique com o botão direito no projeto `EntryPoint` no Solution Explorer
- Selecione "Set as Startup Project"
- Pressione **F5** para rodar em debug ou **Ctrl + F5** para rodar em release

#### Opção B - VS Code ou Rider

> **Importante:** Este projeto requer o .NET 10 SDK. Se você não tiver instalado, consulte a seção `Instalando .NET 10 para o VS Code` ao final deste documento.

- Certifique-se que o `EntryPoint` esteja como projeto de inicialização

```bash
dotnet run --project EntryPoint
```

## 3. Confira seu Puzzle

Cadastre-se em [Advent of Code](https://adventofcode.com/) e clique na interface do site para acessar e ler as instruções do desafio do dia 1.

## 4. Copie o Input do Desafio

Ainda no site Advent of Code, copie seu input personalizado:

```
Estará no final da página, link verde escrito: `get your puzzle input`
```

## 5. Navegue no Projeto e Cole seu Input

Repare na seguinte organização:

```
AdventOfCode/
├── Solutions2025/
│   └── Day01/
│       ├── Part1.cs
│       └── input1.txt
```

- Abra o arquivo `Solutions2025/Day01/input1.txt` e cole o input personalizado obtido no passo anterior

## 6. Implemente a Solução para o Desafio

Abra o arquivo `Solutions2025/Day01/Part1.cs` e, no método `Run`, implemente o código que resolve o puzzle:

```csharp
namespace Solutions2025.Day01;

public static class Part1
{
    public static string Run(string[] inputs)
    {
        // Sua lógica de solução aqui...
        // 'inputs' é um array onde cada elemento é uma linha do arquivo de entrada
        int result = 0;
        foreach (var line in inputs)
        {
            // Processe cada linha conforme o desafio
        }
        return result.ToString();
    }
}
```

## 7. Confira o Resultado de sua Solução

Execute a aplicação. No console, digite `11` para executar o dia 1, parte 1. Você verá algo como:

```bash
╔═══════════════════════════════════════════════════════╗
║  Advent of Code 2025 - Dia 1, Parte 1                 ║
╚═══════════════════════════════════════════════════════╝
> Aguarde, execução em andamento...
✓ Código executado com sucesso
┌─────────────────────────────────────────────────────┐
│  Solução: 👉 3                                     │
│  Tempo de execução: 12.45 ms                        │
└─────────────────────────────────────────────────────┘
```

## 8. Verificar Resposta no Advent of Code

Tendo em mãos sua solução (neste exemplo é o número `3`):

- Vá até o site do Advent of Code e cole no campo `Answer`
- Clique no botão verde `submit`
- Caso a resposta seja errada, ajuste seu código (passos 6 e 7) até encontrar a solução correta

## 9. Múltiplas Implementações

Na tentativa de melhorar a performance do seu código, crie múltiplas abordagens de otimização:

```csharp
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
```

Para conferir as múltiplas versões:

- `> 11` roda a versão baseline `Run` do dia 1, parte 1
- `> 11 2` roda `RunV2` do dia 1, parte 1
- `> 11 3` roda `RunV3` do dia 1, parte 1

## 10. Comparar Versões

É importante garantir que as múltiplas versões implementadas retornam o mesmo resultado. Use o comando de comparação para validar:

- `> c11` compara todas as versões (Run, RunV2, RunV3, etc) do dia 1, parte 1
- `> c12` compara todas as versões do dia 1, parte 2
- `> c122` compara todas as versões do dia 12, parte 2

Você verá uma tabela comparativa e um alerta se houver divergências.

## 11. Benchmarks comparativos

Após confirmar que todas as versões estão produzindo respostas corretas, execute um benchmark para comparar a performance:

- `> b11` executa o benchmark comparativo em todas as versões (Run, RunV2, RunV3, etc) do dia 1, parte 1
- `> b12` executa o benchmark comparativo em todas as versões (Run, RunV2, RunV3, etc) do dia 1, parte 2
- `> b21` executa o benchmark comparativo em todas as versões (Run, RunV2, RunV3, etc) do dia 2, parte 1
- `> b121` executa o benchmark comparativo em todas as versões (Run, RunV2, RunV3, etc) do dia 12, parte 1

## 12. Interpretando os Resultados do Benchmark

Após executar um benchmark, você verá uma tabela com métricas de performance. Aqui está um exemplo e como interpretar cada coluna:

```
| RunVersion | Mean     | Error    | StdDev   | Rank | Gen0   | Allocated |
|----------- |---------:|---------:|---------:|-----:|-------:|----------:|
| Run        | 45.77 ns | 0.930 ns | 0.870 ns |    3 | 0.0315 |     528 B |
| RunV2      | 32.90 ns | 0.599 ns | 0.500 ns |    2 | 0.0172 |     288 B |
| RunV3      | 22.22 ns | 0.420 ns | 0.678 ns |    1 | 0.0048 |      80 B |
```

**Coluna por coluna:**

- **RunVersion** - Nome do método executado (Run, RunV2, RunV3, etc)

- **Mean** - Tempo médio de execução
  
  - Métrica principal para comparar performance
  - Unidades: ns (nanossegundos), μs (microssegundos), ms (milissegundos), s (segundos)
  - **Quanto menor, melhor**

- **Error** - Margem de erro da medição
  
  - Indica a precisão das medições
  - Erro menor = medições mais confiáveis
  - Se Error for alto comparado ao Mean, execute o benchmark novamente

- **StdDev** - Desvio padrão das medições
  
  - Mede a consistência da performance
  - StdDev baixo = performance estável e previsível
  - StdDev alto = performance instável (pode indicar problemas)

- **Rank** - Ranking da performance
  
  - 1 = implementação mais rápida
  - 2, 3, 4... = classificação das demais versões
  - **Use para identificar rapidamente a melhor versão**

- **Gen0** - Coletas de lixo de Geração 0 por operação
  
  - Indica quantas vezes o Garbage Collector foi acionado
  - `-` significa que nenhuma coleta foi necessária
  - **Quanto menor (ou nenhuma), melhor**
  - Gen0 > 0 indica alocações temporárias na heap

- **Allocated** - Total de memória alocada na heap
  
  - Quantidade de memória que será coletada pelo GC
  - `-` ou `0 B` significa zero alocações (ideal!)
  - **Quanto menor, melhor**
  - Alocações impactam performance devido ao GC

**Exemplo de análise:**

No exemplo acima, temos três implementações com diferentes níveis de otimização:

- `RunV3` (Rank 1) é a **mais rápida**: 22.22 ns - representa a solução mais otimizada
- `RunV2` (Rank 2) é **1.5x mais lenta** que RunV3: 32.90 ns - solução intermediária
- `Run` (Rank 3) é a **mais lenta**: 45.77 ns - **2.1x mais lenta** que RunV3

Em termos de memória, `RunV3` também é a campeã:

- `Run` aloca **528 bytes** com Gen0 = 0.0315 (alta pressão no GC)
- `RunV2` aloca **288 bytes** (45% de redução) com Gen0 = 0.0172
- `RunV3` aloca apenas **80 bytes** (85% de redução) com Gen0 = 0.0048 (mínima pressão no GC)

**Conclusão:** `RunV3` é significativamente mais eficiente tanto em velocidade quanto em uso de memória, sendo a melhor escolha para produção.

**Dicas de otimização baseadas nos resultados:**

- Se **Allocated** for alto: considere usar `Span<T>`, `stackalloc`, ou reutilizar buffers
- Se **Gen0** for alto: reduza alocações temporárias de objetos
- Se **StdDev** for alto: verifique se há operações não-determinísticas (I/O, random, etc)
- Se **Mean** for alto: profile o código para identificar gargalos (loops, LINQ, alocações)

----

# Seção Avançada (Leitura Opcional)

## Outros Comandos

Após iniciar o EntryPoint, na interface interativa você poderá utilizar os seguintes comandos:

- **`help`** ou **`?`** - Mostra ajuda e lista soluções implementadas
- **`clear`** ou **`cls`** - Limpa a tela
- **`sair`**, **`exit`** ou **`quit`** - Sai do programa

----

## Sobre o Benchmarking de Performance

O framework inclui integração com [BenchmarkDotNet](https://benchmarkdotnet.org/) para você analisar e otimizar suas soluções.

**Recursos de Benchmark:**

- Medição precisa de tempo de execução (Mean, Error, StdDev)
- Diagnóstico de memória (alocações, GC)
- **Comparação automática** entre todas as versões (Run, RunV2, RunV3...)
- Rankings automáticos mostrando a implementação mais rápida

**Para executar um benchmark da sua solução:**

```bash
> b11       # Compara automaticamente todas as versões (Run, RunV2, RunV3...)
```

Ou diretamente via linha de comando:

```bash
dotnet run -c Release --project PerformanceBenchmark -- --year 2025 --day 1 --part 1
```

**Como funciona:**

- O benchmark **detecta automaticamente** todos os métodos `Run*` na sua classe
- Compara todas as versões lado a lado (Run, RunV2, RunV3, etc)
- Exibe uma tabela com tempo de execução, uso de memória e ranking
- Ideal para testar otimizações e escolher a melhor implementação

----

## Regras Importantes

✅ **OBRIGATÓRIO (o framework usa reflexão e depende disso):**

- Namespace deve ser `Solutions{Ano}.Day{Dia:D2}` (ex: `Solutions2025.Day01`)
- Classe deve ser `public static class Part{Parte}`
- Método principal deve ter a assinatura: `public static string Run(string[] inputs)`
- Métodos alternativos devem seguir: `public static string RunV{N}(string[] inputs)` onde N = 2, 3, 4...
- Nome do arquivo deve corresponder ao nome da classe
- Diretórios devem usar padding de zeros: `Day01`, `Day02`, ..., `Day09`, `Day10`, etc

❌ **NÃO:**

- Mudar a assinatura dos métodos `Run*`
- Usar namespaces diferentes do padrão
- Esquecer de tornar a classe `static`
- Alterar a estrutura de pastas
- Usar `Day1` ao invés de `Day01` (quebra a ordenação)

## Tecnologias Utilizadas

- **.NET 10.0** - Framework principal
- **C# 14** - Linguagem de programação
- **Reflection** - Descoberta dinâmica de soluções
- **BenchmarkDotNet** - Performance testing
- **ANSI Escape Codes** - Formatação colorida do terminal

## Configurações do Projeto

- **TreatWarningsAsErrors:** Habilitado (Debug e Release)
- **Nullable Reference Types:** Habilitado
- **Implicit Usings:** Habilitado

----

## Instalando .NET 10 para o VS Code

Primeiro, verifique se você já possui a versão 10 instalada pelo terminal:

```bash
dotnet --list-sdks
```

### Instalar o .NET 10

Se você não tiver o .NET 10 instalado:

1. Acesse: https://dotnet.microsoft.com/download/dotnet/10.0
2. Baixe o **SDK** (não apenas o Runtime)
3. Escolha a versão correta para seu sistema operacional:
   - **Windows**: x64 ou ARM64
   - **macOS**: x64 ou ARM64 (Apple Silicon)
   - **Linux**: Escolha sua distribuição
4. Instale seguindo as instruções do instalador
5. Após a instalação, **reinicie o VS Code e o terminal**
6. Execute `dotnet --version` para confirmar a instalação

**Exemplo de saída esperada:**

```bash
$ dotnet --version
10.0.100
```

> **Nota Importante:** É necessário o **SDK**, não apenas o Runtime, pois você vai compilar e executar o código.

### Configurar o VS Code

Após instalar o .NET 10 SDK, instale a extensão oficial:

1. Abra o VS Code
2. Vá em Extensions (Ctrl+Shift+X)
3. Procure por "C# Dev Kit" ou "C#"
4. Instale a extensão oficial da Microsoft
5. Reinicie o VS Code

Agora você está pronto para usar o projeto!

----

## Dicas para Resolver os Desafios

- **Leia atentamente:** Os enunciados podem ter detalhes importantes
- **Teste com exemplos:** Use os exemplos fornecidos pelo Advent of Code antes de testar com o input completo
- **Pense em edge cases:** Considere casos extremos e validações
- **Otimize depois:** Faça funcionar primeiro, otimize depois (use o benchmark!)
- **Não desista:** Alguns desafios são desafiadores, especialmente os últimos

----

## Recursos Adicionais

- [Advent of Code](https://adventofcode.com/) - Site oficial
- [r/adventofcode](https://www.reddit.com/r/adventofcode/) - Subreddit com discussões e dicas
- [Advent of Code Wiki](https://adventofcode.fandom.com/) - Wiki com explicações
- [.NET Documentation](https://docs.microsoft.com/dotnet/) - Documentação oficial do .NET

----

## Licença

Este projeto é de código aberto e está disponível para uso pessoal e educacional.

----

## Importante

**Evite compartilhar suas soluções publicamente enquanto o evento estiver acontecendo.** Isso prejudica a experiência de outros participantes. Depois que o evento terminar, fique à vontade para compartilhar!
