using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Pong
{
    public class Cercle : Mobile //Herite from Mobile
    {
        private Rectangle rec;
        public Cercle(int r, int g, int b, int x, int y, int hauteur, int largeur, double orientation, int vitesse) : base(r,g,b,x,y,hauteur,largeur,orientation,vitesse)
        {
            //Initialize attribute
            rec = new Rectangle(_x, _y, _largeur, _hauteur);
        }
        public override void dessine(Graphics e)
        {
            e.FillEllipse(brush, rec);
        }

        public static void preview(Graphics e,int x,int y)
        {
            e.FillEllipse(new SolidBrush(Color.Black), new Rectangle(x, y, 20, 20));
        }

        public override void deplace(int typeCollision)
        {
            //Updates rectangle
            /*if(typeCollision == 0)
            {
                moveLeft();
            }
            else if(typeCollision == 1)
            {
                moveRight();
            }
            else if(typeCollision == 2)
            {
                moveDown();
            }
            else if(typeCollision == 3)
            {
                moveUp();
            }*/

            _y += (int)(_vitesse * Math.Sin(_orientation * (Math.PI / 180)));
            _x += (int)(_vitesse * Math.Cos(_orientation * (Math.PI / 180)));
            rec.X = _x;
            rec.Y = _y;
        }

        public void moveUp()
        {
            _y -= _vitesse; //(int)(1*_vitesse*Math.Sin(_orientation) * (Math.PI / 180));
            _x -= 1;
            //_x += 1;
            rec.Y = _y;
            rec.X = _x;
        }

        public void moveDown()
        {
            _x += 1;
            _y += _vitesse; //(int)(1*_vitesse * Math.Sin(_orientation * (Math.PI / 180)));
            //_x += 1;
            rec.Y = _y;
            rec.X = _x;
        }

        public void moveRight()
        {
            _x += _vitesse; //(int)(1*_vitesse * Math.Cos(_orientation * (Math.PI / 180)));
            _y += 1;
            //_y += 1;
            rec.X = _x;
            rec.Y = _y;
        }

        public void moveLeft()
        {
            _x -= _vitesse;//(int)(1*_vitesse * Math.Cos(_orientation * (Math.PI / 180)));
            _y -= 1;
            //_y += 1;
            rec.X = _x;
            rec.Y = _y;
        }
    }
}
