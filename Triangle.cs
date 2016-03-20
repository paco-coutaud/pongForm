using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Pong
{
    public class Triangle : Mobile
    {
        public Triangle(pongForm.Form1 context, int r, int g, int b, int x, int y, int hauteur, int largeur, double orientation, int vitesse) : base(context,r,g,b,x,y,hauteur,largeur,orientation,vitesse)
        {

        }
        public override void dessine()
        {
            SolidBrush brush = new SolidBrush(_color.getColor());

            PointF point1 = new PointF(_x, _y);
            PointF point2 = new PointF(_x+_largeur, _y);
            PointF point3 = new PointF(_x+(_largeur/2), _y-_hauteur);

            PointF[] points = {point1,point2,point3};

            FillMode fillMethode = FillMode.Winding;

            Graphics graphic = _context.CreateGraphics();
            graphic.FillPolygon(brush, points, fillMethode);
            graphic.Dispose();
        }

        public override void deplace()
        {
            throw new NotImplementedException();
        }

        public override void clear()
        {
            throw new NotImplementedException();
        }
    }
}
