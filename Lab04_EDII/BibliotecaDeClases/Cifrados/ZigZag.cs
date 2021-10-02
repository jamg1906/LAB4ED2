using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDeClases.Cifrados
{
    public class ZigZag : Interfaces.IAlgoritmoCifrado
    {

        List<List<byte>> Matriz = new List<List<byte>>();

        public byte[] CifrarData(byte[] contenido, string llave)
        {
            int longitud = Convert.ToInt32(llave);
            EstablecerMatriz(longitud);

            int pos = 0;
            while (pos < contenido.Length)
            {
                for (int i = 1; i < longitud; i++)
                {
                    if (pos == 0)
                    {
                        Matriz[i - 1].Add(contenido[pos]);
                        Matriz[i].Add(contenido[pos + 1]);
                        pos += 2;
                    }
                    else
                    {
                        if (pos > contenido.Length - 1)
                        {
                            break;
                        }
                        Matriz[i].Add(contenido[pos]);
                        pos++;
                    }
                }
                for (int i = longitud - 2; i >= 0; i--)
                {
                    if (pos > contenido.Length - 1) break;
                    Matriz[i].Add(contenido[pos]);
                    pos++;
                }
            }

            List<byte> resultado = new List<byte>();
            for (int i = 0; i < Matriz.Count; i++)
            {
                List<byte> listaActual = Matriz[i];
                for (int j = 0; j < listaActual.Count; j++)
                {
                    resultado.Add(listaActual[j]);
                }
            }
            return resultado.ToArray();
        }

        void EstablecerMatriz(int lineas)
        {
            for (int i = 0; i < lineas; i++)
            {
                Matriz.Add(new List<byte>());
            }
        }




        public byte[] DescifrarData(byte[] contenido, string llave)
        {
            int longitud = Convert.ToInt32(llave);
            Matriz = new List<List<byte>>();
            EstablecerMatriz(longitud);

            int pos = 0;
            while (pos < contenido.Length)
            {
                for (int i = 1; i < longitud; i++)
                {
                    if (pos == 0)
                    {
                        Matriz[i - 1].Add(224);
                        Matriz[i].Add(224);
                        pos += 2;
                    }
                    else
                    {
                        if (pos > contenido.Length - 1) break;
                        Matriz[i].Add(224);
                        pos++;
                    }
                }
                for (int i = longitud - 2; i >= 0; i--)
                {
                    if (pos > contenido.Length - 1) break;
                    Matriz[i].Add(224);
                    pos++;
                }
            }

            int acumulador = 0;
            for (int i = 0; i < longitud; i++)
            {
                for (int j = 0; j < Matriz[i].Count; j++)
                {
                    Matriz[i][j] = contenido[acumulador + j];
                }
                acumulador += Matriz[i].Count;
            }

            List<byte> resultado = new List<byte>();
            try
            {

                List<byte> primeraLinea = new List<byte>();
                primeraLinea.Add(Matriz[0][0]);
                for (int i = 1; i < Matriz[0].Count; i++)
                {
                    primeraLinea.Add(Matriz[0][i]);
                    primeraLinea.Add(244);
                }
                Matriz[0] = primeraLinea;

                List<byte> ultimaLinea = new List<byte>();
                ultimaLinea.Add(Matriz[longitud - 1][0]);
                for (int i = 1; i < Matriz[longitud - 1].Count; i++)
                {
                    ultimaLinea.Add(244);
                    ultimaLinea.Add(Matriz[longitud - 1][i]);
                }
                Matriz[longitud - 1] = ultimaLinea;

                for (int i = 0; i < longitud; i++)
                {
                    resultado.Add(Matriz[i][0]);
                }

                int longMax = Obtenermax();
                for (int i = 1; i < longMax; i++)
                {
                    if (i % 2 != 0)
                    {
                        for (int j = longitud - 2; j >= 0; j--)
                        {
                            resultado.Add(Matriz[j][i]);
                        }
                    }
                    if (i % 2 == 0)
                    {
                        for (int j = 1; j < longitud; j++)
                        {
                            resultado.Add(Matriz[j][i]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return resultado.ToArray();
            }
            return resultado.ToArray();
        }

        public int Obtenermax()
        {
            int max = Matriz[0].Count;
            for (int i = 0; i < Matriz.Count; i++)
            {
                if (Matriz[i].Count > max) max = Matriz[i].Count;
            }
            return max;
        }

    }
}
