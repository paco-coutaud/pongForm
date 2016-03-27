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
        private bool running;
        public Pong()
        {
            InitializeComponent(); //Initialize all kind of components

            this.BackColor = Color.White; //Set background Color to white
            this.Width = 500; //Set windows width
            this.Height = 500; //Set window height
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
            if (running == true)
            {
                execute(e.Graphics);
                Invalidate();
            }
                //System.Threading.Thread.Sleep(1);
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
                    listMobile.ElementAt(i).setOrientation(-listMobile.ElementAt(i).getOrientation());
                    listMobile.ElementAt(i).setOrientation(listMobile.ElementAt(i).getOrientation() + (new Random().Next(5, 25)));
                }
                else if (listMobile.ElementAt(i).getX() <= 0) //On a touche à gauche
                {
                    listMobile.ElementAt(i).setCollision(1);
                    listMobile.ElementAt(i).setOrientation(-listMobile.ElementAt(i).getOrientation());
                    listMobile.ElementAt(i).setOrientation(listMobile.ElementAt(i).getOrientation() + (new Random().Next(5, 25)));
                }
                else if (listMobile.ElementAt(i).getY() <= 0) //On a touche en haut
                {
                    listMobile.ElementAt(i).setCollision(2);
                    listMobile.ElementAt(i).setOrientation(-listMobile.ElementAt(i).getOrientation());
                    listMobile.ElementAt(i).setOrientation(listMobile.ElementAt(i).getOrientation() + (new Random().Next(5, 25)));
                }
                else if (listMobile.ElementAt(i).getY()+listMobile.ElementAt(i).getHeight() >= this.Height) //On a touche en bas
                {
                    listMobile.ElementAt(i).setCollision(3);
                    listMobile.ElementAt(i).setOrientation(-listMobile.ElementAt(i).getOrientation());
                    listMobile.ElementAt(i).setOrientation(listMobile.ElementAt(i).getOrientation() + (new Random().Next(5, 25)));
                }
            }
        }

        public void addMobile(Mobile m)
        {
            listMobile.Add(m);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 26);
            // 
            // ajouterToolStripMenuItem
            // 
            this.ajouterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.ajouterToolStripMenuItem.Name = "ajouterToolStripMenuItem";
            this.ajouterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ajouterToolStripMenuItem.Text = "Ajouter";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem3.Text = "Cercle";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem4.Text = "Triangle";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Pong
            // 
            this.ClientSize = new System.Drawing.Size(652, 328);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Pong";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e) //Bouton cercle du menu ajouter
        {
            int randWidthHeight = new Random().Next(10, 60);
            addMobile(new Cercle(new Random().Next(0, 255), new Random().Next(0, 255), new Random().Next(0, 255), MousePosition.X, MousePosition.Y, randWidthHeight, randWidthHeight, new Random().Next(10, 90), new Random().Next(1, 5)));
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e) //Bouton triangle du menu ajouter
        {
            int randWidthHeight = new Random().Next(20, 120);
            addMobile(new Triangle(new Random().Next(0, 255), new Random().Next(0, 255), new Random().Next(0, 255), MousePosition.X, MousePosition.Y, randWidthHeight, randWidthHeight, new Random().Next(10, 90), 0));
        }

        private void button2_Click(object sender, EventArgs e) //Bouton start/stop click
        {
            if(button2.Text == "Start")
            {
                button2.Text = "Stop";
                running = true;
            }
            else
            {
                button2.Text = "Start";
                running = false;
            }
        }
    }
}
