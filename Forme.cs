using System.Drawing;

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
        protected SolidBrush brush;

        public Forme(int r,int g, int b,int x, int y, int hauteur, int largeur, double orientation) //Constructor
        {
            //Initialize attributes
            _color = new Couleur(r, g, b);
            brush = new SolidBrush(_color.getColor());
            _x = x;
            _y = y;
            _hauteur = hauteur;
            _largeur = largeur;
            _orientation = orientation;
        }

        public abstract void dessine(Graphics e); //Dessine is abstract and need to be reimplemented in child class

        public int getX()
        {
            return _x;
        }

        public int getY()
        {
            return _y;
        }

        public int getWidth()
        {
            return _largeur;
        }

        public int getHeight()
        {
            return _hauteur;
        }

        public double getOrientation()
        {
            return _orientation;
        }

        public void setOrientation(double orientation)
        {
            _orientation = orientation;
        }
    }
}
