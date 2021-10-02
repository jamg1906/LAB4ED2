using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDeClases.Interfaces
{
    interface IAlgoritmoCifrado
    {

        public byte[] CifrarData(byte[] contenido, string llave);

        public byte[] DescifrarData(byte[] contenido, string llave);

    }
}
