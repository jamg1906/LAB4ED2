using System;
using BibliotecaDeClases;

namespace Lab4_Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            BibliotecaDeClases.Cifrados.ZigZag ZigZag = new BibliotecaDeClases.Cifrados.ZigZag();

            string texto = "in my head i do everything right when you call i forgive and not" +
               " fight all the moments i play in the dark we were wild and flourescent come " +
               "home to my heart, uh, in your car the radio up, in your car the radio up, we" +
               " keep tryin' to talk about us. Melodrama forever. 2017.";

            string llave = "4";

            Console.WriteLine("Cifrado ZigZag, clave: 4");

            byte[] result_encrypt2 = ZigZag.CifrarData(ConvertToByte(texto), llave);
            Console.WriteLine(ConvertToChar(result_encrypt2));

            byte[] result_decrypt2 = ZigZag.DescifrarData(result_encrypt2, llave);
            Console.WriteLine(ConvertToChar(result_decrypt2));

            Console.ReadKey();
        }

        public static byte[] ConvertToByte(string contenido)
        {
            byte[] arreglo = new byte[contenido.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                arreglo[i] = Convert.ToByte(contenido[i]);
            }
            return arreglo;
        }

        public static char[] ConvertToChar(byte[] contenido)
        {
            char[] arreglo = new char[contenido.Length];
            for (int i = 0; i < arreglo.Length; i++)
            {
                arreglo[i] = Convert.ToChar(contenido[i]);
            }
            return arreglo;
        }
    }
}
