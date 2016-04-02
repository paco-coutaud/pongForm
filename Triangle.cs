using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;

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

        public Triangle(int r, int g, int b, int x, int y, int height, int width, double orientation, double speed) : base(r,g,b,x,y,height,width,orientation,speed)
        {
            point1 = new PointF(_x, _y + _width);
            point2 = new PointF(_x + _width, _y + _height);
            point3 = new PointF(_x + (_width/2), _y);

            points = new List<PointF>();
            points.Add(point1);
            points.Add(point2);
            points.Add(point3);

            fillMethode = FillMode.Winding;
        }
        public override void draw(Graphics e)
        {
            brush.Color = _color.getColor();
            e.FillPolygon(brush, points.ToArray(), fillMethode);
        }

        public static void preview(Graphics e, int x, int y)
        {
            PointF pointPreview1 = new PointF(x, y);
            PointF pointPreview2 = new PointF(x + 20, y);
            PointF pointPreview3 = new PointF(x + (20 / 2), y - 20);

            List<PointF> pointsDemo = new List<PointF>();
            pointsDemo.Add(pointPreview1);
            pointsDemo.Add(pointPreview2);
            pointsDemo.Add(pointPreview3);

            e.FillPolygon(new SolidBrush(Color.Black), pointsDemo.ToArray(), FillMode.Winding);
        }

        public override void move()
        {
            _x += (int)(_speed * Math.Cos(_orientation * (Math.PI / 180)));
            _y += (int)(_speed * Math.Sin(_orientation * (Math.PI / 180)));

            point1.X = _x;
            point1.Y = _y + _height;
            point2.X = _x + _width;
            point2.Y = _y + _height;
            point3.X = _x + (_width / 2);
            point3.Y = _y;

            points.Clear();
            points.Add(point1);
            points.Add(point2);
            points.Add(point3);
        }
    }
}
