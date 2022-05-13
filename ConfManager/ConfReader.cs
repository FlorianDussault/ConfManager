using System;
using System.IO;
using System.Xml.Serialization;

namespace ConfManager
{
    /// <summary>
    /// Configuration Reader
    /// </summary>
    public static class ConfReader
    {
        /// <summary>
        /// Load Settings
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="path">File path</param>
        /// <returns>Object</returns>
        public static T Read<T>(string path) => Read<T>(path, out _, out _);

        /// <summary>
        /// Load Settings
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="path">File path</param>
        /// <param name="createDateTime">Create Date</param>
        /// <param name="lastWriteTime">Last Change Date</param>
        /// <returns>Object</returns>
        /// <exception cref="ConfManagerException"></exception>
        public static T Read<T>(string path, out DateTime createDateTime, out DateTime lastWriteTime)
        {
            Helper.CheckPath(path);

            try
            {
                FileInfo fileInfo = new FileInfo(path);

                createDateTime = fileInfo.CreationTime;
                lastWriteTime = fileInfo.LastWriteTime;

                if (!fileInfo.Exists)
                    return default;

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    return (T)xmlSerializer.Deserialize(fileStream);
                }
            }
            catch (Exception exception)
            {
                throw new ConfManagerException(exception);
            }
        }
    } 
}