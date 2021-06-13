using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlienGame
{
    public class MenuClass
    {
        public Point menuPanel = new Point((int)Engine.Engine.ScreenSize.X / 2, (int)Engine.Engine.ScreenSize.Y / 2);
        public static Font menuText = new Font("MS Gothic", 24f);
        public static Color menuBackColor = Color.FromArgb(100, Color.Black);
        public static Color menuTextColor = Color.White;
        public static Color menuSelectedRow = Color.Yellow;

        public static Button resumeButton = new Button
        {
            Size = new Size(100, 40),
            Location = new Point(400, 370),
            BackColor = Color.Transparent,
            FlatStyle = FlatStyle.Flat,
            Text = "Resume",
            ForeColor = Color.White,
            Font = menuText
        };
       
    }
}
