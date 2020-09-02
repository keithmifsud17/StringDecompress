using System;
using System.Text;

namespace StringDecompress
{
    public class StringDecompresser
    {
        private readonly Lazy<string> lazyDecompress;

        public StringDecompresser(string input)
        {
            lazyDecompress = new Lazy<string>(() =>
            {
                var builder = new StringBuilder();
                Decompress(input, 0, builder);
                return builder.ToString();
            });
        }

        public string Decompress() => lazyDecompress.Value;

        public override string ToString() => lazyDecompress.Value;

        private int Decompress(string input, int start, StringBuilder builder)
        {
            var occurrences = new StringBuilder();

            var index = start;
            while (index < input.Length)
            {
                switch (input[index])
                {
                    case var digit when char.IsDigit(digit):
                        occurrences.Append(digit);
                        index++;

                        break;

                    case '[':
                        // We're starting a new group and can work out the number of occurrences.
                        var numberOfOccurrences = int.Parse(occurrences.ToString()); // We know number if valid, and we also know that there is in fact a number in occurrences
                        occurrences = new StringBuilder(); // We don't need this cache anymore

                        var currentGroup = new StringBuilder();
                        index = Decompress(input, index + 1, currentGroup); // case ']' will break the recursion and gives us the group's content
                        for (int i = 0; i < numberOfOccurrences; i++)
                        {
                            builder.Append(currentGroup);
                        }

                        break;

                    case ']':
                        return index + 1;

                    default:
                        builder.Append(input[index]);
                        index++;

                        break;
                }
            }
            return index;
        }
    }
}