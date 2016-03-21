using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Pong;
using System.Threading;

namespace pongForm
{
    public partial class Form1 : Form //Herite from Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.White; //Set background Color
            this.Width = 1920; //Set windows width
            this.Height = 1080; //Set window height
            this.Text = "Animation Pong"; //Set windows title

            Thread myThread = new Thread(new ThreadStart(update)); //Create Thread for Pong
            myThread.Start(); //Demarre le thread (methode update)
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Reimplemente methods
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            this.Paint += new PaintEventHandler(form1_paint);
        }

        private void form1_paint(Object sender, PaintEventArgs e)
        {
            //this.Invalidate();
            /*Thread myThread = new Thread(new ThreadStart(update)); //Create Thread for Pong
            myThread.Start(); //Demarre le thread (methode update)*/
        }

        private void update()
        {
            PongT p = new PongT(this); //Create Pong object
            while(Thread.CurrentThread.IsAlive) //While current thread is alive
            {
                p.execute(); //Execute method in Pong
                Thread.Sleep(100); //Pause Thread (100ms)
            }
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            //Not reimplemented yet, will be useful for closing app properly
        }
    }
}
