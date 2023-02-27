using ConfigManager.AbstractResource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigManager
{
    public class ConfigManager
    {
        public readonly Storage _storage;
        private FileInfo _configFile;
        public ConfigManager(string configFilePath = "Settings.json") 
        {
            this.Initialize(configFilePath);
            this._storage = new Storage(this._configFile);
        }
        
        private void Initialize(string configFilePath)
        {
            if (!File.Exists(configFilePath))
            {
                File.WriteAllText(configFilePath, "{}");
            }

            this._configFile = new FileInfo(configFilePath);
        }

        public void AddField(string key, object value)
        {
            this._storage.Add(key, value);
        }

        public void RemoveField(string key)
        {
            this._storage.RemoveField(key);
        }

        public Dictionary<string, object> Read()
        {
            return this._storage.Read();
        }

        public void Clear()
        {
            this._storage.Purge();
        }

        public void Save()
        {
            this._storage.Save();
        }
    }
}
