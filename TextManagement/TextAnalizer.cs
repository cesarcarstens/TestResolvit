using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Resolvit.TextManagement
{
    public class TextAnalizer
    {

        #region Properties 

        public string RawText
        { get; set; }

        public List<string> WordsNotAnalyzed
        { get; set; }

        public List<string> Sentences
        {
            get
            {
                List<string> result = new List<string>();   
                char[] delimiterChars = { '.', ';' };
                result.AddRange(this.RawText.Split(delimiterChars));
                for (int i = 0; i < result.Count; i++)
                {
                    result[i] = result[i].Trim();
                }
                result.Remove("");
                return result;
            }
        }

        #endregion

        #region Constructor

        public TextAnalizer()
        {
            this.WordsNotAnalyzed = new List<string>();
        }

        #endregion

        #region Methods

        public List<string> GetWordListFromSentence(string sentence)
        {

            List<string> result = new List<string>();
            char delimiter = ' ';
            result.AddRange(sentence.Split(delimiter));
            result = DeleteSymbols(result);
            DeleteWordsNotAnalyzed(result);
            return result;
        }

        private List<string> DeleteSymbols(List<string> words)
        {
            string reg = "[^a-zA-Z]";
            List<string> result = new List<string>();
            foreach (string word in words)
            {
                string newValue = string.Empty;
                newValue = Regex.Replace(word, reg, "");
                result.Add(newValue);
            }
            return result;
        }

        private void DeleteWordsNotAnalyzed(List<string> words)
        {
            int index = -1;
            List<string> toBeDeleted = new List<string>();
            foreach (string word in words)
            {
                index++;
                foreach (string notAnalysed in this.WordsNotAnalyzed)
                {
                    if (word.ToLower() == notAnalysed.ToLower())
                    {
                        toBeDeleted.Add(word);
                    }
                }
            }

            foreach (string s in toBeDeleted)
            {
                words.Remove(s);
            }
        }


        #endregion

    }
}
