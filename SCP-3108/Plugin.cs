using System;
using System.Net;
using Exiled.API.Features;

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
            Config.ItemConfig.TryRegister();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Singleton = null;
            Config.ItemConfig.TryUnregister();
            base.OnDisabled();
        }
    }
}