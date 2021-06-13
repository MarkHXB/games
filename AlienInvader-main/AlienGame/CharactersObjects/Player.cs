using AlienGame.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienGame.Characters
{
    public class Player
    {
        public Sprite2D Interface { get; set; }
        public Sprite2D Ammo { get; set; }
        public int Points { get; set; }
        public int Life { get; set; }
        public float Speed { get; set; }
        public static int ShootedEnemies = 0;
        public int Level = 0;
        public Player()
        {
            Points = 0;
            Life = 2;
            Speed = 4f;
        }
        public static void gameOver(bool gameover)
        {
            if (gameover)
            {


                Engine.Engine.GameLoopThread.Abort();             
            }
        }

        

        
    }
}
