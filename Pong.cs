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
        private List<Mobile> listMobile; //Contains mobile elements
        private List<Mur> listWall; //Contains wall elements
        private bool running; //Useful to know if the animation is lauch
        private bool addWallBool; //Useful to know if we are adding a wall
        private bool addCircleBool;
        private bool addTriangleBool;

        public Pong(String title, int windowsWidth, int windowsHeight)
        {
            InitializeComponent(); //Initialize all kind of graphics components
            initWindowsProperties(title,windowsWidth,windowsHeight);
            initStateVariable();
            initListAttributes();
            initializeEnvironment();
        }

        private void initWindowsProperties(String title, int windowsWidth,int windowsHeight)
        {
            //Set graphics properties
            this.BackColor = Color.White; //Set background Color to white
            this.Width = windowsWidth; //Set windows width
            this.Height = windowsHeight; //Set window height
            this.Text = title; //Set windows title

            //Enable double buffering to disable flickering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void initStateVariable()
        {
            //Initialize boolean variable
            addWallBool = false;
            running = false;
            addTriangleBool = false;
            addCircleBool = false;
        }

        private void initListAttributes()
        {
            //Initialize list attributes
            listMobile = new List<Mobile>();
            listWall = new List<Mur>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Reimplemente methods
            this.Paint += new PaintEventHandler(form1_paint);
        }

        private void form1_paint(Object sender, PaintEventArgs e)
        {
            if (running == true) //Simulate loop
            {
                
                execute(e.Graphics);
                Invalidate(); //Useful to invalidate windows forms and redraw elements

                if(addWallBool == true)
                {
                    showPositionWall(e.Graphics); //Show the wall we are adding on screen
                    Invalidate(); //Useful to invalidate windows forms and redraw elements
                }
                if(addCircleBool == true)
                {
                    showPositionCircle(e.Graphics);
                }
                if(addTriangleBool == true)
                {
                    showPositionTriangle(e.Graphics);
                }

                //System.Threading.Thread.Sleep(2);

            }
            /*else if(addWallBool == true) //We are adding a wall
            {
                showPositionWall(e.Graphics); //Show the wall we are adding on screen
                Invalidate(); //Useful to invalidate windows forms and redraw elements
            }*/
            
        }

        private void initMobiles()
        {
            //Add mobiles to mobile list
            /*addMobile(new Cercle(255, 0, 0, 10, 10, 80, 80, 45, 1));
            addMobile(new Cercle(0, 255, 0, 170, 10, 20, 20, 60, 1));
            addMobile(new Cercle(0, 0, 255, 10, 100, 80, 80, 40, 1));
            addMobile(new Cercle(0, 0, 0, 30, 30, 100, 90, 50, 1));
            addMobile(new Triangle(0, 0, 0, 30, 30, 100, 90, 0, 1));*/
        }
        private void initWalls() //Algo for adding walls depending on the windows form resolution
        {
            int i = 0;
            int l = 0;
            for (int j = 0; j < (int)(this.Width/40); j++) //For lines
            {
                if (i % 2 == 0) //i pair
                {
                    addWall(new Mur(0, 0, 0, j * 40, 30, 20, 40, 0, 0)); //Black color
                    addWall(new Mur(0, 0, 0, j * 40, this.Height-70, 20, 40, 0, 0)); //Black color
                }
                else
                {
                    addWall(new Mur(255, 0, 0, j * 40, 30, 20, 40, 0, 0)); //red color
                    addWall(new Mur(255, 0, 0, j * 40, this.Height - 70,20, 40, 0, 0)); //Black color
                }

                i++;

                for (int k = 0; k < (int)((this.Height)/40)-2; k++)
                {
                    if (l % 2 == 0)
                    {
                        addWall(new Mur(0, 0, 0, 0, 50 + k * 40, 40, 20, 0, 0));
                        addWall(new Mur(0, 0, 0, (this.Width)-40, 50 + k * 40, 40, 20, 0, 0));

                    }
                    else
                    {
                        addWall(new Mur(255, 0, 0, 0, 50 + k * 40, 40, 20, 0, 0));
                        addWall(new Mur(255, 0, 0, this.Width-40, 50 + k * 40, 40, 20, 0, 0));
                    }

                    l++;
                }
            }
        }
        public void initializeEnvironment()
        {
            initWalls();
            initMobiles();
        }

        public void execute(Graphics e)
        {
            foreach (Mur element in listWall)
            {
                element.dessine(e); //Draw walls
            }

            foreach (Mobile element in listMobile)
            {
                Collision(); //Check mobiles collision
                element.deplace(element.getCollision()); //Move mobiles with collision contraints
                element.dessine(e); //Draw mobiles
                //System.Diagnostics.Debug.WriteLine("x : " + element.getX() + " y : " + element.getY()); //Useful for debugging
            }
        }

        public void Collision() //Currently not working
        {
            //Pour les bords
            for (int i = 0; i < listMobile.Count; i++)
            {
                if((listMobile.ElementAt(i).getX() + listMobile.ElementAt(i).getWidth()) >= this.Width || listMobile.ElementAt(i).getX() <= 0 || listMobile.ElementAt(i).getY() <= 0 ||
                    listMobile.ElementAt(i).getY() + listMobile.ElementAt(i).getHeight() >= this.Height)
                {
                    listMobile.ElementAt(i).setOrientation(listMobile.ElementAt(i).getOrientation() + 90);
                }

                /*for(int j=0; j<listWall.Count;j++)
                {
                    if((listMobile.ElementAt(i).getX() + listMobile.ElementAt(i).getWidth()) >= listWall.ElementAt(i).getX() && (listMobile.ElementAt(i).getY() + listMobile.ElementAt(i).getWidth())
                }*/
            }
        }

        /*This method provide a simple way to add mobile element into the list
        It take one argument : m is the mobile to add*/
        public void addMobile(Mobile m)
        {
            listMobile.Add(m);
        }

        /*This method provide a simple way to add wall element into the list
        It take one argument : w is the wall to add*/
        public void addWall(Mur w)
        {
            listWall.Add(w);
        }

        /*This method provide a simple way to preview a wall which will be add to the wall list after
        It permits to show the position where the wall will be added
        It take one argument : e is the graphic element where the wall will be draw*/
        public void showPositionWall(Graphics e)
        {
            var cor = PointToClient(Cursor.Position);
            SolidBrush n = new SolidBrush(Color.Black); //Wall has black color
            e.FillRectangle(n, cor.X, cor.Y, 40, 40); //Draw a wall on the windows forms surface
        }

        public void showPositionCircle(Graphics e)
        {
            var cor = PointToClient(Cursor.Position);
            Cercle.preview(e, cor.X, cor.Y);
        }

        public void showPositionTriangle(Graphics e)
        {
            var cor = PointToClient(Cursor.Position);
            Triangle.preview(e, cor.X, cor.Y);
        }

        /*Initialize all kind of graphics components*/
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.globalMenu = new System.Windows.Forms.MenuStrip();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajouterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.globalMenu.SuspendLayout();
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
            this.ajouterToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.ajouterToolStripMenuItem.Text = "Ajouter";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem3.Text = "Cercle";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.addCircleClick);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem4.Text = "Triangle";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.addTriangleClick);
            // 
            // globalMenu
            // 
            this.globalMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionToolStripMenuItem,
            this.ajouterToolStripMenuItem1});
            this.globalMenu.Location = new System.Drawing.Point(0, 0);
            this.globalMenu.Name = "globalMenu";
            this.globalMenu.Size = new System.Drawing.Size(652, 24);
            this.globalMenu.TabIndex = 6;
            this.globalMenu.Text = "globalMenu";
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionToolStripMenuItem.Text = "Action";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.actionStartClick);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetClick);
            // 
            // ajouterToolStripMenuItem1
            // 
            this.ajouterToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7});
            this.ajouterToolStripMenuItem1.Name = "ajouterToolStripMenuItem1";
            this.ajouterToolStripMenuItem1.Size = new System.Drawing.Size(58, 20);
            this.ajouterToolStripMenuItem1.Text = "Ajouter";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem5.Text = "Cercle";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.addCircleClick);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem6.Text = "Triangle";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.addTriangleClick);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem7.Text = "Mur";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.addWallClick);
            // 
            // Pong
            // 
            this.ClientSize = new System.Drawing.Size(652, 328);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.globalMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.globalMenu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Pong";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Pong_Click);
            this.contextMenuStrip1.ResumeLayout(false);
            this.globalMenu.ResumeLayout(false);
            this.globalMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /*Will be call only if signal on add circle button is receive*/
        private void addCircleClick(object sender, EventArgs e)
        {
            addCircleBool = true;
        }

        /*Will be call only if signal on add triangle button is receive*/
        private void addTriangleClick(object sender, EventArgs e)
        {
            addTriangleBool = true;
        }

        /*Will be call only if signal on start/stop button is receive*/
        private void actionStartClick(object sender, EventArgs e)
        {
            if(startToolStripMenuItem.Text == "Start") //If we click on start button
            {
                startToolStripMenuItem.Text = "Stop"; //Change text on "Stop"
                running = true; //Set to true the running boolean variable
            }
            else //We click on stop button
            {
                startToolStripMenuItem.Text = "Start"; //Change text on "Start"
                running = false; //Set to false the running boolean variable
            }

            Invalidate();
        }

        /*Will be call only if signal on add wall button is receive*/
        private void addWallClick(object sender, EventArgs e)
        {
                addWallBool = true; //We are currently adding a new wall
                Invalidate();
        }

        /*Will be call only if there is a click on the windows form region is receive*/
        private void Pong_Click(object sender, EventArgs e)
        {
            if(addWallBool == true) //If we are adding a wall
            {
                var cor = PointToClient(Cursor.Position);
                addWall(new Mur(0, 0, 0, cor.X, cor.Y, 40, 40, 0, 0)); //We add the wall to the list
                addWallBool = false; //We are not adding a wall because it's the previous step
            }
            if(addCircleBool == true)
            {
                Random rand = new Random(); //Initialize new instance of Random.
                int randWidthHeight = rand.Next(20, 120); //Generate the same aleatory width/height
                addMobile(new Cercle(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, randWidthHeight, randWidthHeight, rand.Next(0, 90), rand.Next(1, 5)));
                addCircleBool = false;
            }
            if(addTriangleBool == true)
            {
                Random rand = new Random(); //Initialize new instance of Random.
                int randWidthHeight = rand.Next(20, 120); //Generate the same aleatory width/height
                addMobile(new Triangle(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), MousePosition.X, MousePosition.Y, randWidthHeight, randWidthHeight, rand.Next(10, 90), 0));
                addTriangleBool = false;
            }
        }

        private void resetClick(object sender, EventArgs e) //reset
        {
            listMobile.Clear();
            listWall.Clear();
            initializeEnvironment();
        }
    }
}
