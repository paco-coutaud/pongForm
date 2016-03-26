using System;
using System.Windows.Forms;

namespace Pong
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Pong form = new Pong();
            Application.Run(form);
        }
    }
    

}
