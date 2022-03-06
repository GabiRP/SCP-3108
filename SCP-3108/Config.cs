using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.API.Interfaces;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using Exiled.Events.Commands.Reload;
using Exiled.Loader;
using UnityEngine;

namespace SCP_3108
{
    public class Config : IConfig
    {
        public Items ItemConfigs;

        public bool IsEnabled { get; set; } = true;
        public string ItemConfigFolder { get; set; } = Path.Combine(Paths.Configs, "CustomItems");
        public string ItemConfigFile { get; set; } = "global.yml";

        
        public void LoadItems()
        {
            if (!Directory.Exists(ItemConfigFolder))
                Directory.CreateDirectory(ItemConfigFolder);

            string filePath = Path.Combine(ItemConfigFolder, ItemConfigFile);
            Log.Info($"{filePath}");
            if (!File.Exists(filePath))
            {
                ItemConfigs = new Items();
                File.WriteAllText(filePath, Loader.Serializer.Serialize(ItemConfigs));
            }
            else
            {
                ItemConfigs = Loader.Deserializer.Deserialize<Items>(File.ReadAllText(filePath));
                File.WriteAllText(filePath, Loader.Serializer.Serialize(ItemConfigs));
            }
        }
    }
}