using System;
using System.Collections.Generic;
using System.Text;

namespace ForkandBeard.Util.Collections
{
    public class Parser
    {
        public static List<T> ArrayStringToEnumList<T>(string arrayString) where T : struct, IConvertible
        {
            string[] split;
            List<T> list = new List<T>();

            split = arrayString.Split(',');
            foreach (string item in split)
            {
                list.Add(Enums.EnumHelper.Parse<T>(item));
            }

            return list;
        }

        public static List<int> ArrayStringToIntList(string arrayString)
        {
            string[] split;
            List<int> list = new List<int>();

            split = arrayString.Split(',');
            foreach (string item in split)
            {
                list.Add(Int32.Parse(item));
            }

            return list;
        }

        public static List<double> ArrayStringToDoubleList(string arrayString)
        {
            string[] split;
            List<double> list = new List<double>();

            if (!String.IsNullOrEmpty(arrayString))
            {
                split = arrayString.Split(',');
                foreach (string item in split)
                {
                    list.Add(Double.Parse(item));
                }
            }
            return list;
        }

        public static string ListToString<t>(List<t> list)
        {
            string[] listAsStringArray;

            listAsStringArray = ListToStringArray<t>(list);

            return String.Join(",", listAsStringArray);
        }

        public static string[] ListToStringArray<t>(List<t> list)
        {
            return Array.ConvertAll<t, string>(list.ToArray(), x => x.ToString());
        }
    }
}
