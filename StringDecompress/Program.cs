using System;
using System.IO;
using System.Threading.Tasks;
using Mono.Options;

namespace StringDecompress
{
    internal class Program
    {
        private static readonly OptionSet parameterStore = new OptionSet() {
            { "i|input=", key => { if (!string.IsNullOrEmpty(key)) input = key; } },
            { "f|file=", key => { if (!string.IsNullOrEmpty(key)) file = key; } },
            { "h|?|help",  key => { showHelp = key != null; } },
        };

        private static string input = default;
        private static string file = default;
        private static bool showHelp = default;

        private static async Task Main(string[] args)
        {
            parameterStore.Parse(args);

            if (showHelp)
            {
                Console.WriteLine("-f | --file: File containing the people's data forming our circus tower");
                Console.WriteLine("-w | --weightFirst: Expects the input to be a {weight},{height}");
            }
            else if (!string.IsNullOrEmpty(input))
            {
                Process(input);
            }
            else if (!string.IsNullOrEmpty(file))
            {
                if (File.Exists(file))
                {
                    using var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                    using var sr = new StreamReader(fs);
                    Process(await sr.ReadToEndAsync());
                }
                else
                {
                    Console.WriteLine("Provided file does not exist.");
                }
            }
            else
            {
                Console.WriteLine("Try running \"StringDecompress --help\"");
            }
        }

        private static void Process(string input)
        {
            Console.WriteLine("Input String: {0}", input);
            Console.WriteLine("Result: {0}", new StringDecompresser(input));
        }
    }
}