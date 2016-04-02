/****************************
****AUTHOR : Paco COUTAUD****
****AUTHOR : Gauthier CASTRO*
**LAST CHANGES : 28/03/2016**
****************************/

using System.Drawing;

namespace Pong
{
    /*This is an abstract class*/
    public abstract class Mobile : Shape
    {
        public double _speed { get; set; } //Mobile's speed

        /******************
        ****CONSTRUCTOR****
        ******************/
        public Mobile(int r, int g, int b, int x, int y, int height, int width, double orientation, double speed) : base(r,g,b,x,y,height,width,orientation)
        {
            _speed = speed;
        }

        /*****************
        **PUBLIC METHODS**
        *****************/
        public abstract override void draw(Graphics e); //Need to be reimplemented in child class
        public abstract void move(); //Need to be reimplemented in child class
    }
}
