using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Ship
    {
        #region Constants
        const int YPOSINIT = 13;
        #endregion

        #region Fields
        private int _x;
        private int _y;
        private int _size;
        private int widthCase;
        private int heightCase;
        private int nShip;
        private Color shipColor = Color.Black;
        private bool dragging = false;
        List<casePlatform> shipSize = new List<casePlatform>();

        /* Contain the differents boat or mines */
        private int[] ships = { 0, 1, 2, 3, 4 };
        #endregion

        #region Properties
        public int HeightCase
        {
            get { return heightCase; }
            set { heightCase = value; }
        }
        public int WidthCase
        {
            get { return widthCase; }
            set { widthCase = value; }
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public bool Draggering
        {
            get { return dragging; }
            set { dragging = value; }
        }
        #endregion

        #region Constructor
        public Ship(int boat, int size) : this(boat, size, new Point(0,0))
        {

        }

        public Ship(int boat, int size, Point Location)
        {
            this.nShip = boat;
            this._size = size;

            /*-----------------------------------------*
             *| NUMBERS | ITEMS             |   SIZE  |*
             *-----------------------------------------*
             *|    0    | MINE              |   1*1   |*
             *|    1    | SUBMARINE         |   1*3   |*
             *|    2    | DESTROYER         |   1*4   |*
             *|    3    | HELICOPTER-CARIER |   2*2   |*
             *|    4    | AIRCRAFT-CARIER   |   2*3   |*
             *-----------------------------------------*/
            switch (this.ships[this.nShip])
            {
                // mine
                case 0:
                    this.X = Location.X * 2;
                    this.Y = Location.Y + this._size * YPOSINIT;
                    this.WidthCase = 1;
                    this.HeightCase = 1;
                    this.shipColor = Color.DarkSeaGreen;
                    break;
                // submarine
                case 1:
                    this.X = this._size * 4;
                    this.Y = this._size * YPOSINIT;
                    this.WidthCase = 1;
                    this.HeightCase = 3;
                    this.shipColor = Color.Aquamarine;
                    break;
                // destroyer
                case 2:
                    this.X = this._size * 6;
                    this.Y = this._size * YPOSINIT;
                    this.WidthCase = 1;
                    this.HeightCase = 4;
                    this.shipColor = Color.Gray;
                    break;
                // helicopter-carier
                case 3:
                    this.X = this._size * 8;
                    this.Y = this._size * YPOSINIT;
                    this.WidthCase = 2;
                    this.HeightCase = 2;
                    this.shipColor = Color.Yellow;
                    break;
                // aircraft-carier
                case 4:
                    this.X = this._size * 11;
                    this.Y = this._size * YPOSINIT;
                    this.WidthCase = 2;
                    this.HeightCase = 3;
                    this.shipColor = Color.Red;
                    break;
            }

            // create the boat
            for (int i = 0; i < this.WidthCase; i++)
            {
                for (int j = 0; j < this.HeightCase; j++)
                {
                    this.shipSize.Add(new casePlatform(this.X + (this._size * i), this.Y + (this._size * j), this._size, this.shipColor));
                }
            }
        }
        #endregion

        #region Methods
        
        /// <summary>
        /// draw a boat
        /// </summary>
        /// <param name="g"></param>
        public void drawShip(Graphics g)
        {
            // width
            int i = 0;
            // height
            int j = 0;

            foreach (casePlatform cases in shipSize)
            {
                // affect a new coordinate to cases
                cases.X = this.X + (i * this._size);
                cases.Y = this.Y + (j * this._size);

                j++;

                if (i < this.WidthCase && j >= this.HeightCase)
                {
                    i++;
                    j = 0;
                }

                cases.DisplayCase(g);
            }
        }

        /// <summary>
        /// Check the postion of the boat and the cursor of the mouse
        /// </summary>
        /// <param name="mouseX">position x of the mouse cursor</param>
        /// <param name="mouseY">position y of the mouse cursor</param>
        /// <returns></returns>
        public bool isBetweenCase(int mouseX, int mouseY)
        {
            if ((mouseX > this.X) && (mouseX < (this.X + this._size * this.WidthCase)) &&
               (mouseY > this.Y) && (mouseY < (this.Y + this._size * this.HeightCase)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// rotate the boat (inverse the height and the width of the boat)
        /// </summary>
        public void rotateBoat()
        {
            int swap = 0;
            swap = this.HeightCase;
            this.HeightCase = this.WidthCase;
            this.WidthCase = swap;
        }

        /// <summary>
        /// check if the boats are in the ally platform and fit him to case
        /// </summary>
        /// <param name="mouseX">position x of the mouse cursor</param>
        /// <param name="mouseY">position y of the mouse cursor</param>
        public void IsInThePlatform(int mouseX, int mouseY, GameBoard platform)
        {
            /*int divX = 0, divY = 0;

            divX = mouseX / platform.CASE_SIZE + platform.Location.X;
            divY = mouseY / platform.CASE_SIZE + platform.Location.Y;

            if ((divX >= platform.Location.X) && (divX < platform.Location.X) && (divY >= 1) && (divY < 11))
            {
                this.X = divX * this._size + platform.Location.X;
                this.Y = divY * this._size + platform.Location.Y;
            }*/

            if ((mouseX > platform.Location.X) && (mouseX < platform.Location.X + platform.Size.Width) && (mouseY > platform.Location.Y) && (mouseY < platform.Location.Y + platform.Size.Height))
            {
                // calcul and trunc the position of the mouse on the platform 
                this.X = platform.Location.X + platform.CASE_SIZE + (mouseX - platform.Location.X - platform.CASE_SIZE) / platform.CASE_SIZE * platform.CASE_SIZE;
                this.Y = platform.Location.Y + platform.CASE_SIZE + (mouseY - platform.Location.Y - platform.CASE_SIZE) / platform.CASE_SIZE * platform.CASE_SIZE;
            }
        }

        public casePlatform[] GetCaseShip()
        {
            return this.shipSize.ToArray();
        }
    }
}
