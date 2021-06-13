using AlienGame.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienGame
{
    public class Present
    {
        public static List<Present> Presents = new List<Present>();

        public int Level { get; set; }
        public Sprite2D currentPresent { get; set; }
        public bool Go = false;
        public string Tag { get; set; }
        public bool Allowed = false;
    }
}
