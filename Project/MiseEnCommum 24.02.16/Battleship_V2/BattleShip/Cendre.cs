using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace BattleShip
{
    class Cendre
    {
        int speed = 10000; // Milliseconds

        Stopwatch _watch = null;

        int _from_x = 0;
        int _from_y = 0;
        int _to_x = 0;
        int _to_y = 0;
        Point Location;
        Image img;
        Size sz;

        public Cendre(int x, int y, int to_x, int to_y, int tm)
        {
            this._from_x = x;
            this._from_y = y;
            this.speed = tm;


            this._to_x = to_x;
            this._to_y = to_y;

            this.Location = new System.Drawing.Point(this._from_x, this._from_y);

            this._watch = new Stopwatch();
            this._watch.Start();

            /*image cendre*/
            Random rnd = new Random();
            switch (rnd.Next(5))
            {
                case 0:
                    this.img = Properties.Resources.Cendre3;
                    break;
                case 1:
                    this.img = Properties.Resources.Cendre;
                    break;
                case 2:
                    this.img = Properties.Resources.Cendre2;
                    break;
                case 3:
                    this.img = Properties.Resources.Cendre2Rouge;
                    break;
                case 4:
                    this.img = Properties.Resources.Cendre2Jaune;
                    break;

            }

            this.sz = new System.Drawing.Size(Properties.Resources.Cendre.Width, Properties.Resources.Cendre.Height);
        }

        public void OnPaint(PaintEventArgs ep)
        {
            if (this._watch.ElapsedMilliseconds <= this.speed)
            {
                // Calcul du X en fonction du temps
                double factor = Convert.ToDouble(this._watch.ElapsedMilliseconds) / Convert.ToDouble(this.speed);

                int x = this._from_x + Convert.ToInt32((this._to_x - this._from_x) * factor);

                // Même calcul pour le Y
                int y = this._from_y + Convert.ToInt32(Math.Abs(this._to_y - this._from_y) * factor);

                // Déplacement du cendre
                this.Location = new System.Drawing.Point(x, y);
            }
            ep.Graphics.DrawImage(this.img, this.Location);
        }
    }
}
