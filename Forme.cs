using System.Drawing;

namespace Pong
{
    public abstract class Forme
    {
        public int _x { get; set; } //x for forme
        public int _y { get; set; } //y for forme
        public int _hauteur { get; set; }//hauteur for form
        public int _largeur; //largeur for form
        public double _orientation; //Orientation for forme
        public Couleur _color; //Color for form
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

        public abstract void draw(Graphics e); //Dessine is abstract and need to be reimplemented in child class

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
