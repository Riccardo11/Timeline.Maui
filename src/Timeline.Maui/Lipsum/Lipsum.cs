using System.Text;

namespace Timeline.Maui.Lipsum;

public class Lipsum
{
    public static string Generate(int minWords = 1, int maxWords = 1,
        int minSentences = 1, int maxSentences = 1,
        int numParagraphs = 1) {

        var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
            "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
            "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

        var rand = new Random();
        int numSentences = rand.Next(maxSentences - minSentences)
                           + minSentences + 1;
        int numWords = rand.Next(maxWords - minWords) + minWords + 1;

        var result = new StringBuilder();

        for(int p = 0; p < numParagraphs; p++) {
            for(int s = 0; s < numSentences; s++) {
                for(int w = 0; w < numWords; w++) {
                    if (w > 0) { result.Append(" "); }
                    result.Append(words[rand.Next(words.Length)]);
                }
                result.Append(". ");
            }
        }

        return result.ToString();
    }
}