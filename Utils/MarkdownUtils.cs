using System.Text.RegularExpressions;

namespace Vertiq.Utils;

internal static partial class MarkdownStringUtils
{
#if NET7_0_OR_GREATER
    [GeneratedRegex(@"^( *)\S")]
    private static partial Regex GetSpaceCountRegEx();

#else
    private static readonly Regex SpaceCountRegex = new Regex(@"^( *)\S", RegexOptions.Compiled);
    private static Regex GetSpaceCountRegEx() => SpaceCountRegex;
#endif

    internal static string TrimLeftSpaces(string input)
    {
        var lines = input.Split(Environment.NewLine);
        if (lines.Length > 1)
        {
            var line = lines.FirstOrDefault(l => !string.IsNullOrWhiteSpace(l)) ?? "";

            //Count the number of spaces in the first line
            var spaceCount = GetSpaceCountRegEx().Match(line).Groups[1].Length;

            var markup = string.Join(Environment.NewLine,
                lines.Select(l => TrimSpaces(l, spaceCount))
            );

            return markup;
        }

        return input;

        static string TrimSpaces(string input, int trimCount = 0)
        {
            var output = input.Length > trimCount ? input.Substring(trimCount) : input;
            return output;
        }
    }
}
