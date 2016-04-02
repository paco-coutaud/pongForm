/****************************
****AUTHOR : Paco COUTAUD****
****AUTHOR : Gauthier CASTRO*
**LAST CHANGES : 02/03/2016**
****************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pong
{
    public partial class Pong : Form //inherite from Form (Windows library)
    {
        private List<Mobile> listMobile; //Contains mobile elements
        private List<Wall> listWall; //Contains wall elements
        private bool running; //Useful to know if the animation is lauch
        private bool addWallBool; //Useful to know if we are adding a wall
        private bool addCircleBool; //Useful to know if we are adding a circle
        private bool addTriangleBool; //Useful to know if we are adding a triangle
        private bool pauseAnimation; //Useful to know if the animation is paused
        private Random randNumber; //Useful to generate pseudo-aleatory numbers

        public Pong(String title, int windowsWidth, int windowsHeight)
        {
            InitializeComponent(); //Initialize all kind of graphics components
            initWindowsProperties(title, windowsWidth, windowsHeight); //Init windows properties
            initStateVariable(); //Init states variable
            initListAttributes(); //Init list attributes variable
            initOtherAttributes(); //Init other kinds of variable
            initializeEnvironment(); //Init environment

            //Timer useful to refresh forms
            Timer s = new Timer();
            s.Interval = 8; //All the 8 ms
            s.Tick += new EventHandler(rf); //When the timer finish to count, connect to rf function, and reset it
            s.Start(); //Start to count
        }

        private void initWindowsProperties(String title, int windowsWidth, int windowsHeight)
        {
            //Set graphics properties
            this.BackColor = Color.White; //Set background's Color to white
            this.Width = windowsWidth; //Set windows width
            this.Height = windowsHeight; //Set window height
            this.Text = title; //Set windows title

            //Disable all the buttons
            this.buttonDeleteElement.Enabled = false;
            this.buttonChangeMobileColor.Enabled = false;
            this.buttonDecreaseSpeed.Enabled = false;
            this.buttonIncreaseSpeed.Enabled = false;

            this.KeyPreview = true; //useful to redirect correctly keyBoard events

            //Enable double buffering to disable flickering, very important
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void initStateVariable()
        {
            //Initialize all boolean variable to false for the beggining
            addWallBool = false;
            running = false;
            addTriangleBool = false;
            addCircleBool = false;
            pauseAnimation = false;
        }

        private void initListAttributes()
        {
            //Initialize list attributes
            listMobile = new List<Mobile>();
            listWall = new List<Wall>();
        }

        private void initOtherAttributes()
        {
            //New instance of randNumber, useful to generate pseudo aleatory numbers
            randNumber = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Connect events
            this.KeyDown += new KeyEventHandler(keyPressed); //Key Down (a key on a keybord is down), connect to keyPressed event
            this.Paint += new PaintEventHandler(form1_paint); //When the windows is paint, this event is call
        }
        
        private void keyPressed(Object sender, KeyEventArgs e) //If a key is pressed
        {
            //If key pressed is P && animation not paused
            if(e.KeyCode == Keys.P && pauseAnimation != true)
            {
                pauseAnimation = true; //We pause the animation
                e.Handled = true; //We say that the event is treated
            }
            else if(e.KeyCode == Keys.P) //Else, the animation is already in pause
            {
                pauseAnimation = false; //So, we unpause the animation
                e.Handled = true;
            }
        }

        private void rf(Object sender, EventArgs e)
        {
            Invalidate(); //Useful to force to redraw in the form, form1_paint is call because it send paint signal
        }
        private void form1_paint(Object sender, PaintEventArgs e) //Call when Invalidate() is call
        {
            if (running == true) //Simulate loop, if the animation is started
            {
                Collision(); //Check collision
                execute(e.Graphics); //Execute (move and draw)

                if (addWallBool == true) //If we are adding a wall
                {
                    showPositionWall(e.Graphics); //Show the wall we are adding on screen
                }
                if (addCircleBool == true) //If we are adding a circle
                {
                    showPositionCircle(e.Graphics);
                }
                if (addTriangleBool == true) //If we are adding a triangle
                {
                    showPositionTriangle(e.Graphics);
                }

            }
        }

        private void initMobiles()
        {
            //Some mobiles to begin
            addMobile(new Circle(255, 0, 0, randNumber.Next(100, 300), randNumber.Next(100, 250), 50, 50, 45, randNumber.Next(2, 5))); //Red circle
            addMobile(new Triangle(0, 255, 0, randNumber.Next(100, 300), randNumber.Next(300, 450), 30, 30, 60, randNumber.Next(2, 5))); //Green circle

            //You can add other mobiles here or remove existing mobiles
        }
        private void initWalls() //Adding walls
        {
            addWall(new Wall(0, 0, 0, 20, 30, 20, 430, 80,1));
            addWall(new Wall(0, 0, 0, 20, 30, 500, 20, 80, 1));
            addWall(new Wall(0, 0, 0, 20, 530, 20, 450, 75, 1));
            addWall(new Wall(0, 0, 0, 450, 30, 500, 20, 75, 1));

            //You can add some walls here or remove existing walls
        }
        private void initializeEnvironment()
        {
            initWalls(); //Initialize walls
            initMobiles(); //Initialize mobiles
        }

        private void execute(Graphics e)
        {
            foreach (Wall element in listWall) //We draw each walls in the wall lists
            {
                element.draw(e); //Draw walls
            }

            foreach (Mobile element in listMobile)
            {
                if(pauseAnimation == false) //If the animation is in pause we don't move the mobiles
                {
                    element.move();
                }

                element.draw(e); //Draw mobiles
            }
        }

        private void Collision()
        {
            for (int i = 0; i < listMobile.Count; i++)
            {
                //To check collision with walls
                for (int j = 0; j < listWall.Count; j++)
                {
                    if (listMobile.ElementAt(i)._x + listMobile.ElementAt(i)._width >= listWall.ElementAt(j)._x
                         && listMobile.ElementAt(i)._x < listWall.ElementAt(j)._x + listWall.ElementAt(j)._width
                         && listMobile.ElementAt(i)._y + listMobile.ElementAt(i)._height > listWall.ElementAt(j)._y
                         && listMobile.ElementAt(i)._y < listWall.ElementAt(j)._y + listWall.ElementAt(j)._height)
                    {
                        listMobile.ElementAt(i)._orientation += listWall.ElementAt(j)._orientation * listWall.ElementAt(j)._coefficient;
                    }
                }

                //To check collision mobiles-mobiles
                for (int h = 0; h < listMobile.Count; h++)
                {
                    if (i != h)
                    {
                        if (listMobile.ElementAt(i)._x + listMobile.ElementAt(i)._width >= listMobile.ElementAt(h)._x
                            && listMobile.ElementAt(i)._x < listMobile.ElementAt(h)._x + listMobile.ElementAt(h)._width
                            && listMobile.ElementAt(i)._y + listMobile.ElementAt(i)._height > listMobile.ElementAt(h)._y
                            && listMobile.ElementAt(i)._y < listMobile.ElementAt(h)._y + listMobile.ElementAt(h)._height)
                        {
                            listMobile.ElementAt(i)._orientation += randNumber.Next(55, 80);
                            listMobile.ElementAt(h)._orientation += randNumber.Next(55, 80);
                        }
                    }
                }
            }
        }

        /*This method provide a simple way to add mobile element into the list
        It take one argument : m is the mobile to add*/
        private void addMobile(Mobile m)
        {
            listMobile.Add(m);
            updateItemsBox(); //Updates the box where we can see all the objects id
        }

        /*This method provide a simple way to add wall element into the list
        It take one argument : w is the wall to add*/
        private void addWall(Wall w)
        {
            listWall.Add(w);
        }

        /*This method provide a simple way to preview a wall which will be add to the wall list after
        It permits to show the position where the wall will be added
        It take one argument : e is the graphic element where the wall will be draw*/
        private void showPositionWall(Graphics e)
        {
            Wall.preview(e, PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y); //Static method to preview a wall objecton the screen
        }

        private void showPositionCircle(Graphics e)
        {
            Circle.preview(e, PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y); //Static method to preview a circle object on the screen
        }

        private void showPositionTriangle(Graphics e)
        {
            Triangle.preview(e, PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y); //Static method to preview a triangle object on the screen
        }

        /*Initialize all kind of graphics components*/
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cercleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.murToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.cercleToolStripMenuItem,
            this.triangleToolStripMenuItem,
            this.murToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 70);
            // 
            // cercleToolStripMenuItem
            // 
            this.cercleToolStripMenuItem.Name = "cercleToolStripMenuItem";
            this.cercleToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.cercleToolStripMenuItem.Text = "Cercle";
            this.cercleToolStripMenuItem.Click += new System.EventHandler(this.addCircleClick);
            // 
            // triangleToolStripMenuItem
            // 
            this.triangleToolStripMenuItem.Name = "triangleToolStripMenuItem";
            this.triangleToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.triangleToolStripMenuItem.Text = "Triangle";
            this.triangleToolStripMenuItem.Click += new System.EventHandler(this.addTriangleClick);
            // 
            // murToolStripMenuItem
            // 
            this.murToolStripMenuItem.Name = "murToolStripMenuItem";
            this.murToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.murToolStripMenuItem.Text = "Mur";
            this.murToolStripMenuItem.Click += new System.EventHandler(this.addWallClick);
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
            this.startToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startToolStripMenuItem.Text = "Démarrer";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.actionStartClick);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.resetToolStripMenuItem.Text = "Reinitialiser";
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
            this.buttonDeleteElement.Text = "Supprimer le mobile";
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
            this.buttonChangeMobileColor.Text = "Changer la couleur";
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
            this.buttonIncreaseSpeed.Text = "Augmenter la vitesse";
            this.buttonIncreaseSpeed.UseVisualStyleBackColor = true;
            this.buttonIncreaseSpeed.Click += new System.EventHandler(this.buttonIncreaseSpeed_Click);
            // 
            // buttonDecreaseSpeed
            // 
            this.buttonDecreaseSpeed.Location = new System.Drawing.Point(490, 293);
            this.buttonDecreaseSpeed.Name = "buttonDecreaseSpeed";
            this.buttonDecreaseSpeed.Size = new System.Drawing.Size(90, 39);
            this.buttonDecreaseSpeed.TabIndex = 11;
            this.buttonDecreaseSpeed.Text = "Diminuer la vitesse";
            this.buttonDecreaseSpeed.UseVisualStyleBackColor = true;
            this.buttonDecreaseSpeed.Click += new System.EventHandler(this.buttonDecreaseSpeed_Click);
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
            if (startToolStripMenuItem.Text == "Démarrer") //If we click on start button
            {
                startToolStripMenuItem.Text = "Arreter"; //Change text on "Stop"
                running = true; //Set to true the running boolean variable
            }
            else //We click on stop button
            {
                startToolStripMenuItem.Text = "Démarrer"; //Change text on "Start"
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
            listBox1.Items.Add(listMobile.Count); //Add in the listBox new id
            listBox1.Update(); //Update the box
        }

        /*Will be call only if there is a click on the windows form region is receive*/
        private void Pong_Click(object sender, EventArgs e)
        {
            if (addWallBool == true) //If we are adding a wall
            {
                addWall(new Wall(0, 0, 0, PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, 40, 40, randNumber.Next(55,80), 0.5)); //We add the wall to the list
                addWallBool = false; //We are not adding a wall because it's the previous step
            }
            if (addCircleBool == true)
            {
                int randWidthHeight = randNumber.Next(25, 55); //Generate the same aleatory width/height
                int randOrientation = randNumber.Next(25, 75);
                addMobile(new Circle(randNumber.Next(0, 255), randNumber.Next(0, 255), randNumber.Next(0, 255), PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, randWidthHeight, randWidthHeight, randOrientation, randNumber.Next(2, 4)));
                addCircleBool = false;
            }
            if (addTriangleBool == true)
            {
                int randWidthHeight = randNumber.Next(25, 55); //Generate the same aleatory width/height
                int randOrientation = randNumber.Next(25, 75);
                addMobile(new Triangle(randNumber.Next(0, 255), randNumber.Next(0, 255), randNumber.Next(0, 255), PointToClient(Cursor.Position).X, PointToClient(Cursor.Position).Y, randWidthHeight, randWidthHeight, randOrientation, randNumber.Next(2, 4)));
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) //Call if the user select an other item in the listBox
        {
            buttonChangeMobileColor.Enabled = true;
            buttonDeleteElement.Enabled = true;
            checkIfIncreaseOrDecreaseAvailable();
            
        }

        private void buttonDeleteElement_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0) //If listBox isn't empty
            {
                int tmp = listBox1.SelectedIndex;
                listMobile.RemoveAt(tmp);
                listBox1.Items.RemoveAt(tmp);
                listBox1.Update();
                buttonDeleteElement.Enabled = false;
                buttonDeleteElement.Update();
                buttonChangeMobileColor.Enabled = false;
                buttonDecreaseSpeed.Enabled = false;
                buttonIncreaseSpeed.Enabled = false;
                buttonIncreaseSpeed.Update();
                buttonDecreaseSpeed.Update();
                buttonChangeMobileColor.Update();
            }
        }

        private void buttonChangeMobileColor_Click(object sender, EventArgs e) //Call if click on change mobile color button receive
        {
            //Change mobile color with aleatory color
            listMobile.ElementAt(listBox1.SelectedIndex)._color.setColor(randNumber.Next(0, 255), randNumber.Next(0, 255), randNumber.Next(0, 255));
        }

        private void buttonIncreaseSpeed_Click(object sender, EventArgs e)
        {
            listMobile.ElementAt(listBox1.SelectedIndex)._speed += 0.2;

            checkIfIncreaseOrDecreaseAvailable();
        }

        private void checkIfIncreaseOrDecreaseAvailable() //This method set the limits of the mobile's speed
        {
            if(listBox1.SelectedIndex != -1)
            {
                if (listMobile.ElementAt(listBox1.SelectedIndex)._speed >= 4)
                {
                    buttonIncreaseSpeed.Enabled = false;
                }
                else
                {
                    buttonIncreaseSpeed.Enabled = true;
                }

                if (listMobile.ElementAt(listBox1.SelectedIndex)._speed <= 2)
                {
                    buttonDecreaseSpeed.Enabled = false;
                }
                else
                {
                    buttonDecreaseSpeed.Enabled = true;
                }
            }
        }

        private void buttonDecreaseSpeed_Click(object sender, EventArgs e)
        {
            listMobile.ElementAt(listBox1.SelectedIndex)._speed -= 0.2;

            checkIfIncreaseOrDecreaseAvailable();
        }
    }
}
