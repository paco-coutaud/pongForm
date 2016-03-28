/****************************
****AUTHOR : Paco COUTAUD****
****AUTHOR : Gauthier CASTRO*
**LAST CHANGES : 28/03/2016**
****************************/

using System;
using System.Drawing;

namespace Pong
{   
    /*This class provides methods to create and manipulate Circles*/
    public class Circle : Mobile //Herite from Mobile
    {
        private Rectangle _rec;

        /******************
        ****CONSTRUCTOR****
        ******************/
        public Circle(int r, int g, int b, int x, int y, int hauteur, int largeur, double orientation, int vitesse) : base(r,g,b,x,y,hauteur,largeur,orientation,vitesse)
        {
            //Initialize attribute
            _rec = new Rectangle(_x, _y, _largeur, _hauteur);
        }

        /*****************
        **PUBLIC METHODS**
        *****************/

        /*This method provides a simple way to draw a circle on a specified Graphics e*/
        public override void draw(Graphics e)
        {
            e.FillEllipse(brush, _rec);
        }

        /*This method provides a simple way to move a circle*/
        public override void move()
        {
            _x += (int)(_vitesse * Math.Cos(_orientation * (Math.PI / 180)));
            _y += (int)(_vitesse * Math.Sin(_orientation * (Math.PI / 180)));
            _rec.X = _x;
            _rec.Y = _y;
        }

        /*****************
        **STATIC METHODS**
        *****************/

        /*This static method provides a simple way to preview a circle element*/
        public static void preview(Graphics e,int x,int y)
        {
            e.FillEllipse(new SolidBrush(Color.Black), new Rectangle(x, y, 20, 20));
        }
    }
}
