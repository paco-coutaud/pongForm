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
        public Cercle(pongForm.Form1 context, int r, int g, int b, int x, int y, int hauteur, int largeur, double orientation, int vitesse) : base(context,r,g,b,x,y,hauteur,largeur,orientation,vitesse)
        {
            //Initialize attribute
            rec = new Rectangle(_x, _y, _largeur, _hauteur);
        }
        public override void dessine()
        {
            graphic.FillEllipse(brush, rec);
        }

        public override void deplace()
        {
            _x += 1; //Update x atribute
            _y += 1; //Update y attribute

            //Update rectangle
            rec.X = _x;
            rec.Y = _y;
            rec.Height = _hauteur;
            rec.Width = _largeur;
        }

        public override void clear()
        {
            graphic.Clear(Color.White); //Clear graphics
        }

        ~Cercle()
        {
            graphic.Dispose(); //Delete graphic
        }
    }
}
