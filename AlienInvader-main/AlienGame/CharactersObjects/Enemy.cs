using AlienGame.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienGame
{
   public class Enemy
    {
        public Sprite2D enemy { get; set; }
        public Sprite2D Ammo { get; set; }
        public Sprite2D Present { get; set; }
        public float Speed { get; set; }
        public static int CollisonIndex = 0;
        public int Level { get; set; }
        public int Life { get; set; }
        public static List<Enemy> Enemies = new List<Enemy>();
        public static int EnemiesCount = 20;
        public int EnemyCurrency { get; set; }

        public bool IsAlive()
        {
            bool x = false;

            if (this != null)
            {
                x = true;
            }

            return x;
        }
    }
}
