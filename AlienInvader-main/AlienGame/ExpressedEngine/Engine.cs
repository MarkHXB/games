using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlienGame.Engine
{
    public class Canvas : Form
    {
        public Canvas()
        {
            this.SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer,
            true);
        }
        private Bitmap renderBmp;
        public override Image BackgroundImage
        {
            get
            {
                return renderBmp;
            }
            set
            {
                Image baseImage = value;

                renderBmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                Graphics g = Graphics.FromImage(renderBmp);
                // g.DrawImage(baseImage, 0, 0, 1025, 740); 
                g.DrawImage(baseImage, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                g.Dispose();
            }
        }

        
    }
    public abstract class Engine
    {
        #region NULLvariables
        public static Canvas Window = null;
        string Title = "";

        Color BackGroundColor = Color.FromArgb(0, 0, 117);

        public static Vector ScreenSize = null;

        public static Thread GameLoopThread = null;

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();
        public static List<Character> AllCharacters = new List<Character>();


        //just for test
        public static List<testPresent> AllPresents = new List<testPresent>();

        public static bool gameStart = true;

        #endregion

        #region PAINTtest

        SolidBrush sb = new SolidBrush(Color.White);
        Random r = new Random();
        public int valami = 0;
        #endregion

        public Engine(Vector screensize,string title)
        {
            this.Title = title;
            ScreenSize = screensize;         

            Window = new Canvas();
            
            Window.Size = new Size((int)ScreenSize.X, (int)ScreenSize.Y);
            Window.Text = this.Title;
            Window.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            //Window.BackgroundImage = Image.FromFile(@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Game\AlienGame\Assets\Sprite\wallpaper_1_edited.jpg");

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Window.Paint += Renderer;
            Window.FormClosing += Window_Closing;
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;
           
            Application.Run(Window);
        }

        

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        private void Window_Closing(object sender, FormClosingEventArgs e)
        {
            //ha pause van, akkor valami nem jó
            if (GameLoopThread.IsAlive)
                GameLoopThread.Abort();
            else
                GameLoopThread.Interrupt();
        }

        public static void RegisterShape(Shape2D shape)
        {
            try
            {
                AllShapes.Add(shape);
            }
            catch(Exception x)
            {
                Log.Error($"{x}");
            }
            
            Log.Info($"{shape.Tag} shape is registered successfully.");
        }
        public static void RegisterSprite(Sprite2D sprite)
        {
            try
            {
                AllSprites.Add(sprite);
            }
            catch (Exception x)
            {
                Log.Error($"{x}");
            }

            Log.Info($"{sprite.Tag} sprite is registered successfully.");
        }

        public static void UnRegisterSprite(Sprite2D sprite)
        {
            AllSprites.Remove(sprite);
        }

        public static void RegisterCharacter(Character cpc)
        {
            AllCharacters.Add(cpc);
        }



        public static void RegisterPresent(testPresent present)
        {
            try
            {
                AllPresents.Add(present);
            }
            catch (Exception x)
            {
                Log.Error($"{x}");
            }

            Log.Info($"{present.Tag} sprite is registered successfully.");
        }

        public static void UnRegisterPresent(testPresent present)
        {
            try
            {
                AllPresents.Remove(present);
            }
            catch (Exception x)
            {
                Log.Error($"{x}");
            }

            Log.Warning($"{present.Tag} sprite is already unrigestered.");
        }



        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackGroundColor);


            for (int i = 0; i < AllSprites.Count; i++)
            {
                g.DrawImage(AllSprites[i].Sprite, AllSprites[i].Position.X, AllSprites[i].Position.Y,
                    AllSprites[i].Scale.X, AllSprites[i].Scale.Y);
            }


            for (int i = 0; i < AllPresents.Count; i++)
            {
                g.DrawImage(AllPresents[i].Sprite, AllPresents[i].Position.X, AllPresents[i].Position.Y,
                    AllPresents[i].Scale.X, AllPresents[i].Scale.Y);
            }

            OnDraw(e);

        }

        

        void GameLoop()
        {
   
            OnLoad();

            while (GameLoopThread.IsAlive)
            {
                Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                OnUpdate();

               Thread.Sleep(10);
            }
        }

        //ABSTRACT METHODS
        public abstract void OnLoad();
        public abstract void OnDraw(PaintEventArgs e);
        public abstract void OnUpdate();
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
    }
}
