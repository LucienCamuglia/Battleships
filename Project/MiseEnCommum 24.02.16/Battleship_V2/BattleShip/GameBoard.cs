using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    class GameBoard
    {
        #region Fields
        private Size _size;
        private string _name;
        private List<casePlatform> _board;
        private List<Label> _coordinates;
        private Label _lblPlayer;
        private Point _location;
        private Font _labelFont;
        private Color _elipseColor;
        #endregion

        #region Constants
        private const int SIZE = 40;
        private const int LINES = 10;
        private const int COLUMNS = 10;
        private char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        private Color BACK_COLOR = Color.Transparent;
        #endregion

        #region Proriétés
        /// <summary>
        /// Get or set the elipse color
        /// </summary>
        public Color ElipseColor
        {
            get { return _elipseColor; }
            set { _elipseColor = value; }
        }

        /// <summary>
        /// Get the size of a case
        /// </summary>
        public int CASE_SIZE
        {
            get { return SIZE; }
        }

        /// <summary>
        /// Get or set the size
        /// </summary>
        public Size Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// Get or set the name of the player
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Get or set the label player
        /// </summary>
        public Label LblPlayer
        {
            get { return _lblPlayer; }
            set { _lblPlayer = value; }
        }

        /// <summary>
        /// Get or set the font for label
        /// </summary>
        public Font LabelFont
        {
            get { return _labelFont; }
            set { _labelFont = value; }
        }

        /// <summary>
        /// Get or set the coordinates
        /// </summary>
        public List<Label> Coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }

        /// <summary>
        /// Get or set case which constitute the grid
        /// </summary>
        private List<casePlatform> Board
        {
            get { return _board; }
            set { _board = value; }
        }

        /// <summary>
        /// Position x, y of the grid
        /// </summary>
        public Point Location
        {
            get { return _location; }
            set { _location = value; }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Create a new gameboard 10x10 (case)
        /// </summary>
        public GameBoard()
            : this("Field", 0, 0)
        {

        }

        /// <summary>
        /// Create a new gameboard 10x10 at the relative location
        /// </summary>
        /// <param name="posX">Position X</param>
        /// <param name="posY">Position Y</param>
        public GameBoard(string name, int posX, int posY)
        {
            this.Board = new List<casePlatform>();
            this.Coordinates = new List<Label>();
            this.Location = new Point(posX, posY);
            this.LabelFont = new Font("Arial", 12);
            this.Name = name;
            this.ElipseColor = Color.Red;
            this.InitialisationGrille();
            this.AddLabelPlayer();
            this.InitializeSize();
            
        }
        #endregion

        #region Methodes

        /// <summary>
        /// Calcul the size in pixel by the rapport in the number of case and the size
        /// </summary>
        private void InitializeSize()
        {
            this.Size = new Size(LINES * SIZE + SIZE, COLUMNS * SIZE + LblPlayer.Height + SIZE);
        }

        /// <summary>
        /// Initialize each case of the grid
        /// </summary>
        private void InitialisationGrille()
        {
            for (int i = 0; i < LINES; ++i)
            {
                this.AddCoordinates(i, SIZE * i + SIZE);
                for (int j = 0; j < COLUMNS; ++j)
                {
                    this.Board.Add(new casePlatform((this.Location.X + SIZE) + (SIZE * i), (this.Location.Y + SIZE) + (SIZE * j), SIZE, Color.White));
                }
            }
        }

        /// <summary>
        /// Drawing the board game
        /// </summary>
        /// <param name="pe">Element graphics</param>
        public void Display(PaintEventArgs pe)
        {
            foreach (casePlatform c in this.Board)
                c.DisplayCase(pe.Graphics);

            foreach (Label l in this.Coordinates)
                pe.Graphics.DrawString(l.Text, l.Font, new SolidBrush(Color.Red), new Point(l.Location.X + l.Size.Width / 2 - (int)(l.Font.Size / 2), l.Location.Y + l.Size.Height / 2 - (int)(l.Font.Size / 2)));

            pe.Graphics.DrawString(LblPlayer.Text, LblPlayer.Font, new SolidBrush(Color.Black), LblPlayer.Location);

            pe.Graphics.FillEllipse(new SolidBrush(this.ElipseColor), this.Location.X, this.Location.Y, 20, 20);
        }

        /// <summary>
        /// Verifie si la souris est sur uns case
        /// </summary>
        /// <param name="x">Position x de la souris</param>
        /// <param name="y">Position y de la souris</param>
        public void OverCase(int x, int y)
        {
            foreach (casePlatform c in this.Board)
                c.OverCase(x, y);
        }

        /// <summary>
        /// Verifie si un carré est touché
        /// </summary>
        /// <param name="x">Position x de la souris</param>
        /// <param name="y">Position y de la souris</param>
        public void TouchedCase(int x, int y)
        {
            foreach (casePlatform c in this.Board)
            {
                if (c.IsBetweenCase(x, y) && !c.Cross)
                {
                    c.Cross = true;
                }
            }
        }

        /// <summary>
        /// Add the ship on the grid
        /// </summary>
        /// <param name="shipCase"></param>
        public void AddShip(casePlatform[] shipCase)
        {
            foreach (casePlatform c in shipCase)
            {
                //this.Board[((int)(c.X/40)-1)*10+(int)(c.Y/40)-1] = c;
                this.Board.Remove(c);
                this.Board.Add(c);
            }
        }

        /// <summary>
        /// Creation of dynamic labels for coordonate
        /// </summary>
        /// <param name="coord">coordinate</param>
        /// <param name="pos">the position corresponding to the coordinate</param>
        private void AddCoordinates(int coord, int pos)
        {
            Label lblNumbers = new Label();
            lblNumbers.Text = (coord + 1).ToString();
            lblNumbers.Height = SIZE;
            lblNumbers.Width = SIZE;
            lblNumbers.TextAlign = ContentAlignment.MiddleCenter;
            lblNumbers.Font = this.LabelFont;
            lblNumbers.BackColor = BACK_COLOR;
            lblNumbers.Location = new Point(this.Location.X, pos + this.Location.Y);
            this.Coordinates.Add(lblNumbers);

            Label lblLetters = new Label();
            lblLetters.Text = letters[coord].ToString();
            lblLetters.Height = SIZE;
            lblLetters.Width = SIZE;
            lblLetters.TextAlign = ContentAlignment.MiddleCenter;
            lblLetters.Font = this.LabelFont;
            lblLetters.BackColor = BACK_COLOR;
            lblLetters.Location = new Point(pos + this.Location.X, this.Location.Y);
            this.Coordinates.Add(lblLetters);
        }

        /// <summary>
        /// Add the name of the gameboard
        /// </summary>
        private void AddLabelPlayer()
        {
            this.LblPlayer = new Label();
            this.LblPlayer.Text = this.Name;
            this.LblPlayer.AutoSize = true;
            this.LblPlayer.Font = this.LabelFont;
            this.LblPlayer.BackColor = BACK_COLOR;
            this.LblPlayer.Location = new Point(this.Location.X + (SIZE * 10) / 2 - this.LblPlayer.Width / 2 + SIZE, this.Location.Y - 5);
        }

        public int[,] ToArrayGameboard()
        {
            int[,] gameboard = new int[10,10];
            
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    gameboard[i, j] = (this.Board[i * 10 + j].Color != Color.White) ? 0 : 1;
                }
            }

            return gameboard;
        }
        #endregion
    }
}
