using System;
using System.Net;
using Exiled.API.Features;
using Exiled.CustomItems.API;

namespace SCP_3108
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "SCP-3108";
        public override string Prefix => "scp_3108";
        public override string Author => "GabiRP";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(4, 2, 2);

        public static Plugin Singleton;
        public override void OnEnabled()
        {
            Singleton = this;
            Config.LoadItems();
            Config.ItemConfigs.Scp3108.Register();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Singleton = null;
            Config.ItemConfigs.Scp3108.Unregister();
            base.OnDisabled();
        }
    }
}