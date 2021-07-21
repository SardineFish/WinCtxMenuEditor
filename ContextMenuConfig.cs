using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ContextMenuEditor
{
    public class ContextMenuConfig
    {
        [JsonProperty("directory")] 
        public bool Directory;
        [JsonProperty("directory_background")] 
        public bool DirectoryBackground;
        [JsonProperty("drive")] 
        public bool Drive;
        [JsonProperty("file_extensions")] 
        public string[] FileExtensions;

        [JsonProperty("key")] 
        public string Key;
        [JsonProperty("command")] 
        public MenuCommand Command;
    }

    public class MenuCommand
    {
        [JsonProperty("name")] 
        public string Name;

        [JsonProperty("shell")] 
        public string Shell;
        [JsonProperty("icon")] 
        public string Icon;
        
        [JsonProperty("sub_commands")] 
        public Dictionary<string, MenuCommand> SubCommands;
    }
}