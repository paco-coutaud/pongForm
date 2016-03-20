using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    class PongT
    {
        private List<Mobile> listMobile;
        public PongT(pongForm.Form1 context)
        {
            listMobile = new List<Mobile>();
            addMobile(new Cercle(context, 255, 0, 0, 10, 10, 80, 80, 0, 0));
            addMobile(new Cercle(context, 0, 255, 0, 170, 10, 20, 20, 0, 0));
            addMobile(new Cercle(context, 0, 0, 255, 10, 100, 80, 80, 0, 0));
            addMobile(new Cercle(context, 0, 0, 0, 30, 30, 100, 90, 0, 0));
        }

        public void execute()
        {
            foreach (Mobile element in listMobile)
            {
                element.clear();
            }

            foreach (Mobile element in listMobile)
            {
                element.deplace();
                element.dessine();
            }
        }
        public void Collision()
        {

        }

        public void addMobile(Mobile m)
        {
            listMobile.Add(m);
        }
    }
}
