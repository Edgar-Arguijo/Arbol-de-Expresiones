using System;
using System.Collections;

namespace WindowsFormsApp1.ClasesAuxiliares
{
    public class Arbol
    {
        #region Campos
        //Insercion de la cola
        private string precedencia = "+-*/^";
        private string[] delimitadores = { "+", "-", "*", "/", "^" };
        private string[] operandos/*1234564*/;
        private string[] operadores/*+-*/;
        private Queue colaExpresion;/*2+2+2 = {2}{+}{2}{+}{2}*/

        //Creacion del arbol
        private string token;
        private string operadorTmp;
        private int i = 0;
        private Stack pilaOperadores;
        private Stack pilaOperandos;
        private Stack pilaDot;
        private Nodo raiz = null;

        public Nodo nodoDot { get; set; }

        //Propiedades para recorridos
        public string Pre { get; set; }
        public string In { get; set; }
        public string Post { get; set; }

        #endregion

        #region Constructores
        public Arbol()
        {
            pilaOperadores = new Stack();
            pilaOperandos = new Stack();
            pilaDot = new Stack();
            colaExpresion = new Queue();
        }

        #endregion

        #region Insercion_Cola

        public void Insertar_EnCola(string expresion)/*Expresion = 2+2+5*3/8 */
        {
            //operandos = {2},{2},{5},{3},{8}
            operandos = expresion.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
            //operadores {+},{+},{*},{/}
            operadores = expresion.Split(operandos, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; colaExpresion.Count < operandos.Length + (operadores.Length - 1); i++)
            {
                //Enqueue = Agregar en cola
                colaExpresion.Enqueue(operandos[i]);
                colaExpresion.Enqueue(operadores[i]);
            }
            colaExpresion.Enqueue(operandos[operandos.Length - 1]);
        }
        #endregion

        #region Arbol

        public Nodo CrearArbol()
        {
            while (colaExpresion.Count != 0)
            {
                //{2}{+}{2}{+}{5}{*}{3}{/}{8}
                token = (string)colaExpresion.Dequeue();
                if (precedencia.IndexOf(token) < 0)
                {
                    //Mandamos a operandos
                    pilaOperandos.Push(new Nodo(token));
                    //Pila dot para interpretar e imprimir
                    pilaDot.Push(new Nodo($"nodo{++i}[label=\"{token}\"]"));
                    //Agregar todos los numeros 2,2,5,3,8
                }
                else
                {
                    if (pilaOperadores.Count != 0)
                    {
                        operadorTmp = (string)pilaOperadores.Peek();
                        while (pilaOperadores.Count != 0 && precedencia.IndexOf(operadorTmp) >= precedencia.IndexOf(token))
                        {
                            GuardarSubArbol();
                            if (pilaOperadores.Count != 0)
                            {
                                operadorTmp = (string)pilaOperadores.Peek();
                            }
                        }
                    }
                    pilaOperadores.Push(token);
                }
            }

            raiz = (Nodo)pilaOperandos.Peek();
            nodoDot = (Nodo)pilaDot.Peek();
            while (pilaOperadores.Count != 0)
            {
                GuardarSubArbol();
                raiz = (Nodo)pilaOperandos.Peek();
                nodoDot = (Nodo)pilaDot.Peek();
            }
            return raiz;
        }

        private void GuardarSubArbol()
        {
            /*IZQ{2} - DER{2}*/
            Nodo derecho = (Nodo)pilaOperandos.Pop();
            Nodo izquierdo = (Nodo)pilaOperandos.Pop();
            pilaOperandos.Push(new Nodo(derecho, izquierdo, pilaOperadores.Peek()));

            Nodo derechoG = (Nodo)pilaDot.Pop();
            Nodo izquierdoG = (Nodo)pilaDot.Pop();
            pilaDot.Push(new Nodo(derechoG, izquierdoG, $"nodo{++i}[label=\"{pilaOperadores.Pop()}\"]"));
        }

        #endregion

        #region Recorridos

        public string InsertaPre(Nodo arbol)
        {
            if (arbol != null)
            {
                Pre += arbol.Datos + " ";
                InsertaPre(arbol.NodoIzquierdo);
                InsertaPre(arbol.NodoDerecho);
            }
            return Pre;
        }

        public string InsertaIn(Nodo arbol)
        {
            if (arbol != null)
            {
                InsertaIn(arbol.NodoIzquierdo);
                In += arbol.Datos + " ";
                InsertaIn(arbol.NodoDerecho);
            }
            return In;
        }

        public string InsertaPost(Nodo arbol)
        {
            if (arbol != null)
            {
                InsertaPost(arbol.NodoIzquierdo);
                InsertaPost(arbol.NodoDerecho);
                Post += arbol.Datos + " ";
            }
            return Post;
        }

        #endregion

        public void Limpiar()
        {
            Post = Pre = In = "";
        }
    }
}
