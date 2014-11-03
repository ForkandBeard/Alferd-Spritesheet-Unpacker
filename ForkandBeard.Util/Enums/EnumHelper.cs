using System;
using System.Collections.Generic;
using System.Text;

namespace ForkandBeard.Util.Enums
{
    public class EnumHelper
    {
        public static enumType Parse<enumType>(string text) where enumType : struct, IConvertible
        {
            return (enumType)Enum.Parse(typeof(enumType), text);
        }

        public static List<T> GetEnums<T>() where T : struct, IConvertible
        {
            List<T> enums = new List<T>();
            foreach (string enumString in Enum.GetNames(typeof(T)))
            {
                enums.Add((T)Enum.Parse(typeof(T), enumString));
            }

            return enums;
        }
    }
}
