using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public class Mur : Forme
    {
        private double _coefficient;
        private Rectangle _rec;

        public Mur(int r, int g, int b, int x, int y, int hauteur, int largeur, int orientation, double coefficient) : base(r,g,b,x,y,hauteur,largeur,orientation)
        {
            _coefficient = coefficient;
            _rec = new Rectangle(_x, _y, _largeur, _hauteur);
        }

        public override void dessine(Graphics e)
        {
            e.FillRectangle(brush, _rec);
        }

        /*public static void showWall(Graphics e)
        {
            for(int i= 0; i<12; i++)
            {
                for(int j=0;j<12;j++)
                {
                    _x = 10 + j * 40;
                    _y = 10 + i * 40;
                    _largeur = 40;
                    _hauteur = 40;
                    _rec.Height = _hauteur;
                    _rec.Width = _largeur;
                    _rec.X = _x;
                    _rec.Y = _y;
                    dessine(e);
                }
            }
        }*/
    }
}
