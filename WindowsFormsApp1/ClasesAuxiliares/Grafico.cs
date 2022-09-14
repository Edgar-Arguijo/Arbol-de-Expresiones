using System;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApp1.ClasesAuxiliares
{
    public class Grafico
    {
        #region Campos

        Nodo arbol;
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private string command = @"/c Batch.bat";
        private int i, j;

        #endregion

        #region Constructores

        public Grafico(Nodo arb)
        {
            this.arbol = arb;
        }

        #endregion

        #region Funciones

        public void DrawTree()
        {
            CreateFileDot();
            ExecuteDot();
        }

        private void ExecuteDot()
        {
            Directory.SetCurrentDirectory(path);
            using (Process prc = new Process())
            {
                ProcessStartInfo Info = new ProcessStartInfo("cmd", command);
                Info.CreateNoWindow = true;
                prc.StartInfo = Info;
                prc.Start();
                prc.WaitForExit();
                prc.Close();
            }
        }

        private string CreateFileDot()
        {
            string cadenaDot = "";
            StartFileDot(arbol, ref cadenaDot);
            using (StreamWriter archivo = new StreamWriter(path + @"\Arbol.dot"))
            {
                archivo.WriteLine(cadenaDot);
                archivo.Close();
            }
            return cadenaDot;
        }

        private void StartFileDot(Nodo arbol, ref string cadenaDot)
        {
            if (arbol != null)
            {
                cadenaDot += "digraph Grafico {\nnode [style=bold, fillcolor=gray];\n";
                Recorrido(arbol, ref cadenaDot);
                cadenaDot += "\n}";
            }
        }

        private void Recorrido(Nodo arbol, ref string cadenaDot)
        {
            if (arbol != null)
            {
                cadenaDot += $"{arbol.Datos}\n";
                if (arbol.NodoIzquierdo != null)
                {
                    i = arbol.Datos.ToString().IndexOf("[");
                    j = arbol.NodoIzquierdo.Datos.ToString().IndexOf("[");
                    cadenaDot += $"{arbol.Datos.ToString().Remove(i)}->{arbol.NodoIzquierdo.Datos.ToString().Remove(j)};\n";
                    
                }

                if (arbol.NodoDerecho != null)
                {
                    i = arbol.Datos.ToString().IndexOf("[");
                    j = arbol.NodoDerecho.Datos.ToString().IndexOf("[");
                    cadenaDot += $"{arbol.Datos.ToString().Remove(i)}->{arbol.NodoDerecho.Datos.ToString().Remove(j)};\n";
                }
                Recorrido(arbol.NodoIzquierdo, ref cadenaDot);
                Recorrido(arbol.NodoDerecho, ref cadenaDot);
            }
        }

        #endregion
    }
}
