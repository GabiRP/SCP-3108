using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API;
using Exiled.Events.EventArgs;
using InventorySystem.Items.Pickups;
using Mirror;
using PlayerStatsSystem;
using UnityEngine;

namespace SCP_3108
{
    public class Scp3108 : CustomItem
    {
        public override uint Id { get; set; } = 24;
        public override string Name { get; set; } = "SCP-3108";
        public override string Description { get; set; } = "The Nerfing Gun";
        public override float Weight { get; set; } = 1;
        public override ItemType Type { get; set; } = ItemType.GunRevolver;
        
        public List<ItemType> ReplacedItems { get; set; }= new List<ItemType>
        {
            ItemType.Adrenaline,
            ItemType.Coin
        };

        public uint Bullets { get; set; } = 8;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            DynamicSpawnPoints = new List<DynamicSpawnPoint>(){
                new DynamicSpawnPoint()
                {
                    Chance = 50,
                    Location = SpawnLocation.Inside914,
                },
                new DynamicSpawnPoint()
                {
                    Chance = 50,
                    Location = SpawnLocation.InsideLocker,
                },
            },
            Limit = 1,
        };

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Shooting += OnShooting;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            base.SubscribeEvents();
        }
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Shooting -= OnShooting;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            base.UnsubscribeEvents();
        }

        internal void OnShooting(ShootingEventArgs ev)
        {
            if(!Check(ev.Shooter.CurrentItem)) return;
            if (Physics.Raycast(ev.Shooter.CameraTransform.position, ev.Shooter.CameraTransform.forward, out RaycastHit hit, 500f))
            {
                var ipb = hit.collider.GetComponentInParent<ItemPickupBase>();
                if (ipb == null) return;
                new Item(ReplacedItems[UnityEngine.Random.Range(0, ReplacedItems.Count)]).Spawn(ipb.Rb.position, ipb.Rb.rotation);
                ipb.DestroySelf();
            }
        }
        internal void OnHurting(HurtingEventArgs ev)
        {
            if (Check(ev.Handler.Item))
                ev.IsAllowed = false;
        }
    }
}