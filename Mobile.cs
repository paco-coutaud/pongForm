/****************************
****AUTHOR : Paco COUTAUD****
****AUTHOR : Gauthier CASTRO*
**LAST CHANGES : 28/03/2016**
****************************/

using System.Drawing;

namespace Pong
{
    /*This is an abstract class*/
    public abstract class Mobile : Forme
    {
        protected int _vitesse; //Mobile's speed

        /******************
        ****CONSTRUCTOR****
        ******************/
        public Mobile(int r, int g, int b, int x, int y, int hauteur, int largeur, double orientation, int vitesse) : base(r,g,b,x,y,hauteur,largeur,orientation)
        {
            _vitesse = vitesse;
        }

        /*****************
        **PUBLIC METHODS**
        *****************/
        public abstract override void draw(Graphics e); //Need to be reimplemented in child class
        public abstract void move(); //Need to be reimplemented in child class
    }
}
