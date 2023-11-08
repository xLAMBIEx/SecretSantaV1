using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace SecretSantaV1
{
    public static class HelperClass
    {
        private static string fileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static void SerializeToFile(string fileName, object item)
        {
            if (Directory.Exists(fileDirectory))
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(fileDirectory, fileName), false))
                {
                    DataContractSerializer serializer = new DataContractSerializer(item.GetType());
                    serializer.WriteObject(sw.BaseStream, item);
                }
            }
        }

        public static T DeserializeFromFile<T>(string fileName)
        {
            T retObj = default(T);

            if(File.Exists(Path.Combine(fileDirectory, fileName))) 
            {
                using(StreamReader sr = new StreamReader(Path.Combine(fileDirectory, fileName)))
                {
                    DataContractSerializer deserializer = new DataContractSerializer(typeof(T));
                    retObj = (T)deserializer.ReadObject(sr.BaseStream);
                }
            }

            return retObj;
        }
    }
}
