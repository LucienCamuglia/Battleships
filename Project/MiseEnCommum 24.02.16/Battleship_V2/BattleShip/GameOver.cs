using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    class GameOver
    {
        #region Fields
        private List<Cendre> _cendres;
        private Random _rnd;
        private Form _view;
        private Stopwatch _watch;
        #endregion

        #region Properties
        public Stopwatch Watch
        {
            get { return _watch; }
            set { _watch = value; }
        }

        public Form View
        {
            get { return _view; }
            set { _view = value; }
        }

        public Random Rnd
        {
            get { return _rnd; }
            set { _rnd = value; }
        }

        public List<Cendre> Cendres
        {
            get { return _cendres; }
            set { _cendres = value; }
        }
        #endregion

        #region Constructor
        public GameOver(Form view)
        {
            this.View = view;
            this.Rnd = new Random();
            this.Cendres = new List<Cendre>();

            this.Watch = new Stopwatch();
            this.Watch.Start();
        }
        #endregion

        #region Methods
        public void CreateCendre()
        {
            if (this.Watch.ElapsedMilliseconds % 1000 == 0)
            {
                Cendre c = new Cendre(this.Rnd.Next(0, this.View.Right - this.View.Left), 0, this.Rnd.Next(0, this.View.Right - this.View.Left), this.View.Height - 40, 5000 + this.Rnd.Next(5000));
                this.Cendres.Add(c);
            }
        }

        public void Draw(PaintEventArgs pe)
        {
            this.CreateCendre();

            foreach (Cendre c in this.Cendres)
            {
                c.OnPaint(pe);
            }
        }
        #endregion
    }
}
