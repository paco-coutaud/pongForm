using System.Drawing;

namespace Pong
{

    public abstract class Mobile : Forme
    {
        protected int _vitesse; //Speed of the mobile
        protected int _collision; //type of collision

        public Mobile(int r, int g, int b, int x, int y, int hauteur, int largeur, double orientation, int vitesse) : base(r,g,b,x,y,hauteur,largeur,orientation)
        {
            _collision = 0;
            _vitesse = vitesse;
        }

        public int getCollision()
        {
            return _collision;
        }

        public void setCollision(int collision)
        {
            _collision = collision;
        }

        public abstract override void dessine(Graphics e);
        public abstract void deplace(int typeCollision);
    }

}
