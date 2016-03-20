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
        protected int _x; //x for forme
        protected int _y; //y for forme
        protected int _hauteur; //hauteur for form
        protected int _largeur; //largeur for form
        protected double _orientation; //Orientation for forme
        protected Couleur _color; //Color for form
        protected Graphics graphic;
        protected SolidBrush brush;
        protected pongForm.Form1 _context; //Context for forme

        public Forme(pongForm.Form1 context, int r,int g, int b,int x, int y, int hauteur, int largeur, double orientation) //Constructor
        {
            //Initialize attributes
            _color = new Couleur(r, g, b);
            _x = x;
            _y = y;
            _hauteur = hauteur;
            _largeur = largeur;
            _orientation = orientation;
            brush = new SolidBrush(_color.getColor());
            _context = context;
            graphic = _context.CreateGraphics();
        }

        public abstract void dessine(); //Dessine is abstract and need to be reimplemented in child class
    }
}
