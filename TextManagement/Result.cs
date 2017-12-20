using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;


namespace Resolvit.TextManagement
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public List<ResultItem> results
        { get; set; }

        private string JSONBody
        {
            get
            {
                string body = @"{ ""results"": [ " + Environment.NewLine + "<<ITEMS>>" + Environment.NewLine + "]}";
                return body;
            }
        }

        private string JSONItem
        {
            get
            {
                string body = @"{" + Environment.NewLine
                + @"""word"": ""<<WORD>>""," + Environment.NewLine
                + @"""total-occurrences"": <<OCRURR>>," + Environment.NewLine
                + @"""sentence-indexes"": <<INDEXES>>" + Environment.NewLine
                + @"}";
                return body;
            }
        }

        public Result()
        {
            this.results = new List<ResultItem>();
        }

        public string ToJSONString()
        {
            string json = "";
            json = JsonConvert.SerializeObject(this);

            return json;
        }

        public string ToJSONResolvit()
        {
            string json = string.Empty;
            string items = string.Empty;

            foreach (ResultItem item in this.results)
            {
                string itemString = this.JSONItem;
                itemString = itemString.Replace("<<WORD>>", item.word);
                itemString = itemString.Replace("<<OCRURR>>", item.total_occurrences.ToString());
                string indexes = string.Empty;
                foreach (int i in item.sentence_indexes)
                {
                    if (indexes == string.Empty)
                    {
                        indexes += "[" + i.ToString() + "]";
                    }
                    else
                    {
                        indexes += ", [" + i.ToString() + "]";

                    }
                }
                itemString = itemString.Replace("<<INDEXES>>", indexes);
                if (items == string.Empty)
                {
                    items = itemString;
                }
                else
                {
                    items += "," + Environment.NewLine + itemString; 
                }
            }

            json = this.JSONBody.Replace("<<ITEMS>>", items);

            return json;

        }
    }
}
