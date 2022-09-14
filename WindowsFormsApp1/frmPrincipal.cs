using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.ClasesAuxiliares;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class frmPrincipal : Form
    {
        private Nodo raiz;
        private Arbol arbol;
        Grafico grafico;

        public frmPrincipal()
        {
            GenerarBatch();

            InitializeComponent();
            arbol = new Arbol();
        }

        private void GenerarBatch()
        {
            string filename = $@"{path}\Batch.bat";
            using (StreamWriter writer = File.CreateText(filename))
            {
                writer.WriteLine("@echo off");
                writer.WriteLine($@"cd {path}");
                writer.WriteLine("del Imagen.png");
                writer.WriteLine("dot.exe - Tpng Arbol.dot - o Imagen.png");
            }

            Process ps = new Process();
            ps.StartInfo.FileName = filename;
            // add process to list
            ps.Start();
            ps.Close();
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

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            if (pnlRegistro.Visible)
            {
                pnlRegistro.Visible = false;
                btnMostrar.Text = "Ocultar";
            }
            else
            {
                pnlRegistro.Visible = true;
                btnMostrar.Text = "Mostrar";
            }
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            if (txtExpresion.Text!="")
            {
                arbol.Insertar_EnCola(txtExpresion.Text);
                raiz = arbol.CrearArbol();
                arbol.Limpiar();

                textBox1.Clear();

                textBox1.AppendText($"PreOrden\r\n{arbol.InsertaPre(raiz)}");
                textBox1.AppendText($"\r\nInOrden\r\n{arbol.InsertaIn(raiz)}");
                textBox1.AppendText($"\r\nPostOrden\r\n{arbol.InsertaPost(raiz)}");

                grafico = new Grafico(arbol.nodoDot);
                grafico.DrawTree();
                ShowTree();
            }
            else
            {
                MessageBox.Show("Por favor ingrese una expresion");
            }
        }

        private string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        private void ShowTree()
        {
            if (File.Exists($@"{path}\imagen.png"))
            {
                using (FileStream img = new FileStream($@"{path}\imagen.png", FileMode.Open))
                {
                    pctArbol.Image = Bitmap.FromStream(img);
                }
            }
            else
            {
                MessageBox.Show("No se pudo crear el arbol");
            }
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
