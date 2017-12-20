using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Resolvit.TextManagement
{
    [DataContract]
    public class ResultItem
    {
        [DataMember]
        public string word
        { get; set; }

        [DataMember]
        public int total_occurrences
        {get; set;}

        [DataMember]
        public List<int> sentence_indexes
        {get; set;}

        public ResultItem()
        {
            this.word = string.Empty;
            this.total_occurrences = 0;
            this.sentence_indexes = new List<int>();
        }
    }
}
