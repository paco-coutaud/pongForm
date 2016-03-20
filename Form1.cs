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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.Width = 500;
            this.Height = 500;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            this.Paint += new PaintEventHandler(form1_paint);
        }

        private void form1_paint(Object sender, PaintEventArgs e)
        {
            Thread myThread = new Thread(new ThreadStart(update));// new Thread(() => Run(0, 42)).Start();
            myThread.Start();
            /*Cercle firstC = new Cercle(this, 255, 0, 0, 10, 10, 20, 20, 1.0, 0);
            firstC.dessine();

            Triangle firstT = new Triangle(this, 0, 255, 0, 100, 70, 40, 20, 0.0, 0);
            firstT.dessine();*/
            /*Graphics graphic = CreateGraphics();
            graphic.DrawRectangle(new Pen(color.getColor()), new Rectangle(20, 20, 100, 30));
            graphic.Dispose();*/
        }

        private void update()
        {
            PongT p = new PongT(this);
            while(Thread.CurrentThread.IsAlive)
            {
                p.execute();
                Thread.Sleep(100);
            }
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            /*Thread.Sleep(3000);
            Thread.CurrentThread.Abort();*/
        }
    }
}
