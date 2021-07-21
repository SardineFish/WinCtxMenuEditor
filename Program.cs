using System;
using System.IO;
using DefaultNamespace;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace ContextMenuEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var path = args.Length switch
            {
                > 1 => args[1],
                _ => "./config.json",
            };
            var config = JsonConvert.DeserializeObject<ContextMenuConfig>(File.ReadAllText(path));

            if (config.Directory) 
                CreateContextMenu(Registry.CurrentUser.OpenOrCreateKey($@"SOFTWARE\Classes\Directory\shell\{config.Key}"), config.Command);
            if (config.DirectoryBackground)
                CreateContextMenu(Registry.CurrentUser.OpenOrCreateKey($@"SOFTWARE\Classes\Directory\Background\shell\{config.Key}"), config.Command);
            if (config.Drive)
                CreateContextMenu(Registry.CurrentUser.OpenOrCreateKey($@"Software\Classes\Drive\shell\{config.Key}"), config.Command);
            if (config.FileExtensions is not null)
                foreach(var ext in config.FileExtensions)
                {
                    CreateContextMenu(Registry.ClassesRoot.OpenOrCreateKey($@"SystemFileAssociations\{ext}\shell\{config.Key}"), config.Command);
                }
        }

        static void CreateContextMenu(RegistryKey key, MenuCommand command)
        {
            if (command.SubCommands is not null)
            {
                key.SetValue("MUIVerb", command.Name);
                key.SetValue("subcommands", "");
                var shell = key.OpenSubKey("shell", true) ?? key.CreateSubKey("shell", true);
                Console.WriteLine($"Created Menu '{command.Name}'");
                foreach(var (subkeyName, subcmd) in command.SubCommands)
                {
                    var subkey = shell.CreateSubKey(subkeyName, true);
                    CreateContextMenu(subkey, subcmd);
                }
            }
            else
            {
                key.SetValue(null, command.Name);
                key.CreateSubKey("command", true).SetValue(null, command.Shell);
                if (command.Icon is not null or "")
                    key.SetValue("Icon", command.Icon);
                Console.Write($"Created Command '{command.Name}'");
            }
        }
    }
}
