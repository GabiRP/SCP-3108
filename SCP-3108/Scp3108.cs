using System;
using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using InventorySystem.Items.Pickups;
using Mirror;
using PlayerStatsSystem;
using UnityEngine;

namespace SCP_3108
{
    public class Scp3108 : CustomItem
    {
        public override uint Id { get; set; } = Plugin.Singleton.Config.ItemId;
        public override string Name { get; set; } = "SCP-3108";
        public override string Description { get; set; } = "The Nerfing Gun";
        public override float Weight { get; set; } = Plugin.Singleton.Config.ItemWeight;
        public override ItemType Type { get; set; } = ItemType.GunRevolver;

        private Dictionary<Scp3108, uint> TimesShot = new Dictionary<Scp3108, uint>();

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            DynamicSpawnPoints = Plugin.Singleton.Config.PossibleSpawnPoints,
            Limit = Plugin.Singleton.Config.ItemLimit,
        };

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Shooting += OnShooting;
            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Shooting -= OnShooting;
            base.UnsubscribeEvents();
        }

        internal void OnShooting(ShootingEventArgs ev)
        {
            if(!Check(ev.Shooter.CurrentItem)) return;
            ev.IsAllowed = false;
            if (TimesShot.TryGetValue(this, out uint value))
            {
                //Do this
            }
            if (Physics.Raycast(ev.Shooter.CameraTransform.position, ev.Shooter.CameraTransform.forward, out RaycastHit hit, 500f))
            {
                var ipb = hit.collider.GetComponentInParent<ItemPickupBase>();
                if (ipb == null) return;
                new Item(Plugin.Singleton.Config.ReplacedItems[UnityEngine.Random.Range(0, Plugin.Singleton.Config.ReplacedItems.Count)]).Spawn(ipb.Rb.position, ipb.Rb.rotation);
                ipb.DestroySelf();
            }
        }
    }
}