using AlienGame.Characters;
using AlienGame.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AlienGame
{
    public class testPresent
    {
        public static List<testPresent> Presents = new List<testPresent>();

        public int Level { get; set; }
        public bool Go = false;

        public bool Allowed = false;

        public Image Sprite;
        public Vector Position { get; set; }
        public Vector Scale { get; set; }
        public string Directory = "";
        public string Tag = "";

        public testPresent(Vector _position, Vector _scale, string _directory, string _tag, int _level)
        {
            //DEFINE BASIC PROPERTIES
            this.Position = _position;
            this.Scale = _scale;
            this.Directory = _directory;
            this.Tag = _tag;
            Go = false;
            Allowed = true;
            this.Level = _level;

            Image tmp = Image.FromFile($@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Game\AlienGame\Assets\Sprite\{Directory}.png");
            Bitmap sprite = new Bitmap(tmp, (int)this.Scale.X, (int)this.Scale.Y);
            Sprite = sprite;


            Engine.Engine.RegisterPresent(this);
        }

        public bool IsColliding(Player player)
        {
            bool van = false;

            for (int i = 0; i < Engine.Engine.AllPresents.Count; i++)
            {
                if (player.Interface.Position.X < Engine.Engine.AllPresents[i].Position.X + Engine.Engine.AllPresents[i].Scale.X
                    && player.Interface.Position.X + player.Interface.Scale.X > Engine.Engine.AllPresents[i].Position.X
                    && player.Interface.Position.Y < Engine.Engine.AllPresents[i].Position.Y + Engine.Engine.AllPresents[i].Scale.Y
                    && player.Interface.Position.Y + player.Interface.Scale.Y > Engine.Engine.AllPresents[i].Position.Y)
                {
                    van = true;
                }
            }
            return van;
        }

        public static void DestroySelf(testPresent present)
        {
            Engine.Engine.UnRegisterPresent(present);
        }

    }
}