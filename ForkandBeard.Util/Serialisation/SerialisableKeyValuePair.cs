using System;
using System.Collections.Generic;
using System.Text;

namespace ForkandBeard.Util.Serialisation
{
    public class SerialisableKeyValuePair
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public SerialisableKeyValuePair()
        {
            this.Key = String.Empty;
            this.Value = String.Empty;
        }

        public SerialisableKeyValuePair(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public static Dictionary<string, string> ListToDictionary(List<SerialisableKeyValuePair> data)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (SerialisableKeyValuePair keyValuePair in data)
            {
                dictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return dictionary;
        }

        public static string ListToString(List<SerialisableKeyValuePair> data)
        {
            string text = String.Empty;

            foreach (SerialisableKeyValuePair keyValue in data)
            {
                text += String.Format("[{0}: {1}],", keyValue.Key, keyValue.Value);
            }

            if (!String.IsNullOrEmpty(text))
            {
                text = text.Substring(0, text.Length - 1);
            }

            return text;
        }
    }
}
