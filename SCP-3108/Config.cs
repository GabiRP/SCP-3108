using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Features.Spawn;
using Exiled.API.Interfaces;
using Exiled.CustomItems.API;
using UnityEngine;

namespace SCP_3108
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("CustomItem Id, leave this at 24 if there isn't any problem")]
        public uint ItemId { get; set; } = 24;
        [Description("Weight of The Nerfing Gun")]
        public float ItemWeight { get; set; } = 1f;
        [Description("The limit of Nerfing Guns that can appear in a round")]
        public uint ItemLimit { get; set; } = 1;

        [Description("Where The Nerfing Gun can appear")]
        public List<DynamicSpawnPoint> PossibleSpawnPoints { get; set; } = new List<DynamicSpawnPoint>()
        {
            new DynamicSpawnPoint()
            {
                Chance = 50,
                Location = SpawnLocation.InsideLocker,
            },
            new DynamicSpawnPoint()
            {
                Chance = 30,
                Position = new Vector3(53, 41, -45)
            },
        };
        [Description("The items the shooted item can be replaced with")]
        public List<ItemType> ReplacedItems = new List<ItemType>
        {
            ItemType.Adrenaline,
            ItemType.Coin
        };
    }
}