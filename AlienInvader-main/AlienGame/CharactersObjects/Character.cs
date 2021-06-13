using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienGame
{
    public class Character
    {
        public float Speed { get; set; }
        public string Tag { get; set; }
        public Character(float _speed,string _tag)
        {
            this.Speed = _speed;
            this.Tag = _tag;
        }
    }
}
