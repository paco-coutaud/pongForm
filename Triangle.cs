using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Pong
{
    public class Triangle : Mobile
    {
        //Need 3 points to create a triangle
        private PointF point1;
        private PointF point2;
        private PointF point3;

        //Add in PointF, 3 points
        private List<PointF> points;

        //Fill mode
        private FillMode fillMethode;

        public Triangle(int r, int g, int b, int x, int y, int hauteur, int largeur, double orientation, int vitesse) : base(r,g,b,x,y,hauteur,largeur,orientation,vitesse)
        {
            //points = new PointF[];
            point1 = new PointF(_x, _y);
            point2 = new PointF(_x + _largeur, _y);
            point3 = new PointF(_x + (_largeur / 2), _y - _hauteur);

            points = new List<PointF>();
            points.Add(point1);
            points.Add(point2);
            points.Add(point3);

            fillMethode = FillMode.Winding;
        }
        public override void dessine(Graphics e)
        {
            e.FillPolygon(brush, points.ToArray(), fillMethode);
        }

        public override void deplace(int typeCollision)
        {
            point1.X += 1;
            point1.Y += 1;
            point2.X += 1;
            point2.Y += 1;
            point3.X += 1;
            point3.Y += 1;

            points.Clear();
            points.Add(point1);
            points.Add(point2);
            points.Add(point3);

        }
    }
}
