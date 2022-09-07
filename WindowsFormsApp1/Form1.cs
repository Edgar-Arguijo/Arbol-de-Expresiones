using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
   
        int m, mx, my;
        
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            m = 1;
            mx = e.X;
            my = e.Y;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {
            txtMostrar.Visible= false;
            btnOcultar.Visible = false;
            btnMostrar.Visible = true;
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            txtMostrar.Visible = true;
            btnMostrar.Visible = false;
            btnOcultar.Visible = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m == 1)
            {
                this.SetDesktopLocation(MousePosition.X - mx, MousePosition.Y - my);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            m = 0;
        }
    }
}
