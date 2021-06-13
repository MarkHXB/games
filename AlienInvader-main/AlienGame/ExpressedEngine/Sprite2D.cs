using AlienGame.Characters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienGame.Engine
{
    public class Sprite2D
    {
        public Vector Position;
        public Vector Scale;
        public Image Sprite;
        public string Directory = "";
        public string Tag = "";
        public bool IsReference = false;
  

        public Sprite2D(Vector _position, Vector _scale, string _directory, string _tag)
        {
            this.Position = _position;
            this.Scale = _scale;
            this.Directory = _directory;
            this.Tag = _tag;

            Image tmp = Image.FromFile($@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Game\AlienGame\Assets\Sprite\{Directory}.png");
            Bitmap sprite = new Bitmap(tmp, (int)this.Scale.X, (int)this.Scale.Y);
            Sprite = sprite;


            Engine.RegisterSprite(this);
        }


        public Sprite2D(Vector _Position, Vector _Scale,
            Sprite2D reference, string _Tag)
        {
            this.Position = _Position;
            this.Scale = _Scale;
            this.Tag = _Tag;

            Sprite = reference.Sprite;

            Engine.RegisterSprite(this);
        }

        //JUST FOR ENEMIES
        /*public Sprite2D(Vector _position, Vector _scale,string _directory,
            string _tag,int _level,int _enemyLife)
        {
            this.Position = _position;
            this.Scale = _scale;
            this.Directory = _directory;
            this.Tag = _tag;
            

            Image tmp = Image.FromFile($@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Game\AlienGame\Assets\Sprite\{Directory}.png");
            Bitmap sprite = new Bitmap(tmp, (int)this.Scale.X, (int)this.Scale.Y);
            Sprite = sprite;

            Engine.RegisterSprite(this);
        }*/

        //JUST FOR PLANETS
        public Sprite2D(Vector _position, Vector _scale, string _directory)
        {
            this.Position = _position;
            this.Scale = _scale;
            this.Directory = _directory;

            Image tmp = Image.FromFile($@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Game\AlienGame\Assets\Sprite\{Directory}.png");
            Bitmap sprite = new Bitmap(tmp, (int)this.Scale.X, (int)this.Scale.Y);
            Sprite = sprite;

            Engine.RegisterSprite(this);
        }



        public void DestroySelf()
        {
            Player.ShootedEnemies++;

            Log.Warning(this.Tag + " " + "was unrigestered");
            

            Engine.UnRegisterSprite(this);
        }


        


        public Sprite2D IsColliding(string tag)
        {
            int a = 0;
            for (int i=0;i<Engine.AllSprites.Count;i++)
            {
                if (Engine.AllSprites[i].Tag == tag)
                {
                    if (Position.X < Engine.AllSprites[i].Position.X + Engine.AllSprites[i].Scale.X
                        && Position.X + Scale.X > Engine.AllSprites[i].Position.X
                        && Position.Y < Engine.AllSprites[i].Position.Y + Engine.AllSprites[i].Scale.Y
                        && Position.Y + Scale.Y > Engine.AllSprites[i].Position.Y)
                    {
                        Enemy.CollisonIndex = a;
                        return Engine.AllSprites[i];
                    }
                    a++;
                }
            }
            return null;
        }

        public Sprite2D IsAmmoColliding(string tag)
        {
            int a = 0;
            for (int i = 0; i < Engine.AllSprites.Count; i++)
            {
                if (Engine.AllSprites[i].Tag == tag)
                {
                    if ( Position.Y == Engine.AllSprites[i].Position.Y
                        && Position.X < Engine.AllSprites[i].Position.X + Engine.AllSprites[i].Scale.X
                        && Position.X + Scale.X > Engine.AllSprites[i].Position.X)
                    {
                        Enemy.CollisonIndex = a;
                        return Engine.AllSprites[i];
                    }
                    a++;
                }
            }
            return null;
        }
    }
}
