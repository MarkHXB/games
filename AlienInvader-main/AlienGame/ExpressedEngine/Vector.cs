using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienGame.Engine
{
    public class Vector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Vector()
        {
            this.X = Zero().X;
            this.Y = Zero().Y;
        }
        public Vector(float x,float y)
        {
            X = x;
            Y = y;
        }
        public static Vector Zero()
        {
            return new Vector(0, 0);
        }
    }
}
