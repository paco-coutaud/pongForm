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
        private bool addCircleBool; //Useful to know if we are adding a circle
        private bool addTriangleBool; //Useful to know if we are adding a triangle
        private Random randNumber; //Useful to generate pseudo-aleatory numbers

        public Pong(String title, int windowsWidth, int windowsHeight)
        {
            InitializeComponent(); //Initialize all kind of graphics components
            initWindowsProperties(title,windowsWidth,windowsHeight); //Init 
            initStateVariable();
            initListAttributes();
            initOtherAttributes();
            initializeEnvironment();

            //Timer useful to refresh forms
            Timer s = new Timer();
            s.Interval = 8;
            s.Tick += new EventHandler(rf);
            s.Start();
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

        private void initOtherAttributes()
        {
            randNumber = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Reimplemente methods
            this.Paint += new PaintEventHandler(form1_paint);
        }

        private void rf(Object sender, EventArgs e)
        {
            Invalidate();
        }
        private void form1_paint(Object sender, PaintEventArgs e) //Call when Invalidate()
        {
            if (running == true) //Simulate loop
            {
                Collision();
                execute(e.Graphics);
                
                if (addWallBool == true)
                {
                    showPositionWall(e.Graphics); //Show the wall we are adding on screen
                }
                if(addCircleBool == true)
                {
                    showPositionCircle(e.Graphics);
                }
                if(addTriangleBool == true)
                {
                    showPositionTriangle(e.Graphics);
                }

            }
        }

        private void initMobiles()
        {
            //Some mobiles to begin
            addMobile(new Circle(255, 0, 0, randNumber.Next(100,300), randNumber.Next(100, 300), 50, 50, 45, randNumber.Next(2,5))); //Red circle
            addMobile(new Triangle(0, 255, 0, randNumber.Next(100, 300), randNumber.Next(100, 300), 30, 30, 60, randNumber.Next(2, 5))); //Green circle
        }
        private void initWalls() //Adding walls
        {
            addWall(new Wall(0, 0, 0, 20, 30, 20, 430, 0, 0));
            addWall(new Wall(0, 0, 0, 20, 30, 500, 20, 0, 0));
            addWall(new Wall(0, 0, 0, 20, 530, 20, 450, 0, 0));
            addWall(new Wall(0, 0, 0, 450, 30, 500, 20, 0, 0));
        }
        public void initializeEnvironment()
        {
            initWalls(); //Initialize walls
            initMobiles(); //Initialize mobiles
        }

        public void execute(Graphics e)
        {
            foreach (Wall element in listWall)
            {
                element.draw(e); //Draw walls
            }

            foreach (Mobile element in listMobile)
            {
                element.move();
                element.draw(e); //Draw mobiles
            }
        }

        public void Collision()
        {
                for(int i=0; i<listMobile.Count;i++)
                {
                    //To check collision with walls
                    for (int j = 0; j < listWall.Count; j++)
                    {
                       if (listMobile.ElementAt(i)._x + listMobile.ElementAt(i)._largeur >= listWall.ElementAt(j)._x
                            && listMobile.ElementAt(i)._x < listWall.ElementAt(j)._x + listWall.ElementAt(j)._largeur // trop à gauche
                            && listMobile.ElementAt(i)._y + listMobile.ElementAt(i)._hauteur > listWall.ElementAt(j)._y
                            && listMobile.ElementAt(i)._y < listWall.ElementAt(j)._y + listWall.ElementAt(j)._hauteur)
                            {
                                listMobile.ElementAt(i)._orientation += randNumber.Next(45,90);
                            }
                    }

                    //To check collision mobiles-mobiles
                    for(int h = 0; h<listMobile.Count; h++)
                    {
                        if(i != h)
                        {
                            if (listMobile.ElementAt(i)._x + listMobile.ElementAt(i)._largeur >= listMobile.ElementAt(h)._x
                                && listMobile.ElementAt(i)._x < listMobile.ElementAt(h)._x + listMobile.ElementAt(h)._largeur // trop à gauche
                                && listMobile.ElementAt(i)._y + listMobile.ElementAt(i)._largeur > listMobile.ElementAt(h)._y
                                && listMobile.ElementAt(i)._y < listMobile.ElementAt(h)._y + listMobile.ElementAt(h)._hauteur)
                            {
                                listMobile.ElementAt(i)._orientation += randNumber.Next(45, 90);
                                listMobile.ElementAt(h)._orientation += randNumber.Next(45, 90);
                            }
                        }
                    }   
                }
            }

        /*This method provide a simple way to add mobile element into the list
        It take one argument : m is the mobile to add*/
        public void addMobile(Mobile m)
        {
            listMobile.Add(m);
            updateItemsBox();
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
            SolidBrush n = new SolidBrush(Color.Black); //Wall has black color
            e.FillRectangle(n, PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, 40, 40); //Draw a wall on the windows forms surface
            n.Dispose(); //Free the ressource
        }

        public void showPositionCircle(Graphics e)
        {
            Circle.preview(e, PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y);
        }

        public void showPositionTriangle(Graphics e)
        {
            Triangle.preview(e, PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y);
        }

        /*Initialize all kind of graphics components*/
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.cercleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.murToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.globalMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cercleToolStripMenuItem,
            this.triangleToolStripMenuItem,
            this.murToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 92);
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
            this.buttonChangeMobileColor.Click += new System.EventHandler(this.buttonChangeMobileColor_Click);
            // 
            // buttonIncreaseSpeed
            // 
            this.buttonIncreaseSpeed.Location = new System.Drawing.Point(490, 246);
            this.buttonIncreaseSpeed.Name = "buttonIncreaseSpeed";
            this.buttonIncreaseSpeed.Size = new System.Drawing.Size(90, 41);
            this.buttonIncreaseSpeed.TabIndex = 10;
            this.buttonIncreaseSpeed.Text = "Increase Speed";
            this.buttonIncreaseSpeed.UseVisualStyleBackColor = true;
            this.buttonIncreaseSpeed.Click += new System.EventHandler(this.buttonIncreaseSpeed_Click);
            // 
            // buttonDecreaseSpeed
            // 
            this.buttonDecreaseSpeed.Location = new System.Drawing.Point(490, 293);
            this.buttonDecreaseSpeed.Name = "buttonDecreaseSpeed";
            this.buttonDecreaseSpeed.Size = new System.Drawing.Size(90, 39);
            this.buttonDecreaseSpeed.TabIndex = 11;
            this.buttonDecreaseSpeed.Text = "Decrease Speed";
            this.buttonDecreaseSpeed.UseVisualStyleBackColor = true;
            this.buttonDecreaseSpeed.Click += new System.EventHandler(this.buttonDecreaseSpeed_Click);
            // 
            // cercleToolStripMenuItem
            // 
            this.cercleToolStripMenuItem.Name = "cercleToolStripMenuItem";
            this.cercleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cercleToolStripMenuItem.Text = "Cercle";
            this.cercleToolStripMenuItem.Click += new System.EventHandler(this.addCircleClick);
            // 
            // triangleToolStripMenuItem
            // 
            this.triangleToolStripMenuItem.Name = "triangleToolStripMenuItem";
            this.triangleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.triangleToolStripMenuItem.Text = "Triangle";
            this.triangleToolStripMenuItem.Click += new System.EventHandler(this.addTriangleClick);
            // 
            // murToolStripMenuItem
            // 
            this.murToolStripMenuItem.Name = "murToolStripMenuItem";
            this.murToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.murToolStripMenuItem.Text = "Mur";
            this.murToolStripMenuItem.Click += new System.EventHandler(this.addWallClick);
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
        }

        private void updateItemsBox()
        {
            listBox1.Items.Add(listMobile.Count);
            listBox1.Update();
        }

        /*Will be call only if there is a click on the windows form region is receive*/
        private void Pong_Click(object sender, EventArgs e)
        {
            if(addWallBool == true) //If we are adding a wall
            {
                addWall(new Wall(0, 0, 0, PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, 40, 40, 0, 0)); //We add the wall to the list
                addWallBool = false; //We are not adding a wall because it's the previous step
            }
            if(addCircleBool == true)
            {
                int randWidthHeight = randNumber.Next(25, 55); //Generate the same aleatory width/height
                int randOrientation = randNumber.Next(25, 75);
                addMobile(new Circle(randNumber.Next(0, 255), randNumber.Next(0, 255), randNumber.Next(0, 255), PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, randWidthHeight, randWidthHeight, randOrientation, randNumber.Next(2,4)));
                addCircleBool = false;
            }
            if(addTriangleBool == true)
            {
                int randWidthHeight = randNumber.Next(25,55); //Generate the same aleatory width/height
                int randOrientation = randNumber.Next(25, 75);
                addMobile(new Triangle(randNumber.Next(0, 255), randNumber.Next(0, 255), randNumber.Next(0, 255), PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, randWidthHeight, randWidthHeight, randOrientation, randNumber.Next(2,4)));
                addTriangleBool = false;
            }
        }

        private void resetClick(object sender, EventArgs e) //reset
        {
            listMobile.Clear(); //Clear mobiles list
            listWall.Clear(); //Clear walls list
            listBox1.Items.Clear(); //Clear box;
            listBox1.Update(); //Update box
            initializeEnvironment(); //Initialize environment
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonDeleteElement.Enabled = true;
            buttonChangeMobileColor.Enabled = true;
            checkIfIncreaseOrDecreaseAvailable();
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
                buttonChangeMobileColor.Enabled = false;
                buttonChangeMobileColor.Update();
                buttonDecreaseSpeed.Enabled = false;
                buttonDecreaseSpeed.Update();
                buttonIncreaseSpeed.Enabled = false;
                buttonIncreaseSpeed.Update();
            }
        }

        private void buttonChangeMobileColor_Click(object sender, EventArgs e)
        {
            listMobile.ElementAt(listBox1.SelectedIndex)._color.setColor(randNumber.Next(0, 255), randNumber.Next(0, 255), randNumber.Next(0, 255));
        }

        private void buttonIncreaseSpeed_Click(object sender, EventArgs e)
        {
            listMobile.ElementAt(listBox1.SelectedIndex)._vitesse += 0.2;

            checkIfIncreaseOrDecreaseAvailable();
        }

        private void checkIfIncreaseOrDecreaseAvailable()
        {
            if (listMobile.ElementAt(listBox1.SelectedIndex)._vitesse >= 4)
            {
                buttonIncreaseSpeed.Enabled = false;
            }
            else
            {
                buttonIncreaseSpeed.Enabled = true;
            }

            if (listMobile.ElementAt(listBox1.SelectedIndex)._vitesse <= 2)
            {
                buttonDecreaseSpeed.Enabled = false;
            }
            else
            {
                buttonDecreaseSpeed.Enabled = true;
            }
        }

        private void buttonDecreaseSpeed_Click(object sender, EventArgs e)
        {
            listMobile.ElementAt(listBox1.SelectedIndex)._vitesse -= 0.2;

            checkIfIncreaseOrDecreaseAvailable();
        }
    }
}
