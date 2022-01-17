using System;
using System.Collections.Generic;
using System.ComponentModel;
using Discord;
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
        public override uint Id { get; set; } = 24;
        public override string Name { get; set; } = "SCP-3108";
        public override string Description { get; set; } = "The Nerfing Gun";
        public override float Weight { get; set; } = 1f;
        public override ItemType Type { get; set; } = ItemType.GunRevolver;
        
        [Description("The amount of damage SCP-3108 deals to a player")]
        public float DamageAmount { get; set; } = 60f;

        public List<ItemType> Items { get; set; } = new List<ItemType>()
        {
            ItemType.Coin,
            ItemType.Adrenaline
        };

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            DynamicSpawnPoints = new List<DynamicSpawnPoint>()
            {
                new DynamicSpawnPoint()
                {
                    Chance = 50,
                    Location = SpawnLocation.InsideLocker,
                },
                new DynamicSpawnPoint()
                {
                    Chance = 50,
                    Location = SpawnLocation.Inside173Bottom
                }
            },
            Limit = 2,
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
            if (Physics.Raycast(ev.Shooter.CameraTransform.position, ev.Shooter.CameraTransform.forward, out RaycastHit hit, 500f))
            {
                var ipb = hit.collider.GetComponentInParent<ItemPickupBase>();
                if (ipb == null) return;
                new Item(Items[UnityEngine.Random.Range(0, Items.Count)]).Spawn(ipb.Rb.position, ipb.Rb.rotation);
                ipb.DestroySelf();
            }
        }
    }
}