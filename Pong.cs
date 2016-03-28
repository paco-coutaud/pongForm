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
        private List<Wall> listWall; //Contains wall elements
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

            System.Diagnostics.Debug.Write("Il y a " + listWall.Count + "murs dans la liste");
            System.Diagnostics.Debug.Write("Il y a " + listMobile.Count + "mobiles dans la liste");
        }

        private void initWindowsProperties(String title, int windowsWidth,int windowsHeight)
        {
            //Set graphics properties
            this.BackColor = Color.White; //Set background Color to white
            this.Width = windowsWidth; //Set windows width
            this.Height = windowsHeight; //Set window height
            this.Text = title; //Set windows title

            this.buttonDeleteElement.Enabled = false;
            this.buttonChangeMobileColor.Enabled = false;
            this.buttonDecreaseSpeed.Enabled = false;
            this.buttonIncreaseSpeed.Enabled = false;
            //this.buttonDeleteWall.Enabled = false;

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
            listWall = new List<Wall>();
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
                    //Invalidate(); //Useful to invalidate windows forms and redraw elements
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
            for (int j = 0; j < (int)((this.Width-100)/40); j++) //For lines
            {
                if (i % 2 == 0) //i pair
                {
                    addWall(new Wall(0, 0, 0, j * 40, 30, 20, 40, 0, 0)); //Black color
                    addWall(new Wall(0, 0, 0, j * 40, this.Height-110, 20, 40, 0, 0)); //Black color
                }
                else
                {
                    addWall(new Wall(255, 0, 0, j * 40, 30, 20, 40, 0, 0)); //red color
                    addWall(new Wall(255, 0, 0, j * 40, this.Height - 110,20, 40, 0, 0)); //Black color
                }

                i++;
            }

            for (int k = 0; k < (int)(((this.Height - 100)) / 40); k++)
            {
                if (l % 2 == 0)
                {
                    addWall(new Wall(0, 0, 0, 0, 30 + k * 40, 40, 20, 0, 0));
                    addWall(new Wall(0, 0, 0, (this.Width) - 140, 30 + k * 40, 40, 20, 0, 0));

                }
                else
                {
                    addWall(new Wall(255, 0, 0, 0, 30 + k * 40, 40, 20, 0, 0));
                    addWall(new Wall(255, 0, 0, this.Width - 140, 30 + k * 40, 40, 20, 0, 0));
                }

                l++;
            }
        }
        public void initializeEnvironment()
        {
            initWalls();
            initMobiles();
        }

        public void execute(Graphics e)
        {
            foreach (Wall element in listWall)
            {
                element.draw(e); //Draw walls
            }

            foreach (Mobile element in listMobile)
            {
                Collision(element); //Check mobiles collision
                
                //element.deplace(element.getCollision()); //Move mobiles with collision contraints
                element.draw(e); //Draw mobiles
                //System.Diagnostics.Debug.WriteLine("x : " + element.getX() + " y : " + element.getY()); //Useful for debugging
            }
        }

        public void Collision(Mobile m) //Currently not working
        {
            //Pour les bords
            //for (int i = 0; i < listMobile.Count; i++)
            //{
                /*if((listMobile.ElementAt(i).getX() + listMobile.ElementAt(i).getWidth()) >= this.Width || listMobile.ElementAt(i).getX() <= 0 || listMobile.ElementAt(i).getY() <= 0 ||
                    listMobile.ElementAt(i).getY() + listMobile.ElementAt(i).getHeight() >= this.Height)
                {
                    listMobile.ElementAt(i).setOrientation(listMobile.ElementAt(i).getOrientation() + 90);
                }*/

                for(int j=0; j<listWall.Count; j++)
                {
                    if ((m.getX() >= (listWall.ElementAt(j).getX() + listWall.ElementAt(j).getWidth())) || ((m.getX() + m.getWidth()) <= listWall.ElementAt(j).getX()) // trop à gauche
                    || (m.getY() >= (listWall.ElementAt(j).getY() + listWall.ElementAt(j).getHeight())) || ((m.getY() + m.getHeight()) <= listWall.ElementAt(j).getY()))
                    {
                        m.setOrientation(m.getOrientation() + 90);
                    }
                }

            m.move();

            /*for(int j=0; j<listWall.Count;j++)
            {
                if((listMobile.ElementAt(i).getX() + listMobile.ElementAt(i).getWidth()) >= listWall.ElementAt(i).getX() && (listMobile.ElementAt(i).getY() + listMobile.ElementAt(i).getWidth())
            }*/
            // }
        }

        /*This method provide a simple way to add mobile element into the list
        It take one argument : m is the mobile to add*/
        public void addMobile(Mobile m)
        {
            listMobile.Add(m);
        }

        /*This method provide a simple way to add wall element into the list
        It take one argument : w is the wall to add*/
        public void addWall(Wall w)
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
            Circle.preview(e, cor.X, cor.Y);
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonDeleteElement = new System.Windows.Forms.Button();
            this.buttonChangeMobileColor = new System.Windows.Forms.Button();
            this.buttonIncreaseSpeed = new System.Windows.Forms.Button();
            this.buttonDecreaseSpeed = new System.Windows.Forms.Button();
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
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(490, 30);
            this.listBox1.Margin = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(90, 147);
            this.listBox1.TabIndex = 7;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // buttonDeleteElement
            // 
            this.buttonDeleteElement.Location = new System.Drawing.Point(490, 338);
            this.buttonDeleteElement.Name = "buttonDeleteElement";
            this.buttonDeleteElement.Size = new System.Drawing.Size(90, 39);
            this.buttonDeleteElement.TabIndex = 8;
            this.buttonDeleteElement.Text = "Delete Mobile Selected";
            this.buttonDeleteElement.UseVisualStyleBackColor = true;
            this.buttonDeleteElement.Click += new System.EventHandler(this.buttonDeleteElement_Click);
            // 
            // buttonChangeMobileColor
            // 
            this.buttonChangeMobileColor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonChangeMobileColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonChangeMobileColor.Location = new System.Drawing.Point(490, 195);
            this.buttonChangeMobileColor.Name = "buttonChangeMobileColor";
            this.buttonChangeMobileColor.Size = new System.Drawing.Size(90, 45);
            this.buttonChangeMobileColor.TabIndex = 9;
            this.buttonChangeMobileColor.Text = "Change Mobile Color";
            this.buttonChangeMobileColor.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonChangeMobileColor.UseVisualStyleBackColor = true;
            // 
            // buttonIncreaseSpeed
            // 
            this.buttonIncreaseSpeed.Location = new System.Drawing.Point(490, 246);
            this.buttonIncreaseSpeed.Name = "buttonIncreaseSpeed";
            this.buttonIncreaseSpeed.Size = new System.Drawing.Size(90, 41);
            this.buttonIncreaseSpeed.TabIndex = 10;
            this.buttonIncreaseSpeed.Text = "Increase Speed";
            this.buttonIncreaseSpeed.UseVisualStyleBackColor = true;
            // 
            // buttonDecreaseSpeed
            // 
            this.buttonDecreaseSpeed.Location = new System.Drawing.Point(490, 293);
            this.buttonDecreaseSpeed.Name = "buttonDecreaseSpeed";
            this.buttonDecreaseSpeed.Size = new System.Drawing.Size(90, 39);
            this.buttonDecreaseSpeed.TabIndex = 11;
            this.buttonDecreaseSpeed.Text = "Decrease Speed";
            this.buttonDecreaseSpeed.UseVisualStyleBackColor = true;
            // 
            // Pong
            // 
            this.ClientSize = new System.Drawing.Size(652, 446);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.buttonDecreaseSpeed);
            this.Controls.Add(this.buttonIncreaseSpeed);
            this.Controls.Add(this.buttonChangeMobileColor);
            this.Controls.Add(this.buttonDeleteElement);
            this.Controls.Add(this.listBox1);
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
                //Invalidate();
        }

        /*Will be call only if there is a click on the windows form region is receive*/
        private void Pong_Click(object sender, EventArgs e)
        {
            if(addWallBool == true) //If we are adding a wall
            {
                var cor = PointToClient(Cursor.Position);
                addWall(new Wall(0, 0, 0, cor.X, cor.Y, 40, 40, 0, 0)); //We add the wall to the list
                System.Threading.Thread.Sleep(2);
                addWallBool = false; //We are not adding a wall because it's the previous step
                System.Diagnostics.Debug.WriteLine("Il y a " + listWall.Count + " murs dans la liste");
                //listBox2.Items.Add(listWall.Count);
                //listBox2.Update();
                //Invalidate();
            }
            if(addCircleBool == true)
            {
                Random rand = new Random(); //Initialize new instance of Random.
                int randWidthHeight = rand.Next(20, 70); //Generate the same aleatory width/height
                addMobile(new Circle(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, randWidthHeight, randWidthHeight, rand.Next(10,80), 2));
                addCircleBool = false;
                listBox1.Items.Add(listMobile.Count);
                listBox1.Update();
            }
            if(addTriangleBool == true)
            {
                Random rand = new Random(); //Initialize new instance of Random.
                int randWidthHeight = rand.Next(20, 120); //Generate the same aleatory width/height
                addMobile(new Triangle(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), MousePosition.X, MousePosition.Y, randWidthHeight, randWidthHeight, rand.Next(10, 90), 0));
                addTriangleBool = false;
                listBox1.Update();
            }
        }

        private void resetClick(object sender, EventArgs e) //reset
        {
            listMobile.Clear();
            listWall.Clear();
            listBox1.Items.Clear();
            listBox1.Update();
            initializeEnvironment();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDeleteElement.Enabled = true;
        }

        private void buttonDeleteElement_Click(object sender, EventArgs e)
        {
            if(listBox1.Items.Count != 0) //If listBox isn't empty
            {
                int tmp = listBox1.SelectedIndex;
                listMobile.RemoveAt(tmp);
                listBox1.Items.RemoveAt(tmp);
                listBox1.Update();
                buttonDeleteElement.Enabled = false;
                buttonDeleteElement.Update();
            }
        }

        /*private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDeleteWall.Enabled = true;
        }

        private void buttonDeleteWall_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count != 0) //If listBox isn't empty
            {
                int tmp = listWall.Count + listBox2.SelectedIndex;
                listWall.RemoveAt(tmp);
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                listBox2.Update();
                buttonDeleteWall.Enabled = false;
                buttonDeleteWall.Update();
            }
        }*/
    }
}
