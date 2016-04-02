/****************************
****AUTHOR : Paco COUTAUD****
****AUTHOR : Gauthier CASTRO*
**LAST CHANGES : 02/03/2016**
****************************/

using System.Drawing;

namespace Pong
{
    public abstract class Shape
    {
        public int _x { get; set; } //Member variable _x with accessor and mutator, respect the encapsulation
        public int _y { get; set; } //Member variable _y with accessor and mutator, respect the encapsulation
        public int _height { get; set; }//Member variable _width with accessor and mutator, respect the encapsulation
        public int _width{ get; set; } //Member variable _heught with accessor and mutator, respect the encapsulation
        public double _orientation { get; set; }
        public Couleur _color { get; set; }
        protected SolidBrush brush;

        public Shape(int r,int g, int b,int x, int y, int height, int width, double orientation) //Constructor
        {
            //Initialize attributes
            _color = new Couleur(r, g, b);
            brush = new SolidBrush(_color.getColor());
            _x = x;
            _y = y;
            _height = height;
            _width = width;
            _orientation = orientation;
        }

        public abstract void draw(Graphics e); //Draw is abstract and need to be reimplemented in child class
    }
}
