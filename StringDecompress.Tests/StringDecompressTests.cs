using FluentAssertions;
using Xunit;

namespace StringDecompress.Tests
{
    public class StringDecompressTests
    {
        [Theory]
        [InlineData("3[abc]4[ab]c", "abcabcabcababababc")]
        [InlineData("2[3[a]b]", "aaabaaab")]
        [InlineData("10[a]", "aaaaaaaaaa")]
        [InlineData("10[a2[b5[c]]]", "abcccccbcccccabcccccbcccccabcccccbcccccabcccccbcccccabcccccbcccccabcccccbcccccabcccccbcccccabcccccbcccccabcccccbcccccabcccccbccccc")] // ComplexScenario
        public void TestProvidedSample(string input, string expectedResult)
        {
            var result = new StringDecompresser(input).Decompress();
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}