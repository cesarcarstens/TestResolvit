using Resolvit.TextManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolvit.ParagraphAnalyser
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] notAllowed = { "a", "the", "and", "of", "in", "be", "also", "as" };
            string text = System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "TextToAnalyse.txt"));
            Result result = new Result();

            TextAnalizer analizer = new TextAnalizer();
            analizer.WordsNotAnalyzed.AddRange(notAllowed);
            analizer.RawText = text;

            Console.WriteLine("Text to Analize:");
            Console.WriteLine(text);
            Console.WriteLine("");
            Console.WriteLine("Sentences:");
            foreach (string sentence in analizer.Sentences)
            {
                //Write the sentence to analize
                Console.WriteLine(sentence);
                List<string> words = analizer.GetWordListFromSentence(sentence);
                foreach (string word in words)
                {
                    ResultItem item = new ResultItem();
                    item.word = word;
                    result.results.Add(item);
                }
            }
            ResultAnalysis.SortResults(result);
            ResultAnalysis.DeleteRepeatedResults(result);

            Result cleanList = new Result();

            foreach (ResultItem item in result.results)
            {
                Console.WriteLine(@"Finding words of the same meaning... """ + item.word + @"""");
                List<string> buffer = ResultAnalysis.GetSameWord(item.word, result);
                if (buffer != null)
                {
                    ResultItem newItem = new ResultItem();
                    newItem.word = item.word;
                    newItem.total_occurrences = ResultAnalysis.GetTotalOccurrences(buffer, analizer.RawText);
                    foreach (string sentence in analizer.Sentences)
                    {
                        newItem.sentence_indexes.AddRange(ResultAnalysis.GetSentenceIndexes(buffer, sentence));                            
                    }

                    cleanList.results.Add(newItem);
                }

            }

            Console.WriteLine("");
            Console.WriteLine("Result JSON:");
            Console.WriteLine("");

            Console.WriteLine(cleanList.ToJSONResolvit());

            Console.WriteLine("");
            Console.WriteLine("Press Any Key to Contnue...");

            Console.ReadKey();


        }
    }
}
