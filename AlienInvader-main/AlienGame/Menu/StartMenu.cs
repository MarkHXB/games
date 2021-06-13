using AlienGame.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlienGame.Menu
{
    public class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }
    public abstract class StartMenu
    {
        public string Title = "";
        public Canvas Window;
        public Vector ScreenSize { get; set; }
        public Thread MenuThread;
        Color BackGroundColor = Color.FromArgb(0, 0, 117);


        public StartMenu(Vector _ScreenSize,string _Title)
        {
            Window = new Canvas();
            this.ScreenSize = _ScreenSize;
            this.Title = _Title;

            Window.Text = this.Title;
            Window.FormBorderStyle = FormBorderStyle.None;
            Window.Size = new System.Drawing.Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y);

            MenuThread = new Thread(MenuThreadLoop);
            MenuThread.Start();

            Window.Paint += MenuPain_Renderer;
        }

        private void MenuPain_Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackGroundColor);


        }

        private void MenuThreadLoop()
        {
            OnLoad();
            while (MenuThread.IsAlive)
            {
                OnDraw();
                Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                OnUpdate();
            }
        }
        public abstract void OnDraw();
        public abstract void OnLoad();
        public abstract void OnUpdate();
    }
}
