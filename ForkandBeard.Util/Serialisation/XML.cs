using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace ForkandBeard.Util.Serialisation
{
    public class XML
    {
        public static string SerialiseToString(object toSerialise)
        {
            XmlSerializer serialiser = new System.Xml.Serialization.XmlSerializer(toSerialise.GetType());
            using (StringWriter sw = new StringWriter())
            {
                serialiser.Serialize(sw, toSerialise);
                return sw.ToString();
            }
        }

        public static T DeserialiseFromString<T>(string xml)
        {
            T item;

            XmlSerializer serialiser = new System.Xml.Serialization.XmlSerializer(typeof(T));
            item = (T)serialiser.Deserialize(new StringReader(xml));
            return item;
        }

        public static void Serialise(object toSerialise, string path)
        {
            XmlSerializer serialiser = new System.Xml.Serialization.XmlSerializer(toSerialise.GetType());

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate))
            {
                serialiser.Serialize(fs, toSerialise);
            }
        }

        public static T Deserialise<T>(string path)
        {
            if (File.Exists(path))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    return Deserialise<T>(fs);
                }
            }
            else
            {
                return default(T);
            }  
        }

        public static T Deserialise<T>(FileStream xmlStream)
        {
            T item;

            XmlSerializer serialiser = new System.Xml.Serialization.XmlSerializer(typeof(T));
            item = (T)serialiser.Deserialize(xmlStream);
            return item;
        }
    }
}
