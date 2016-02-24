using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public partial class Form1 : Form
    {
        #region Constants
        const int SIZE = 40;
        const int XFORM = 31;
        const int YFORM = 19;
        const int NITEMBOAT = 7;
        const int NMINES = 3;
        const int LINES = 10;
        const int COLUMNS = 11;
        const int SPACEPLATFORM = 15;
        const int LABELWIDTH = 100;
        const string PATH = "";
        const string PSEUDO = "Bob";
        #endregion

        #region Fields
        // platform of the two players
        GameBoard platAlly, platEnemy;
        List<Ship> ships = new List<Ship>();
        int size = SIZE;
        int nShip;
        Random rnd = new Random();
        List<Cendre> cendres = new List<Cendre>();
        bool gameInProgress = true;
        Button button;
        bool GameStarting;
        GameOver gameover;
        FileManager fm;
        #endregion
        
        #region Constructor
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// create the different gameboard with the label associate, 
        /// add the game button
        /// add the boats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            //this.fm = new FileManager(PATH, "Bob");
            // fix the size of the form compared to the size of one case 
            this.Size = new System.Drawing.Size(XFORM * size, YFORM * size);
            this.GameStarting = false;

            // creation of new gameboard for the battleship
            platAlly = new GameBoard("Your field", 10, 10);
            platEnemy = new GameBoard("Enemy field", this.platAlly.Size.Width + 150, this.platAlly.Location.Y);

            // add button
            gameButton();

            // boats
            for (int i = 0; i < NITEMBOAT; i++)
            {
                // 3 mines
                if (i < NMINES)
                {
                    nShip = 0;
                }
                else
                {
                    nShip = i - 2;
                }
                ships.Add(new Ship(nShip, size, new Point(size, (size + 10) * i)));
            }
            gameInProgress = true;
            //GameFinish("Victoire !", Color.Green);

            //fm = new FileManager(PATH, PSEUDO);

        }

        /// <summary>
        /// call the methods paint each 100ms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmr_Tick(object sender, EventArgs e)
        {
            /*if (!gameInProgress)
                this.CreateCendre();*/

            if (!gameInProgress && this.gameover == null)
                this.gameover = new GameOver(this);

            Invalidate();
        }

        /// <summary>
        /// Display each platform + ship
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            platAlly.Display(e);
            platEnemy.Display(e);

           
            foreach (Ship ship in ships)
            {
                ship.drawShip(e.Graphics);
            }

            /*if (!gameInProgress)
            {
                foreach (Cendre c in cendres)
                {
                    c.OnPaint(e);
                }
            }*/

            if (gameover != null)
                gameover.Draw(e);
        }

        /// <summary>
        /// get the values of the cursor
        /// movement of the ship
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            platEnemy.OverCase(e.X, e.Y);

            // moving
            foreach (Ship ship in ships)
            {
                if (ship.Draggering)
                {
                    // get the middle of the case
                    ship.X = e.X - size / 2;
                    ship.Y = e.Y - size / 2;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// test which mouse clic is used
        /// left clic :
        /// In enemy platform put a cross if the clic touch a case
        /// Give the autorisation to a ship to move
        /// Right clic :
        /// rotate the boat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    platEnemy.TouchedCase(e.X, e.Y);
                    if (!this.GameStarting)
                    {
                        foreach (Ship ship in ships)
                        {
                            if (ship.isBetweenCase(e.X, e.Y))
                            {
                                ship.Draggering = true;
                            }
                        }
                    }

                    break;

                case MouseButtons.Right:
                    foreach (Ship ship in ships)
                    {
                        if (ship.isBetweenCase(e.X, e.Y))
                            ship.rotateBoat();
                    }
                    break;
            }

        }

        /// <summary>
        /// stop the movement of a ship
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            // check if the game has started
            if (!GameStarting)
            {
                foreach (Ship ship in ships)
                {
                    if (ship.Draggering)
                    {
                        ship.IsInThePlatform(e.X, e.Y, platAlly);
                        ship.Draggering = false;
                    }
                }

            }

        }

        private void gameButton()
        {
            button = new Button();
            button.Text = "begin";
            button.Height = this.size;
            button.Width = LABELWIDTH;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Font = new Font("Arial", 12);
            button.BackColor = Color.Transparent;
            button.Location = new Point(this.platAlly.Location.X + this.platAlly.Size.Width + 20, this.platAlly.Location.Y + LABELWIDTH);
            button.Click += button_Click;
            this.Controls.Add(button);

            Button btnRestart = new Button();
            btnRestart.Text = "RESTART";
            btnRestart.Height = this.size;
            btnRestart.Width = this.size * 3;
            btnRestart.Font = new Font("Arial", 12);
            btnRestart.Location = new Point(size * 27, this.Width / 2);
            btnRestart.SendToBack();
            this.Controls.Add(btnRestart);
            btnRestart.Click += btnRestart_Click;

            button = new Button();
            button.Text = "Join game";
            button.Height = this.size;
            button.Width = LABELWIDTH;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Font = new Font("Arial", 12);
            button.BackColor = Color.Transparent;
            button.Location = new Point(this.platAlly.Location.X + this.platAlly.Size.Width + 20, this.platAlly.Location.Y + LABELWIDTH + 50);
            button.Click += button_Click;
            this.Controls.Add(button);

            button = new Button();
            button.Text = "Create game";
            button.Height = this.size;
            button.Width = LABELWIDTH;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Font = new Font("Arial", 12);
            button.BackColor = Color.Transparent;
            button.Location = new Point(this.platAlly.Location.X + this.platAlly.Size.Width + 20, this.platAlly.Location.Y + LABELWIDTH + 100);
            button.Click += create_game;     
            this.Controls.Add(button);
        }

        void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        void create_game(object sender, EventArgs e)
        {
            FolderBrowserDialog fldBd = new FolderBrowserDialog();
            if (fldBd.ShowDialog() == DialogResult.OK)
            {
                fm = new FileManager(fldBd.SelectedPath, PSEUDO);
                this.button_Click(sender, e);
            }
        }

        /// <summary>
        /// begin the game 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void button_Click(object sender, EventArgs e)
        {
            button.Text = "Ready";
            button.Enabled = false;
            this.GameStarting = true;

            
            foreach (Ship s in this.ships)
            {
                this.platAlly.AddShip(s.GetCaseShip());        
            }

           this.ships.Clear();
            int[,] plat = this.platAlly.ToArrayGameboard();

            for (int i = 0; i < 10; ++i)
                for (int j = 0; j < 10; ++j)
                {
                    fm.writeBoard("PLATEAU-", "P1", i, j, plat[i,j]);
                }
        }

        private void CreateCendre()
        {
            if (!gameInProgress)
            {
                Cendre c = new Cendre(rnd.Next(0, this.Right - this.Left), 0, rnd.Next(0, this.Right - this.Left), this.Height - 40, 5000 + rnd.Next(5000));
                cendres.Add(c);
            }
        }

        /// <summary>
        /// Permet d'afficher en gros le texte "Défaite" ou "Victoire"
        /// </summary>
        /// <param name="text">Text à afficher</param>
        /// <param name="couleur">Couleur du text</param>
        private void GameFinish(string text, Color couleur)
        {
            Label StatusGame = new Label();
            StatusGame.Name = "GameFinish";
            StatusGame.Text = text;
            StatusGame.AutoSize = true;
            StatusGame.TextAlign = ContentAlignment.MiddleCenter;
            StatusGame.Font = new Font("Arial", 50);
            StatusGame.BackColor = Color.Transparent;
            StatusGame.ForeColor = couleur;
            StatusGame.Location = new Point(this.Height / 2, this.Width / 2);
            StatusGame.BringToFront();
            this.Controls.Add(StatusGame);
        }
        #endregion
    }
}
