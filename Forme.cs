using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Pong
{
    public abstract class Forme
    {
        protected int _x;
        protected int _y;
        protected int _hauteur;
        protected int _largeur;
        protected double _orientation;
        protected Couleur _color;
        protected pongForm.Form1 _context;

        public Forme(pongForm.Form1 context, int r,int g, int b,int x, int y, int hauteur, int largeur, double orientation)
        {
            _color = new Couleur(r, g, b);
            _x = x;
            _y = y;
            _hauteur = hauteur;
            _largeur = largeur;
            _orientation = orientation;
            _context = context;
        }

        public abstract void dessine();
    }
}
