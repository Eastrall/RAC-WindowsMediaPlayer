using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/*--------------------------------------------------------
 * XmlSerializer.cs - file description
 * 
 * Version: 1.0
 * Author: Filipe
 * Created: 03/01/2015 11:22:13
 * 
 * Notes:
 * -------------------------------------------------------*/

namespace WindowsMediaPlayer.Serializer
{
    public class XmlSerializer
    {
        /// <summary>
        /// Serialize un objet 'obj' passé en parametre de type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="sw"></param>
        public static void Serialize<T>(T obj, String path)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer _xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (StreamWriter _sw = new StreamWriter(path))
                {
                    _xs.Serialize(_sw, obj);
                }
            }
            catch { }
        }

        /// <summary>
        /// Deserialisation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static T Deserialize<T>(StreamReader sr)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer _xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (sr)
                {
                    return (T)((Object)_xs.Deserialize(sr));
                }
            }
            catch { }
            return default(T);
        }
    }
}
