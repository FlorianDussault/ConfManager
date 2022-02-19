using System.Collections.Generic;

namespace ConfManager.Examples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Build Settings
            MySettings settings = new MySettings
            {
                ConnectionName = "Production server",
                Enabled = true,
                Username = "admin",
                Password = "123456789",
                Server = "prod.server.com",
                Port = 8080,
                Paths = new List<string> { @"C:\Temp", @"\\quality.server.com\shared$" }
            };

            // Save
            ConfManager.Save("app.settings.xml", settings);
            // or
            settings.SaveSettings("app.settings.xml");

            // Load settings

            MySettings mySettings = ConfManager.Load<MySettings>("app.settings.xml");

            // And we can use the settings
            // Server server = new Server(mySettings.Server, mySettings.Port);
            // ...
        }
    }

    public class MySettings
    {
        public string ConnectionName { get; set; }
        public bool Enabled { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public List<string> Paths;
    }
}
