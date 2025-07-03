using GTA;
using GTA.Math;
using GTA.UI;
using System;
using System.Drawing;

namespace GTAVWebhook.Types
{
    class Attacker
    {
        private Ped npc = null;
        private string name = null;

        public Attacker(string name, bool withPistol = false)
        {
            this.name = name;

            if (Game.Player.Character.IsInVehicle())
            {
                npc = World.CreateRandomPed((Game.Player.Character).Position + new Vector3(0, 0, 2) + (Game.Player.Character).ForwardVector * 3);
            }
            else
            {
                npc = World.CreateRandomPed(Game.Player.Character.Position);
            }


            if (withPistol || new Random().Next(1, 25) == 10)
            {
                npc.Weapons.Give(WeaponHash.Pistol, 100, true, true);
                npc.Task.ShootAt(Game.Player.Character, 30 * 1000, FiringPattern.Default);
            }
            else
            {
                npc.Task.FightAgainst(Game.Player.Character);
            }


            npc.MaxHealth = 100;
        }

        public void StartShooting(int duration)
        {
            if (npc != null)
            {
                npc.Weapons.Give(WeaponHash.Pistol, 100, true, true);
                npc.Task.ShootAt(Game.Player.Character, duration * 1000, FiringPattern.Default);
            }
        }

        public void Remove()
        {
            if (npc != null)
            {
                npc.Delete();
                npc = null;
            }
        }

        public void DrawName()
        {
             if (npc != null && name != null && World.GetDistance(npc.Position, Game.Player.Character.Position) <= 30 && npc.IsOnScreen)
            {
                PointF pointF = Screen.WorldToScreen(npc.Position, false);

                 int hash = npc.GetHashCode();
                int r = (hash & 0xFF0000) >> 16;
                int g = (hash & 0x00FF00) >> 8;
                int b = (hash & 0x0000FF);

                 Color npcColor = Color.FromArgb(255, r, g, b);
                 
                 new TextElement(name, pointF, 0.6f, npcColor, GTA.UI.Font.Pricedown, Alignment.Center).Draw();
            }
        }
    }
}
