using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Pong
{
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

        public Color getColor()
        {
            return _color;
        }
    }
}
