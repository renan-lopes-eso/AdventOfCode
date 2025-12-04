namespace Utilities;

public static partial class Helper
{
    public static class Files
    {
        public static string GetProjectRoot()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var dirInfo = new DirectoryInfo(baseDir);

            var max = 10;
            while (dirInfo != null && !dirInfo.GetFiles("*.slnx").Any() && max-- > 0)
            {
                if (dirInfo.Parent != null)
                {
                    dirInfo = dirInfo.Parent;
                }
            }

            return dirInfo?.FullName ?? throw new DirectoryNotFoundException("Não foi possível encontrar a raiz do projeto (.slnx)");
        }

        public static string[] LoadInputFromFile(int year, int day, int exercise)
        {
            var root = Helper.Files.GetProjectRoot();
            var filePath = Path.Combine($"{root}/Solutions{year}", $"Day{day:D2}", $"input{exercise}.txt");
            try
            {
                return File.ReadAllLines(filePath);
            }
            catch
            {
                System.Console.WriteLine($"Arquivo de entrada Day{day:D2}/input{exercise}.txt não encontrado: {filePath}");
                return Array.Empty<string>();
            }
        }
    }
}
