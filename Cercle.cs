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
        private double realX;
        private double realY;


        /******************
        ****CONSTRUCTOR****
        ******************/
        public Circle(int r, int g, int b, int x, int y, int hauteur, int largeur, double orientation, double vitesse) : base(r,g,b,x,y,hauteur,largeur,orientation,vitesse)
        {
            //Initialize attribute
            _rec = new Rectangle(_x, _y, _largeur, _hauteur);
            realX = x;
            realY = y;
            //_rec.Offset(_largeur / 2, _hauteur / 2);
            //_rec.Location.Offset(_largeur / 2, _hauteur / 2);
            //_rec.Location.= _largeur / 2;
            //_rec.Location.Y
            System.Diagnostics.Debug.WriteLine("Cercle, x : " + _x + " y : "+ _y);
            System.Diagnostics.Debug.WriteLine("Centre : x : " + _rec.Location.X + "y: " + _rec.Location.Y);
        }

        /*****************
        **PUBLIC METHODS**
        *****************/

        /*This method provides a simple way to draw a circle on a specified Graphics e*/
        public override void draw(Graphics e)
        {
            brush.Color = _color.getColor();
            e.FillEllipse(brush, _rec);
        }

        /*This method provides a simple way to move a circle*/
        public override void move()
        {
            realX += (_vitesse * Math.Cos(_orientation * (Math.PI / 180)));
            realY += (_vitesse * Math.Sin(_orientation * (Math.PI / 180)));
            _x = (int)(realX);
            _y = (int)(realY);
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
