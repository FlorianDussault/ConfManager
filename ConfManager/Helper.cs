using System;
using System.IO;

namespace ConfManager
{
    /// <summary>
    /// Helper
    /// </summary>
    internal static class Helper
    {
        /// <summary>
        /// Check file name
        /// </summary>
        /// <param name="path">File name</param>
        /// <exception cref="ConfManagerException">Exception is case of error</exception>
        public static void CheckPath(string path)
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
            if (ReferenceEquals(fileInfo, null) || fileInfo.Name.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
            {
                throw new ConfManagerException($"Invalid filename {path}");
            }
        }
    }
}
