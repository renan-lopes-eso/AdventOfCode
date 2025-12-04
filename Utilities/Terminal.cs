namespace Utilities;

public static partial class Helper
{
    public static class Terminal
    {
        private static readonly string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
        private static readonly string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
        private static readonly string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
        private static readonly string YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";
        private static readonly string BLUE = Console.IsOutputRedirected ? "" : "\x1b[94m";
        private static readonly string MAGENTA = Console.IsOutputRedirected ? "" : "\x1b[95m";
        private static readonly string CYAN = Console.IsOutputRedirected ? "" : "\x1b[96m";
        private static readonly string GREY = Console.IsOutputRedirected ? "" : "\x1b[97m";
        private static readonly string BOLD = Console.IsOutputRedirected ? "" : "\x1b[1m";
        private static readonly string NOBOLD = Console.IsOutputRedirected ? "" : "\x1b[22m";
        private static readonly string UNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[4m";
        private static readonly string NOUNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[24m";
        private static readonly string REVERSE = Console.IsOutputRedirected ? "" : "\x1b[7m";
        private static readonly string NOREVERSE = Console.IsOutputRedirected ? "" : "\x1b[27m";

        public static string Reverse(string? input)
        {
            return $"{Helper.Terminal.REVERSE}{input}{Helper.Terminal.NOREVERSE}";
        }

        public static string Underline(string? input)
        {
            return $"{Helper.Terminal.UNDERLINE}{input}{Helper.Terminal.NOUNDERLINE}";
        }

        public static string Bold(string? input)
        {
            return $"{Helper.Terminal.BOLD}{input}{Helper.Terminal.NOBOLD}";
        }

        public static string Green(string? input)
        {
            return $"{Helper.Terminal.GREEN}{input}{Helper.Terminal.NORMAL}";
        }

        public static string Red(string? input)
        {
            return $"{Helper.Terminal.RED}{input}{Helper.Terminal.NORMAL}";
        }

        public static string Yellow(string? input)
        {
            return $"{Helper.Terminal.YELLOW}{input}{Helper.Terminal.NORMAL}";
        }

        public static string Blue(string? input)
        {
            return $"{Helper.Terminal.BLUE}{input}{Helper.Terminal.NORMAL}";
        }

        public static string Cyan(string? input)
        {
            return $"{Helper.Terminal.CYAN}{input}{Helper.Terminal.NORMAL}";
        }

        public static string Magenta(string? input)
        {
            return $"{Helper.Terminal.MAGENTA}{input}{Helper.Terminal.NORMAL}";
        }

        public static string Grey(string? input)
        {
            return $"{Helper.Terminal.GREY}{input}{Helper.Terminal.NORMAL}";
        }

        public static int GetArgs(string[] args, string key, int defaultValue)
        {
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i] == key && int.TryParse(args[i + 1], out int value))
                {
                    return value;
                }
            }
            return defaultValue;
        }
    }
}
