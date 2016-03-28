/****************************
****AUTHOR : Paco COUTAUD****
****AUTHOR : Gauthier CASTRO*
**LAST CHANGES : 28/03/2016**
****************************/

using System.Drawing;

namespace Pong
{
    /*This class provides methods to create and manipulate Walls*/
    public class Wall : Forme
    {
        private double _coefficient;
        private Rectangle _rec;

        /******************
        ****CONSTRUCTOR****
        ******************/
        public Wall(int r, int g, int b, int x, int y, int hauteur, int largeur, int orientation, double coefficient) : base(r,g,b,x,y,hauteur,largeur,orientation)
        {
            _coefficient = coefficient;
            _rec = new Rectangle(_x, _y, _largeur, _hauteur);
        }

        /*****************
        **PUBLIC METHODS**
        *****************/
        public override void draw(Graphics e)
        {
            e.FillRectangle(brush, _rec);
        }
    }
}
