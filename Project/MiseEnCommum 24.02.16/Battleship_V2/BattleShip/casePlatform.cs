using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class casePlatform
    {
        #region Fields
        private Rectangle _rect;
        private bool _cross;
        private Pen p;
        private Color _color;
        private Color _crossColor;
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the cross color
        /// </summary>
        public Color CrossColor
        {
            get { return _crossColor; }
            set { _crossColor = value; }
        }

        /// <summary>
        /// Get or set the rectangle
        /// </summary>
        public Rectangle Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }

        /// <summary>
        /// Get or set the X location
        /// </summary>
        public int X
        {
            get { return this.Rect.Location.X; }
            set { ChangeRectLocation(value, this.Rect.Location.Y); }
        }

        /// <summary>
        /// Get or set the Y location
        /// </summary>
        public int Y
        {
            get { return this.Rect.Location.Y; }
            set { ChangeRectLocation(this.Rect.Location.X, value); }
        }

        /// <summary>
        /// Get or set the size
        /// </summary>
        public int Size
        {
            get { return this.Rect.Size.Width; }
            set { this.ChangeRectSize(value); }
        }

        /// <summary>
        /// Get or set if the cross must be deployed
        /// </summary>
        public bool Cross
        {
            get { return _cross; }
            set { _cross = value; }
        }

        /// <summary>
        /// Get or set the color case
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        #endregion

        #region Const
        private const int LOCATION_VARIATION = 4;
        #endregion

        #region Constructor
        public casePlatform(int x, int y, int size, Color color)
        {
            this.Rect = new Rectangle(new Point(x, y), new Size(size, size));
            this.CrossColor = Color.Red;
            this.Color = color;
        }
        #endregion

        #region Methods

        /// <summary>
        /// create a case of a platform
        /// </summary>
        /// <param name="g"></param>
        public void DisplayCase(Graphics g)
        {
            SolidBrush brush = new SolidBrush(this.Color);
            this.p = new Pen(Color.Black, 1);
            g.FillRectangle(brush, this.Rect);
            g.DrawRectangle(p, this.Rect);
            if (this.Cross)
            {
                this.DisplayCross(g, this.X, this.Y);
            }
        }

        /// <summary>
        /// display a red cross inside a case
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x">coordinate x of the case</param>
        /// <param name="y">coordinate y of the case</param>
        public void DisplayCross(Graphics g, int x, int y)
        {
            this.p = new Pen(this.CrossColor, 3);

            Point point1 = new Point(x + this.Size / LOCATION_VARIATION, y + this.Size / LOCATION_VARIATION);
            Point point1End = new Point(x + this.Size / LOCATION_VARIATION * 3, y + this.Size / LOCATION_VARIATION * 3);
            Point point2 = new Point(x + this.Size / LOCATION_VARIATION * 3, y + this.Size / LOCATION_VARIATION);
            Point point2End = new Point(x + this.Size / LOCATION_VARIATION, y + this.Size / LOCATION_VARIATION * 3);

            g.DrawLine(p, point1, point1End);
            g.DrawLine(p, point2, point2End);
        }

        /// <summary>
        /// test if the cursor position is between a case
        /// </summary>
        /// <param name="mouseX">position x of the mouse cursor</param>
        /// <param name="mouseY">position y of the mouse cursor</param>
        /// <returns></returns>
        public bool IsBetweenCase(int mouseX, int mouseY)
        {
            if ((mouseX > this.X) && (mouseX < this.X + this.Size) && (mouseY > this.Y) && (mouseY < this.Y + this.Size))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// change the size of the rectangle if the case size size has change
        /// </summary>
        /// <param name="size"></param>
        private void ChangeRectSize(int size)
        {
            this.Rect = new Rectangle(this.Rect.Location, new Size(size, size));
        }

        /// <summary>
        /// change the rectangle location if the case size has change
        /// </summary>
        /// <param name="x">New X position</param>
        /// <param name="y">New Y position</param>
        private void ChangeRectLocation(int x, int y)
        {
            this.Rect = new Rectangle(new Point(x, y), this.Rect.Size);
        }

        /// <summary>
        /// check if the mouse is over the case and display it
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void OverCase(int x, int y)
        {
            if ((x > this.Rect.X) && (x < this.Rect.X + this.Rect.Width) && (y > this.Rect.Y) && (y < this.Rect.Y + this.Rect.Height))
            {
                this.Color = Color.CornflowerBlue;
            }
            else
            {
                this.Color = Color.White;
            }
        }
        #endregion
    }
}
