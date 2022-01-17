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
        private Scp3108 Scp3108;
        public override void OnEnabled()
        {
            Singleton = this;
            Scp3108 = new Scp3108();
            Scp3108.TryRegister();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Singleton = null;
            Scp3108.TryUnregister();
            Scp3108 = null;
            base.OnDisabled();
        }
    }
}