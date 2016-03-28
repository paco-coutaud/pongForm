/****************************
****AUTHOR : Paco COUTAUD****
****AUTHOR : Gauthier CASTRO*
**LAST CHANGES : 28/03/2016**
****************************/

using System.Drawing;

namespace Pong
{
    /******************
    ****CONSTRUCTOR****
    ******************/
    public class Couleur
    {
        private int _r;
        private int _v;
        private int _b;

        private Color _color;

        public Couleur(int r, int v, int b)
        {
            _r = r;
            _v = v;
            _b = b;

            _color = Color.FromArgb(_r, _v, _b);
        }

        /*****************
        **PUBLIC METHODS**
        *****************/
        public Color getColor()
        {
            return _color;
        }
    }
}
