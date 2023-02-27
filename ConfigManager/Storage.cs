using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigManager.AbstractResource
{
    public class Storage : Dictionary<string, object>
    {
        private readonly FileInfo _configFile;

        public Storage(FileInfo configFile)
        {
            this._configFile = configFile;
        }

        public void AddField(string key, object value)
        {
            if (this.ContainsKey(key)) {
                this[key] = value; ;
            } 
            else {
                this.Add(key, value);
            }
        }
        public void RemoveField(string key)
        {
            if (!this.ContainsKey(key)) {
                return;
            }
            this.Remove(key);
        }

        public Dictionary<string, object> Read()
        {
            string content = File.ReadAllText(this._configFile.FullName);
            dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(content);
            foreach(KeyValuePair<string, object> kvp in jsonData) {
                this.AddField(kvp.Key, kvp.Value);
            }

            return this;
        }

        public void Purge()
        {
            this.Clear();
            this.Save();
        }
        public void Save()
        {
            string jsonContent = JsonConvert.SerializeObject(this);
            File.WriteAllText(this._configFile.FullName, jsonContent);
        }
    }
}
