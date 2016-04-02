/****************************
****AUTHOR : Paco COUTAUD****
****AUTHOR : Gauthier CASTRO*
**LAST CHANGES : 28/03/2016**
****************************/

using System.Drawing;

namespace Pong
{
    /*This class provides methods to create and manipulate Walls*/
    public class Wall : Shape
    {
        public double _coefficient { get; set; }
        private Rectangle _rec;

        /******************
        ****CONSTRUCTOR****
        ******************/
        public Wall(int r, int g, int b, int x, int y, int height, int width, int orientation, double coefficient) : base(r,g,b,x,y,height,width,orientation)
        {
            _coefficient = coefficient;
            _rec = new Rectangle(_x, _y, _width, _height);
        }

        /*****************
        **PUBLIC METHODS**
        *****************/
        public override void draw(Graphics e)
        {
            e.FillRectangle(brush, _rec);
        }

        public static void preview(Graphics e, int x, int y)
        {
            SolidBrush n = new SolidBrush(Color.Black); //Wall has black color
            e.FillRectangle(n, x, y, 40, 40); //Draw a wall on the windows forms surface
            n.Dispose(); //Free the ressource
        }
    }
}
