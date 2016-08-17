using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Otomosyon
{
    public partial class acilis : Form
    {
        public acilis()
        {
            InitializeComponent();
        }

        private void acilis_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            timer1.Interval = 100;
            timer1.Start();
            this.Opacity = 0;
        }
        public float opicaty = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            opicaty += 0.03f;
            this.Opacity = opicaty;

            if (this.Opacity == 1.0)
            {
                timer1.Stop();
                timer2.Interval = 90;
                timer2.Start();

            } 
        }
        public void sefaf()
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            opicaty -= 0.04f;
            this.Opacity = opicaty;
            if (this.Opacity == 0.0f)
            {

                acilis.ActiveForm.Close();
            
            }


        }
    }
}
