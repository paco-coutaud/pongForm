using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{

    public abstract class Mobile : Forme
    {
        protected int _vitesse;

        public Mobile(pongForm.Form1 context, int r, int g, int b, int x, int y, int hauteur, int largeur, double orientation, int vitesse) : base(context,r,g,b,x,y,hauteur,largeur,orientation)
        {
            _vitesse = vitesse;
        }

        public abstract override void dessine();
        public abstract void deplace();

        public abstract void clear();
    }

}
