using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using AlienGame.Engine;
using AlienGame.Characters;

namespace AlienGame
{
    public class Ammo
    {
        public Sprite2D CurrentAmmo;
        public bool Shoot;
        public Ammo()
        {
            Shoot = false;
        }
        public Ammo(Player player)
        {
            Shoot = false;

            if(player.Level == 0)
            {
                this.CurrentAmmo = new Sprite2D(new Vector(2000, 2000), new Vector(20, 40), $"level{player.Level}_ammo", "Ammo");
                
                DemoGame.Ammos.Add(this);
            }
            if (player.Level == 1)
            {
                this.CurrentAmmo = new Sprite2D(new Vector(2000, 2000), new Vector(20, 40), $"level{player.Level}_ammo", "Ammo");
                DemoGame.Ammos.Add(this);
                DemoGame.Ammos.Add(this);
            }
            else if (player.Level == 2)
            {
                this.CurrentAmmo = new Sprite2D(new Vector(2000, 2000), new Vector(20, 40), $"level{player.Level}_ammo", "Ammo");

                DemoGame.Ammos.Add(this);
                DemoGame.Ammos.Add(this);
                DemoGame.Ammos.Add(this);
            }
            else if (player.Level == 3)
            {
                this.CurrentAmmo = new Sprite2D(new Vector(player.Interface.Position.X, player.Interface.Position.Y), new Vector(20, 40), $"level{player.Level}_ammo", "Ammo");

                DemoGame.Ammos.Add(this);
                DemoGame.Ammos.Add(this);
                DemoGame.Ammos.Add(this);
                DemoGame.Ammos.Add(this);
            }
        }
        public static void RegisterAmmo(Ammo ammo)
        {
            DemoGame.Ammos.Add(ammo);
        }
        public static void DestroySelf(Ammo ammo)
        {
            DemoGame.Ammos.Remove(ammo);
            ammo.CurrentAmmo.DestroySelf();
        }
    }
}
