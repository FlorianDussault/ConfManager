namespace ConfManager
{
    public static class ConfManagerExtension
    {
        /// <summary>
        /// Save Settings
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="path">Path</param>
        public static void SaveSettings(this object obj, string path) => ConfWriter.Write(path, obj);
    }
}
