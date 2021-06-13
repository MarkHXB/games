using AlienGame.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace AlienGame.Engine
{
    public class DemoGame : Engine
    {
        #region PLAYER
        public static Player player = null;
       
        //Player movement
        bool up;
        bool down;
        bool left;
        bool right;
        public static bool gamePause=false;
        bool gamePause_2 = false;
        bool gameOver;
        bool gameWin = false;
        bool givePresent = false;
        bool upgradeIsComplete = false;
        bool presentGo = false;

        //Sprite2D Present = null;

        Vector playerLastPos = Vector.Zero();

        Image lifeImage = Image.FromFile(@"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Game\AlienGame\Assets\Sprite\life.png");

        List<Sprite2D> playerLives = new List<Sprite2D>();
        #endregion

        #region MAP

        //planets
        List<Sprite2D> Planets = new List<Sprite2D>();

        //stars
        List<float> posX = new List<float>();
        List<float> posY = new List<float>();

        //wallpapers
        //List<Sprite2D> Wallpapers = new List<Sprite2D>();

        //meteors
        List<Sprite2D> Meteors = new List<Sprite2D>();

        //Achieved Maps
        int completedMaps = 1;

        //Current achieved level
        private bool currentWin = false;


        

        #endregion

        #region ENEMIES



        #endregion

        #region AMMO

        public static List<Ammo> Ammos=new List<Ammo>();

        Ammo ammo;

        #endregion

        #region PRESENT

        bool ammoUpgradeAllowed = false;
        bool lifeUpgradeAllowed = false;

        List<testPresent> testPresents = new List<testPresent>();

        #endregion

        #region etc.

        bool keyPressed = false;

        WindowsMediaPlayer shootSound = new WindowsMediaPlayer();
        WindowsMediaPlayer presentSound = new WindowsMediaPlayer();
        WindowsMediaPlayer enemyBlowingUpSound = new WindowsMediaPlayer();

        #endregion

        public DemoGame() : base(new Vector(900, 1000), "Demo Game") { }

        [Obsolete]
        public override void GetKeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left) { left = true; }
            if (e.KeyCode == Keys.Right) { right = true; }
            if (e.KeyCode == Keys.Up) { up = true; }
            if (e.KeyCode == Keys.Down) { down = true; }
            if (e.KeyCode == Keys.Space && !keyPressed)
            {
                
                bool stop = false;
                int cc = 0;
                keyPressed = true;

                while (stop != true)
                {
                    
                    if (!Ammos[cc].Shoot)
                    {
                        Ammos[cc].CurrentAmmo = new Sprite2D(new Vector(player.Interface.Position.X + 20, player.Interface.Position.Y), new Vector(20, 40), $"level{player.Level}_ammo", "Ammo");
                        Ammos[cc].Shoot = true;
                        stop = true;
                        shootSound.settings.volume = 10;
                        shootSound.controls.play();
                    }

                    if (cc != Ammos.Count-1)
                        cc++;
                    else
                    {   
                        stop = true;
                    }
                }
            }

            if (e.KeyCode == Keys.P && !gamePause)
            {
                gamePause = true;
                keyPressed = true;
                GameLoopThread.Suspend();
            }
            if (e.KeyCode == Keys.P && gamePause && !keyPressed) { gamePause = false; GameLoopThread.Resume(); }
            
        }
        
        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.Right) { right = false; }
            if (e.KeyCode == Keys.Up) { up = false; }
            if (e.KeyCode == Keys.Down) { down = false; }
            if(e.KeyCode == Keys.P) { keyPressed = false; }
            if (e.KeyCode == Keys.Space) { keyPressed = false; }
        }

        public override void OnDraw(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Point scorePont = new Point((int)Engine.ScreenSize.X / 10, (int)Engine.ScreenSize.Y / 10);
            SolidBrush scoreBrush = new SolidBrush(Color.White);
            Font scoreFont = new Font("MS Gothic", 22);

            float speed = 2f;
            Random r = new Random();

            try
            {
                if (gamePause)
                {
                    g.FillRectangle(new SolidBrush(MenuClass.menuBackColor), 320, 380, 240, 50);
                    g.DrawString("Game is paused", MenuClass.menuText, new SolidBrush(Color.White), 320, 400);
                }
                if (gameOver)
                { 
                    Button button = new Button
                    {
                        Text="Finish",
                        Size = new Size(50,50),
                        Location = new Point((int)Engine.ScreenSize.X / 2, (int)Engine.ScreenSize.Y / 2),
                    };

                    Engine.Window.Controls.Add(button);
                    Log.Info($"{button} element is registered");

                    button.Click += GameOver_Click;
                }
                
                for (int i = 0; i < 10; i++)
                {
                    posY[i] += speed;
                    if (posY[i] > Engine.ScreenSize.Y)
                    {
                        posY[i] = r.Next(-500, 22);
                    }
                    g.FillEllipse(new SolidBrush(Color.White), posX[i], posY[i], 5, 5);
                }

                g.DrawString("Points: ", scoreFont, scoreBrush, new Point(20,35));
                g.DrawString(player.Points.ToString(), scoreFont, scoreBrush, 150,35);

                g.DrawString("Level: ", scoreFont, scoreBrush, new Point(690,35));
                g.DrawString(player.Level.ToString(), scoreFont, scoreBrush, 790,35);

            }
            catch (Exception x)
            {
                Log.Warning(x.Message);
            }
        }

        public override void OnLoad()
        {
            #region .etc


            shootSound.URL = @"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Game\AlienGame\Assets\Sound\shoot.wav";
            shootSound.settings.volume =0;

            enemyBlowingUpSound.URL = @"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Game\AlienGame\Assets\Sound\enemyBlowUp.wav";
            enemyBlowingUpSound.settings.volume = 0;

            presentSound.URL = @"C:\Users\bakon\OneDrive\Asztali gép\Infó\C#\Game\AlienGame\Assets\Sound\present.wav";
            presentSound.settings.volume = 0;

            #endregion

            Random r = new Random();
            player = new Player();

            for (int i = 0; i < 10; i++)
            {
                posX.Add(r.Next(10,900));
                if (i > 5)
                    posY.Add(r.Next(-1000, -500));
                else
                    posY.Add(r.Next(-500, 0));
            }

            try
            {
                #region MAP

                //planets
                Planets.Add(new Sprite2D(new Vector(r.Next(0, 901), r.Next(-200, -100)), new Vector(100, 70), "Jupi"));
                Planets.Add(new Sprite2D(new Vector(r.Next(0, 901), r.Next(-3000, -1500)), new Vector(100, 70), "Saturn"));
                //Planets.Add(new Sprite2D(new Vector(r.Next(0, 901), r.Next(-1100, -600)), new Vector(100, 70), "Saturn"));

                //wallpapers
                //Wallpapers.Add(new Sprite2D(new Vector(0, 0), new Vector(Engine.ScreenSize.X, Engine.ScreenSize.Y), "wallpaper_1_edited", "Wallpaper", "jpg", true));
            

                #endregion

                #region PLAYER

                player.Interface = new Sprite2D(new Vector(500, 500), new Vector(60, 60), "player", "Player");

                //singleAmmo = new Sprite2D(new Vector(player.Interface.Position.X, player.Interface.Position.Y),
                   //new Vector(20, 40), "level1_ammo", "Ammo");

                playerLives.Add(new Sprite2D(new Vector(800, 100), new Vector(30, 30), "life", "Life"));
                playerLives.Add(new Sprite2D(new Vector(750, 100), new Vector(30, 30), "life", "Life"));

                #endregion

                #region ENEMY

                for (int i = 0; i < Enemy.EnemiesCount; i++)
                {
                    Enemy.Enemies.Add(new Enemy
                    {
                        Ammo = null,
                        enemy = new Sprite2D(new Vector(r.Next(0, 900), r.Next(-1000, -30))
                        , new Vector(60, 60), "E2", "Enemy"),
                        EnemyCurrency=1,
                        Speed=1.2f,
                        Level=1,
                        Life=1
                    });
                }

                #endregion               

                #region PRESENT

                /*Present = new Sprite2D(new Vector(2000,2000),
                   new Vector(70, 60), "ammo-upgrade-lvl1", "Present");*/

                for (int i = 0; i < 3; i++)
                {
                    testPresents.Add(new testPresent(new Vector(2000, 2000), new Vector(70, 60), $"ammo-upgrade-lvl{i + 1}", $"Ammo{i+1}", i + 1));
                }
                testPresents.Add(new testPresent(new Vector(2000, 2000), new Vector(70, 60), "life", "Life", 1));

                Present.Presents.Add(new Present
                {
                    currentPresent = new Sprite2D(new Vector(2000, 2000),
                   new Vector(70, 60), "ammo-upgrade-lvl1", "Present"),
                    Go = false,
                    Level = 1,
                    Allowed = true,
                    Tag="Ammo1"
                });
                Present.Presents.Add(new Present
                {
                    currentPresent = new Sprite2D(new Vector(2000, 2000),
                   new Vector(70, 60), "ammo-upgrade-lvl2", "Present"),
                    Go = false,
                    Level = 2,
                    Allowed = true,
                    Tag="Ammo2"
                });
                Present.Presents.Add(new Present
                {
                    currentPresent = new Sprite2D(new Vector(2000, 2000),
                   new Vector(70, 60), "ammo-upgrade-lvl3", "Present"),
                    Go = false,
                    Level = 3,
                    Allowed = true,
                    Tag = "Ammo3"
                });
                Present.Presents.Add(new Present
                {
                    currentPresent = new Sprite2D(new Vector(2000, 2000),
                   new Vector(40, 30), "life", "Present"),
                    Go = false,
                    Level = 1,
                    Allowed = true,
                    Tag="Life"
                });


                #endregion

                #region AMMO

                ammo = new Ammo(player);

                #endregion

                //THIS SECTIONS IS FOR TESTING


                //END
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
            }
        }

        public override void OnUpdate()
        {
            //JUST FOR TESTING
            ReleaseUpgrades();

            //ENDING

            if (Player.ShootedEnemies == Enemy.EnemiesCount)
            {
                Win();
            }

            if(player.Life == 0){ gameOver = true; }


            Player.gameOver(gameOver);


            if (up) { player.Interface.Position.Y -= player.Speed; }
            if (down) { player.Interface.Position.Y += player.Speed; }
            if (left) { player.Interface.Position.X -= player.Speed;  }
            if (right) { player.Interface.Position.X += player.Speed; }
            for (int x = 0; x < Ammos.Count; x++)
            {
                if (Ammos[x].Shoot)
                {
                    // for (int i = x >= 1 ? x:0; i < Ammos.Count; i++)
                    // {
                    if ((int)Ammos[x].CurrentAmmo.Position.Y > -35)
                    {
                        Ammos[x].CurrentAmmo.Position.Y -= 8f;
                    }
                    else
                    {
                        Ammos[x].Shoot = false;
                        Ammos[x].CurrentAmmo.DestroySelf();
                    }
                   // }
                }
            }
            
            if(upgradeIsComplete)
            {
                try
                {
                    for (int x = 0; x < Ammos.Count; x++)
                    {
                        Ammo.DestroySelf(Ammos[x]);
                    }

                   for (int i = 0; i < (player.Level == 1 ?player.Level+1 :player.Level); i++)
                    {
                       
                        Ammos.Add(new Ammo { CurrentAmmo = new Sprite2D(new Vector(player.Interface.Position.X, player.Interface.Position.Y), new Vector(20, 40), $"level{player.Level}_ammo", "Ammo"), Shoot = false });
                    }
                }
                catch (Exception e)
                {
                    Log.Warning(e.Message);
                }
                finally
                {
                    upgradeIsComplete = false;
                }
            }


            Sprite2D playerCol = player.Interface.IsColliding("Enemy");
            if (playerCol != null)
            {
                enemyBlowingUpSound.settings.volume = 10;
                enemyBlowingUpSound.controls.play();
                playerCol.DestroySelf();
                if (!gameOver)
                {
                    playerLives[player.Life - 1].DestroySelf();
                }
                player.Life--;
                //Enemies.Remove(Enemies[Enemy.CollisonIndex]);
                player.Interface.Position.X = playerLastPos.X;
                player.Interface.Position.Y = playerLastPos.Y;
                Log.Info("PLAYER COL");
                player.Points-=player.Level;
                Enemy.Enemies.Remove(Enemy.Enemies[Enemy.CollisonIndex]);
            }


            for (int i = 0; i < Ammos.Count; i++)
            {
                Sprite2D isCol = Ammos[i].CurrentAmmo.IsColliding("Enemy");
                if (isCol != null)
                {
                    Console.WriteLine("Index: {0} Level: {1}",Enemy.CollisonIndex,Enemy.Enemies[Enemy.CollisonIndex].Level);
                    if (Enemy.Enemies[Enemy.CollisonIndex].Level > 1)
                    {
                        enemyBlowingUpSound.settings.volume = 10;
                        enemyBlowingUpSound.controls.play();

                        player.Points += Enemy.Enemies[Enemy.CollisonIndex].EnemyCurrency;
                        Enemy.Enemies[Enemy.CollisonIndex].Life--;

                        if (Enemy.Enemies[Enemy.CollisonIndex].Life == 0)
                        {
                            isCol.DestroySelf();
                            Enemy.Enemies.Remove(Enemy.Enemies[Enemy.CollisonIndex]);
                        }

                        Ammos[i].CurrentAmmo.Position.Y = -1500;
                        Ammos[i].Shoot = false;

                        return;
                    }
                    else
                    {
                        enemyBlowingUpSound.settings.volume = 10;
                        enemyBlowingUpSound.controls.play();

                        isCol.DestroySelf();
                        //Enemy.Enemies[i].enemy.DestroySelf();
                        //Enemies.Remove(Enemies[Enemy.CollisonIndex]);
                        //Ammos[i].CurrentAmmo.DestroySelf();
                        

                        player.Points += Enemy.Enemies[Enemy.CollisonIndex].EnemyCurrency;

                        Ammos[i].CurrentAmmo.Position.Y = -1500;
                        Ammos[i].Shoot = false;

                        Enemy.Enemies.Remove(Enemy.Enemies[Enemy.CollisonIndex]);

                        return;
                    }
                }
                
            }
            



            for (int i = 0; i < testPresents.Count; i++)
            {
                bool van = testPresents[i].IsColliding(player);
                if (van)
                {
                    presentSound.settings.volume = 10;
                    presentSound.controls.play();
                    testPresent.DestroySelf(testPresents[i]);

                    //testPresent

                    player.Level++;
                    upgradeIsComplete = true;
                    givePresent = false;
                    testPresents[player.Level].Go = false;
                }
                else
                {
                    playerLastPos.X = player.Interface.Position.X;
                    playerLastPos.Y = player.Interface.Position.Y;
                }
            }


            for (int i = 0; i < Enemy.Enemies.Count; i++)
            {
                if (Enemy.Enemies[i].enemy.Position.Y >= Engine.ScreenSize.Y)
                {
                    Enemy.Enemies[i].enemy.Position.X = new Random().Next(0, 800);
                    Enemy.Enemies[i].enemy.Position.Y = new Random().Next(-1000, -30);
                }
                Enemy.Enemies[i].enemy.Position.Y += Enemy.Enemies[i].Speed;

            }
            for (int i = 0; i < Planets.Count; i++)
            {
                Planets[i].Position.Y += .5f;
            }

            for (int i = 0; i < testPresents.Count; i++)
            {
                if (ammoUpgradeAllowed && testPresents[i].Allowed)
                {
                    if (completedMaps < 5 && testPresents[i].Level == 1)
                    {
                        Console.WriteLine("inner");
                        Random r = new Random();
                        givePresent = true;
                        testPresents[i].Go = true;
                        int random = r.Next(0, Enemy.Enemies.Count);
                        bool stop = false;
                        while (stop != true)
                            stop = Enemy.Enemies[random].IsAlive();

                        testPresents[i].Position.X = Enemy.Enemies[random].enemy.Position.X;
                        testPresents[i].Position.Y = Enemy.Enemies[random].enemy.Position.Y;

                        ammoUpgradeAllowed = false;

                        testPresents[i].Allowed = false;
                    }
                    else if (completedMaps == 3 && testPresents[i].Level ==2)
                    {
                        Random r = new Random();
                        givePresent = true;
                        testPresents[i].Go = true;
                        int random = r.Next(0, Enemy.Enemies.Count);
                        bool stop = false;
                        while (stop != true)
                            stop = Enemy.Enemies[random].IsAlive();

                        testPresents[i].Position.X = Enemy.Enemies[random].enemy.Position.X;
                        testPresents[i].Position.Y = Enemy.Enemies[random].enemy.Position.Y;

                        ammoUpgradeAllowed = false;

                        testPresents[i].Allowed = false;
                    }
                    else if (completedMaps == 5 && testPresents[i].Level == 3)
                    {
                        Random r = new Random();
                        givePresent = true;
                        testPresents[i].Go = true;
                        int random = r.Next(0, Enemy.Enemies.Count);
                        bool stop = false;
                        while (stop != true)
                            stop = Enemy.Enemies[random].IsAlive();

                        testPresents[i].Position.X = Enemy.Enemies[random].enemy.Position.X;
                        testPresents[i].Position.Y = Enemy.Enemies[random].enemy.Position.Y;

                        ammoUpgradeAllowed = false;

                        testPresents[i].Allowed = false;
                    }
                }
                
                if (lifeUpgradeAllowed && testPresents[i].Allowed && testPresents[i].Tag=="Life")
                {
                    Random r = new Random();
                    givePresent = true;

                    Present.Presents[i].Go = true;
                    int random = r.Next(0, Enemy.Enemies.Count);

                    bool stop = false;
                    while (stop != true)
                        stop = Enemy.Enemies[random].IsAlive();

                    testPresents[i].Position.X = Enemy.Enemies[random].enemy.Position.X;
                    testPresents[i].Position.Y = Enemy.Enemies[random].enemy.Position.Y;

                    lifeUpgradeAllowed = false;

                    testPresents[i].Allowed = false;
                }

            }
            for (int i = 0; i < testPresents.Count; i++)
            {
                if (givePresent && testPresents[i].Go)
                {
                    if (testPresents[i].Position.Y > Engine.ScreenSize.Y)
                    {
                        testPresents[i].Position.Y -= Engine.ScreenSize.Y;
                    }
                    testPresents[i].Position.Y += .8f;
                }
            }
        }


        //CSINÁLD MEG A RENDES ENEMY LISTÁT
        private void Win()
        {
            completedMaps++;

            int defaultNumber = 20;

            int minusEnemy_hard = 2;

            int minusEnemy_medium = 3;

            int minusEnemy_easy = 5;

            Random r = new Random();

            if (player.Level == 1)
            {
                Enemy.EnemiesCount += defaultNumber - (minusEnemy_easy * completedMaps);
            }
            else if (player.Level == 2)
            {
                Enemy.EnemiesCount += defaultNumber - (minusEnemy_medium * completedMaps);
            }

            else if (player.Level == 3)
            {
                Enemy.EnemiesCount += defaultNumber - (minusEnemy_hard * completedMaps);
            }


            //ide egy deciser ami eldönteni az enemik szintjeit
            DecideEnemyLevel();


            /* Enemies.Add(new Sprite2D(new Vector(r.Next(0, 900), r.Next(-1000, -30))
                 , new Vector(60, 60), "E2", "Enemy", 1, 1));*/


            currentWin = false;

            Player.ShootedEnemies = 0;

        }

        private void DecideEnemyLevel()
        {
            int all = Enemy.EnemiesCount;
            int lvl1 = 0;
            int lvl2 = 0;
            int lvl3 = 0;
            int boss = 0;

            Random r = new Random();


            /*
            * LVL0
            * */


            if (player.Level == 0)
            {
                lvl1 = all;
                for (int i = 0; i < all; i++)
                {
                    Enemy.Enemies.Add(new Enemy
                    {
                        Ammo = null,
                        enemy = new Sprite2D(new Vector(r.Next(0, 900), r.Next(-1000, -30))
                         , new Vector(60, 60), "E2", "Enemy"),
                        EnemyCurrency = 1,
                        Speed = 1.2f,
                        Level = 1,
                        Life = 1
                    });
                }
            }


            /*
            * LVL1
            * */


            else if (player.Level == 1)
            {
                lvl1 = all / 2 + all / 4;
                all = all - lvl1;
                lvl2 = all;

                for (int i = 0; i < lvl1; i++)
                {
                    Enemy.Enemies.Add(new Enemy
                    {
                        Ammo = null,
                        enemy = new Sprite2D(new Vector(r.Next(0, 900), r.Next(-1000, -30))
                         , new Vector(60, 60), "E2", "Enemy"),
                        EnemyCurrency = 1,
                        Speed = 1.2f,
                        Level = 1,
                        Life = 1
                    });
                }

                for (int i = 0; i < lvl2; i++)
                {
                    Enemy.Enemies.Add(new Enemy
                    {
                        Ammo = null,
                        enemy = new Sprite2D(new Vector(r.Next(0, 900), r.Next(-1000, -30))
                         , new Vector(60, 60), "E1", "Enemy"),
                        EnemyCurrency = 1,
                        Speed = .8f,
                        Level =2,
                        Life = 2
                    });
                    for (int x = 0; x < Enemy.Enemies.Count; x++)
                    {
                        if(Enemy.Enemies[x].Level > 1)
                        {
                            Console.WriteLine("Index: {0} Level: {1}",x,Enemy.Enemies[x].Level);
                        }
                    }
                }

            }
            
            /*
             * LVL2
             * */

            else if (player.Level == 2)
            {
                lvl1 = all / 2 + all / 4;
                all = all - lvl1;
                lvl2 = all;

                for (int i = 0; i < lvl1; i++)
                {
                    Enemy.Enemies.Add(new Enemy
                    {
                        Ammo = null,
                        enemy = new Sprite2D(new Vector(r.Next(0, 900), r.Next(-1000, -30))
                         , new Vector(60, 60), "E2", "Enemy"),
                        EnemyCurrency = 1,
                        Speed = 1.2f,
                        Level = 1,
                        Life = 1
                    });
                }

                for (int i = 0; i < lvl2; i++)
                {
                    Enemy.Enemies.Add(new Enemy
                    {
                        Ammo = null,
                        enemy = new Sprite2D(new Vector(r.Next(0, 900), r.Next(-1000, -30))
                         , new Vector(60, 60), "E1", "Enemy"),
                        EnemyCurrency = 1,
                        Speed = .8f,
                        Level = 2,
                        Life = 2
                    });
                }

            }


            /*
            * LVL3
            * */


            else if (player.Level == 3)
            {
                lvl1 = 0;
                lvl2 = all/2+ all/4;
                all = all - lvl2;
                lvl3 = all;

                for (int i = 0; i < lvl2; i++)
                {
                    Enemy.Enemies.Add(new Enemy
                    {
                        Ammo = null,
                        enemy = new Sprite2D(new Vector(r.Next(0, 900), r.Next(-1000, -30))
                         , new Vector(60, 60), "E1", "Enemy"),
                        EnemyCurrency = 1,
                        Speed = .8f,
                        Level = 2,
                        Life = 2
                    });
                }

                for (int i = 0; i < lvl3; i++)
                {
                    Enemy.Enemies.Add(new Enemy
                    {
                        Ammo = null,
                        enemy = new Sprite2D(new Vector(r.Next(0, 900), r.Next(-1000, -30))
                         , new Vector(60, 60), "E3", "Enemy"),
                        EnemyCurrency = 2,
                        Speed = 1f,
                        Level = 3,
                        Life = 3
                    });
                }

            }

            
        }

        #region GAMEOVER

        private void GameOver_Click(object sender, EventArgs e)
        {
            Engine.Window.Close();
        }

        #endregion

        private void ReleaseUpgrades()
        {
            if (!givePresent)
            {
                if (Player.ShootedEnemies > Enemy.Enemies.Count / 2)
                {
                    ammoUpgradeAllowed = true;
                }
                else if (player.Life < 2 && Player.ShootedEnemies > Enemy.Enemies.Count / 2)
                {
                    lifeUpgradeAllowed = true;
                }
            }
        }
    }
}
