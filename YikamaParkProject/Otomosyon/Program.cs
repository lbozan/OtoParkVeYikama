using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Otomosyon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
       
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
         //  Application.Run(new acilis());
           Application.Run(new kullanici());
            Application.Run(new Form1());
          
        }
    }
}
