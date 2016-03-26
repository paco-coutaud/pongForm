using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pong
{
    public partial class Pong : Form //Herite from Form
    {
        //Contains mobile elements
        private List<Mobile> listMobile;
        public Pong()
        {
            InitializeComponent(); //Initialize all kind of components

            this.BackColor = Color.White; //Set background Color to white
            this.Width = 800; //Set windows width
            this.Height = 800; //Set window height
            this.Text = "Animation Pong"; //Set windows title

            //Initialize attributes
            listMobile = new List<Mobile>();

            //Add mobiles to mobile list
            addMobile(new Cercle(255, 0, 0, 10, 10, 80, 80, 45, 1));
            addMobile(new Cercle(0, 255, 0, 170, 10, 20, 20, 60, 3));
            addMobile(new Cercle(0, 0, 255, 10, 100, 80, 80, 40, 4));
            addMobile(new Cercle(0, 0, 0, 30, 30, 100, 90, 50, 2));
            addMobile(new Triangle(0, 0, 0, 30, 30, 100, 90, 0, 6));

            //Enable double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Reimplemente methods
            this.Paint += new PaintEventHandler(form1_paint);
        }

        private void form1_paint(Object sender, PaintEventArgs e)
        {
            execute(e.Graphics);
            Invalidate();
            System.Threading.Thread.Sleep(1);
        }

        public void execute(Graphics e)
        {
            foreach (Mobile element in listMobile)
            {
                Collision(); //Check collision
                element.deplace(element.getCollision()); //Move mobiles with collision contraints
                element.dessine(e); //Draw
                System.Diagnostics.Debug.WriteLine("x : " + element.getX() + " y : " + element.getY());
            }
        }

        public void Collision()
        {
            for (int i = 0; i < listMobile.Count; i++)
            {

                if (listMobile.ElementAt(i).getX()+listMobile.ElementAt(i).getWidth() >= this.Width) //On sort à droite
                {
                    listMobile.ElementAt(i).setCollision(0);
                    listMobile.ElementAt(i).setOrientation(listMobile.ElementAt(i).getOrientation() + (180 - listMobile.ElementAt(i).getOrientation()) * 2);
                }
                else if (listMobile.ElementAt(i).getX() <= 0) //On a touche à gauche
                {
                    listMobile.ElementAt(i).setCollision(1);
                    listMobile.ElementAt(i).setOrientation(listMobile.ElementAt(i).getOrientation() + (180 - listMobile.ElementAt(i).getOrientation()) * 2);
                }
                else if (listMobile.ElementAt(i).getY() <= 0) //On a touche en haut
                {
                    listMobile.ElementAt(i).setCollision(2);
                    listMobile.ElementAt(i).setOrientation(listMobile.ElementAt(i).getOrientation() + (90 - listMobile.ElementAt(i).getOrientation()) * 2);
                }
                else if (listMobile.ElementAt(i).getY()+listMobile.ElementAt(i).getHeight() >= this.Height) //On a touche en bas
                {
                    listMobile.ElementAt(i).setCollision(3);
                    listMobile.ElementAt(i).setOrientation(listMobile.ElementAt(i).getOrientation() + (90 - listMobile.ElementAt(i).getOrientation()) * 2);
                }
            }
        }

        public void addMobile(Mobile m)
        {
            listMobile.Add(m);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Pong
            // 
            this.ClientSize = new System.Drawing.Size(652, 328);
            this.Name = "Pong";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }
    }
}
