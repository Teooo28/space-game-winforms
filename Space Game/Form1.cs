using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Space_Game
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer gameMedia, shootMedia, explosion;

        PictureBox[] stars;
        int backgroundSpeed;

        PictureBox[] munitions;
        int munitionSpeed;

        PictureBox[] enemies;
        int enemySpeed;

        PictureBox[] enemiesMunitions;
        int enemiesMunitionSpeed;
        
        Random rnd;
        int playerSpeed;

        int score, level, dificulty;
        bool pause, gameIsOver;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pause = false;
            gameIsOver = false;
            score = 0;
            level = 1;
            dificulty = 9;
            
            backgroundSpeed = 2;
            playerSpeed = 4;
            munitionSpeed = 15;
            enemySpeed = 3;
            enemiesMunitionSpeed = 4;

     //enemies
            enemies = new PictureBox[10];

            Image enemy1 = Image.FromFile(@"asserts\E1.png");
            Image enemy2 = Image.FromFile(@"asserts\E2.png");
            Image enemy3 = Image.FromFile(@"asserts\E3.png");
            Image boss1 = Image.FromFile(@"asserts\Boss1.png");
            Image boss2 = Image.FromFile(@"asserts\Boss2.png");

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new PictureBox();
                enemies[i].Size = new Size(40, 40);
                enemies[i].SizeMode = PictureBoxSizeMode.Zoom;
                enemies[i].BorderStyle = BorderStyle.None;
                enemies[i].Visible = false;
                this.Controls.Add(enemies[i]);
                enemies[i].Location = new Point((i + 1) * 45, -50);
            }

            enemies[0].Image = boss1;
            enemies[1].Image = enemy1;
            enemies[2].Image = enemy2;
            enemies[3].Image = enemy3;
            enemies[4].Image = enemy1;
            enemies[5].Image = enemy3;
            enemies[6].Image = enemy2;
            enemies[7].Image = enemy3;
            enemies[8].Image = enemy1;
            enemies[9].Image = boss2;


     //stars
            stars = new PictureBox[15];
            rnd = new Random();

            for (int i=0; i < stars.Length; i++)
            {
                stars[i] = new PictureBox();
                stars[i].BorderStyle = BorderStyle.None;
                stars[i].Location = new Point(rnd.Next(20, 580), rnd.Next(-10, 400));
                if(i % 2 == 0)
                {
                    stars[i].Size = new Size(2, 2);
                    stars[i].BackColor = Color.Wheat;
                }
                else
                {
                    stars[i].Size = new Size(3, 3);
                    stars[i].BackColor = Color.DarkGray;
                }
                this.Controls.Add(stars[i]);
            }


     //munitions
            munitions = new PictureBox[3];
            Image munition = Image.FromFile(@"asserts\munition.png");

            for (int i=0; i < munitions.Length; i++)
            {
                munitions[i] = new PictureBox();
                munitions[i].Size = new Size(9, 9);
                munitions[i].Image = munition;
                munitions[i].SizeMode = PictureBoxSizeMode.Zoom;
                munitions[i].BorderStyle = BorderStyle.None;
                this.Controls.Add(munitions[i]);
            }


     //enemies_munitions
            enemiesMunitions = new PictureBox[10];
            for (int i=0; i < enemiesMunitions.Length; i++)
            {
                enemiesMunitions[i] = new PictureBox();
                enemiesMunitions[i].Size = new Size(2, 20);
                enemiesMunitions[i].Visible = false;
                enemiesMunitions[i].BackColor = Color.Yellow;
                int x = rnd.Next(0, 10);
                enemiesMunitions[i].Location = new Point(enemies[x].Location.X, enemies[x].Location.Y - 20);
                this.Controls.Add(enemiesMunitions[i]);
            }


     //add sound
            gameMedia = new WindowsMediaPlayer();
            shootMedia = new WindowsMediaPlayer();
            explosion = new WindowsMediaPlayer();

            gameMedia.URL = "songs\\GameSong.mp3";
            shootMedia.URL = "songs\\shoot.mp3";
            explosion.URL = "songs\\boom.mp3";

            gameMedia.settings.setMode("loop", true);

            gameMedia.settings.volume = 5;
            shootMedia.settings.volume = 1;
            explosion.settings.volume = 6;

            gameMedia.controls.play();

        }

     //star_move
        private void BackgroundTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].Top += backgroundSpeed;
                if (stars[i].Top >= this.Height)
                {
                    stars[i].Top = -stars[i].Height;
                }
            }
        }



     //player_move
        private void LeftMove_Tick(object sender, EventArgs e)
        {
            if (Player.Left > 10)
            {
                Player.Left -= playerSpeed;
            }
        }

        private void RightMove_Tick(object sender, EventArgs e)
        {
            if (Player.Right < 485)
            {
                Player.Left += playerSpeed;
            }
        }

        private void UpMove_Tick(object sender, EventArgs e)
        {
            if (Player.Top > 10)
            {
                Player.Top -= playerSpeed;
            }
        }

        private void DownMove_Tick(object sender, EventArgs e)
        {
            if (Player.Top < 390)
            {
                Player.Top += playerSpeed;
            }
        }



     //keys_move
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            LeftMove.Stop();
            RightMove.Stop();
            UpMove.Stop(); 
            DownMove.Stop();

            if (e.KeyCode == Keys.Space)
            {
                if (!gameIsOver)
                {
                    if (pause)
                    {
                        StartTimers();
                        label1.Visible = false;
                        gameMedia.controls.play();
                        pause = false;
                    }
                    else
                    {
                        label1.Location = new Point(this.Width / 2 - 160, 150);
                        label1.Text = "PAUSED";
                        label1.Visible = true;
                        gameMedia.controls.pause();
                        StopTimers();
                        pause = true;
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!pause)
            {
                if (e.KeyCode == Keys.Left)
                {
                    LeftMove.Start();
                }
                if (e.KeyCode == Keys.Right)
                {
                    RightMove.Start();
                }
                if (e.KeyCode == Keys.Up)
                {
                    UpMove.Start();
                }
                if (e.KeyCode == Keys.Down)
                {
                    DownMove.Start();
                }
            }
            
        }

     

     //munition_move
        private void MunitionMoveTimer_Tick(object sender, EventArgs e)
        {
            shootMedia.controls.play();

            for (int i=0; i < munitions.Length; i++)
            {
                if (munitions[i].Top > 0)
                {
                    munitions[i].Visible = true;
                    munitions[i].Top -= munitionSpeed;

                    Collision();
                }
                else
                {
                    munitions[i].Visible = false;
                    munitions[i].Location = new Point(Player.Location.X + 20, Player.Location.Y - i * 40);
                }
            }
        }



     //enemies_move
        private void MoveEnemiesTimer_Tick(object sender, EventArgs e)
        {
            MoveEnemies(enemies, enemySpeed);
        }

        private void MoveEnemies(PictureBox[] array, int speed)
        {
            for (int i = 0; i < array.Length; i++) 
            {
                array[i].Visible = true;
                array[i].Top += speed;
                if (array[i].Top > this.Height)
                {
                    array[i].Location = new Point((i + 1) * 45, -200);
                }
            }
        }



     //enemies_munition_move
        private void EnemiesMunitionTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < (enemiesMunitions.Length - dificulty); i++)
            {
                if (enemiesMunitions[i].Top < this.Height)
                {
                    enemiesMunitions[i].Visible = true;
                    enemiesMunitions[i].Top += enemiesMunitionSpeed;

                    CollisionWithEnemyMunitions();
                }
                else
                {
                    enemiesMunitions[i].Visible = false;
                    int x = rnd.Next(0, 10);
                    enemiesMunitions[i].Location = new Point(enemies[x].Location.X + 15, enemies[x].Location.Y + 20);
                }
            }
        }



     //collision (munitions with enemies or player with enemies)
        private void Collision()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (munitions[0].Bounds.IntersectsWith(enemies[i].Bounds) || 
                    munitions[1].Bounds.IntersectsWith(enemies[i].Bounds) ||
                    munitions[2].Bounds.IntersectsWith(enemies[i].Bounds)) 
                {
                    explosion.controls.play();

                    score += 1;
                    ScoreLabel.Text = (score < 10)? "Score: 0" + score.ToString() : "Score: " + score.ToString();

                    if (score == 20)
                    {
                        level += 1;
                        score = 0;
                        LevelLabel.Text = (level < 10)? "Level: 0" + level.ToString() : "Level: " + level.ToString();
                        
                        if (enemySpeed <= 10 && enemiesMunitionSpeed <= 10 && dificulty >= 0)
                        {
                            dificulty--;
                            enemySpeed++;
                            enemiesMunitionSpeed++;
                        }
                        
                        if (level == 10)
                        {
                            GameOver("YOU WON");
                        }
                    }

                    enemies[i].Location = new Point((i + 1) * 45, -100);
                }

                if (Player.Bounds.IntersectsWith(enemies[i].Bounds))
                {
                    explosion.settings.volume = 30;
                    explosion.controls.play();
                    Player.Visible = false;
                    GameOver("Game Over");
                }
            }
        }

       

     //collision with enemy munitions
        private void CollisionWithEnemyMunitions()
        {
            for (int i = 0; i < enemiesMunitions.Length; i++)
            {
                if (Player.Bounds.IntersectsWith(enemiesMunitions[i].Bounds))
                {
                    enemiesMunitions[i].Visible = false;
                    explosion.settings.volume = 30;
                    explosion.controls.play();
                    Player.Visible = false;
                    GameOver("Game Over");
                }
            }
        }



     //game_over
        private void GameOver(string str)
        {
            label1.Text = str;
            label1.Location = new Point(40, 60);
            label1.Visible = true;
            ReplayButton.Visible = true;
            ExitButton.Visible = true;
            
            gameMedia.controls.stop();
            StopTimers();
        }


     //replay and exit buttons
        private void ReplayButton_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponent();
            Form1_Load(e, e);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }



     //stop_timers
        private void StopTimers()
        {
            BackgroundTimer.Stop();
            MoveEnemiesTimer.Stop();
            MunitionMoveTimer.Stop();
            EnemiesMunitionTimer.Stop();
        }



     //start_timers
        private void StartTimers()
        {
            BackgroundTimer.Start();
            MoveEnemiesTimer.Start();
            MunitionMoveTimer.Start();
            EnemiesMunitionTimer.Start();
        }
    }
}
