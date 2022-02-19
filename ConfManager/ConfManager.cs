using System;
using System.IO;
using System.Xml.Serialization;

namespace ConfManager
{
    /// <summary>
    /// ConfManager
    /// </summary>
    public class ConfManager
    {
        /// <summary>
        /// Save Settings
        /// </summary>
        /// <param name="obj">Object to save</param>
        /// <param name="path">File path</param>
        public static void Save(string path, object obj)
        {
            if (obj == null) throw new ConfManagerException(new ArgumentException(nameof(obj)));
            CheckPath(path);

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

        /// <summary>
        /// Load Settings
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="path">File path</param>
        /// <returns>Object</returns>
        public static T Load<T>(string path) => Load<T>(path, out _, out _);

        /// <summary>
        /// Load Settings
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="path">File path</param>
        /// <param name="createDateTime">Create Date</param>
        /// <param name="lastWriteTime">Last Change Date</param>
        /// <returns>Object</returns>
        /// <exception cref="ConfManagerException"></exception>
        public static T Load<T>(string path, out DateTime createDateTime, out DateTime lastWriteTime)
        {
            CheckPath(path);

            try
            {
                FileInfo fileInfo = new FileInfo(path);

                createDateTime = fileInfo.CreationTime;
                lastWriteTime = fileInfo.LastWriteTime;

                if (!fileInfo.Exists)
                    return default;

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    return (T)xmlSerializer.Deserialize(fileStream);
                }
            }
            catch (Exception exception)
            {
                throw new ConfManagerException(exception);
            }
        }


        /// <summary>
        /// Check file name
        /// </summary>
        /// <param name="path">File name</param>
        /// <exception cref="ConfManagerException">Exception is case of error</exception>
        private static void CheckPath(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ConfManagerException(new ArgumentException(nameof(path)));
            FileInfo fileInfo;
            try
            {
                fileInfo = new FileInfo(path);
            }
            catch (Exception exception)
            {
                throw new ConfManagerException(exception);
            }
            if (ReferenceEquals(fileInfo, null) || path.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
            {
                throw new ConfManagerException($"Invalid filename {path}");
            }
        }
    }
}