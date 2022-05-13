using System;
using System.IO;
using System.Xml.Serialization;

namespace ConfManager
{
    /// <summary>
    /// ConfWriter
    /// </summary>
    public class ConfWriter
    {
        /// <summary>
        /// Save Settings
        /// </summary>
        /// <param name="obj">Object to save</param>
        /// <param name="path">File path</param>
        public static void Write(string path, object obj)
        {
            if (obj == null) throw new ConfManagerException(new ArgumentException(nameof(obj)));
            Helper.CheckPath(path);

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                using (StreamWriter streamWriter = new StreamWriter(path))
                {
                    xmlSerializer.Serialize(streamWriter, obj);
                }
            }
            catch (Exception exception)
            {
                throw new ConfManagerException(exception);
            }
        }
    }
}
