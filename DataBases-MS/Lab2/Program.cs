using System;
using System.Windows.Forms;

namespace Lab1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1()); 
        }
    }
}