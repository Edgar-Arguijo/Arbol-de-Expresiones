using System;

namespace WindowsFormsApp1.ClasesAuxiliares
{
    public class Nodo
    {
        #region Campos

        //Contenido del nodo
        private Object datos { get; set; }
        //Nodo de la izquierda
        private Nodo Izquierdo { get; set; }
        //Nodo de la derecha
        private Nodo Derecho { get; set; }

        #endregion

        #region Constructores

        public Nodo()
        {
            Izquierdo = Derecho = null;
        }
        public Nodo(object Datos)
        {
            this.datos = Datos;
            Izquierdo = Derecho = null;
        }
        public Nodo(Nodo der, Nodo izq, Object datos)
        {
            Derecho = der;
            Izquierdo = izq;
            this.datos = datos;
        }
        #endregion

        #region Propiedades

        //Izq
        public Nodo NodoIzquierdo { get => Izquierdo; }
        //Derecho
        public Nodo NodoDerecho { get => Derecho; }

        public Object Datos { get => datos; }


        #endregion

    }
}
