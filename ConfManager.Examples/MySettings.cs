using System.Collections.Generic;

namespace ConfManager.Examples
{
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
