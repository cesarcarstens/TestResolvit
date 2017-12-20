using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolvit.TextManagement
{
    public class ResultAnalysis
    {
        public static void SortResults(Result values)
        {
            List<ResultItem> buffer = values.results.OrderBy(x => x.word.ToLower()).ToList();
            values.results = buffer;
        }

        public static void DeleteRepeatedResults(Result values)
        {
            List<ResultItem> buffer = values.results.GroupBy(x => x.word.ToLower()).Select(y => y.First()).ToList();
            values.results = buffer;
        }

        public static List<string> GetSameWord(string word, Result values)
        {
            List<string> result = new List<string>();
            System.Globalization.CultureInfo info = new System.Globalization.CultureInfo("en-US");
            PluralizationService ps = PluralizationService.CreateService(info);

            result.Add(word);

            foreach (ResultItem item in values.results)
            {
                if (ps.IsSingular((word.ToLower())))
                {
                    string plural = ps.Pluralize(word.ToLower());
                    if (plural == item.word.ToLower())
                    {
                        result.Add(plural);
                    }
                }
                else
                {
                    string singular = ps.Singularize(word.ToLower());
                    string plural = ps.Pluralize(word.ToLower());
                    if (singular == plural)
                    {

                    }
                    else
                    { 
                        if (singular == item.word.ToLower())
                        {
                            return null;
                        }
                    }

                }
            }

            return result;
        }

        public static int GetTotalOccurrences(List<string> words, string rawText)
        {
            int total = 0;
            foreach (string word in words)
            {
                int sub = CountStringOccurrences(rawText.ToLower(), word.ToLower());
                total = total + sub;
            }

            return total;
        }

        public static List<int> GetSentenceIndexes(List<string> words, string sentence)
        {
            List<int> indexes = new List<int>();
            foreach (string word in words)
            {
                List<int> sub = CountStringPositions(sentence.ToLower(), word.ToLower());
                indexes.AddRange(sub);
            }

            return indexes;
        }

        private static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

        private static List<int> CountStringPositions(string text, string pattern)
        {

            char delimiter = ' ';
            List<string> words = new List<string>();
            words.AddRange(text.Split(delimiter));
            List<int> indexes = new List<int>();

            for (int i = 0; i < words.Count; i++)
            {
                if (words[i].Replace(",","").Replace(".", "").Replace(";", "").Replace(":", "") == pattern)
                {
                    indexes.Add(i);
                }
            }

            return indexes;
        }

    }
}
