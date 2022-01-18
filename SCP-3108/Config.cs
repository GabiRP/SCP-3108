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
        [Description("Item Configuration")] public Scp3108 ItemConfig { get; set; } = new Scp3108();
    }
}