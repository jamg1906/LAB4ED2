using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab04_EDII.Models
{
    public class Encryption
    {

        public static void DirectoryCreation()
        {
            string path = Directory.GetCurrentDirectory() + "\\Temp";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Directory.GetCurrentDirectory() + "\\Cifrados";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Directory.GetCurrentDirectory() + "\\Descifrados";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }


        public static string Encrypt(string filePath, string fileName, string method, string key)
        {
            byte[] buffer;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                buffer = new byte[fs.Length];
                using (var br = new BinaryReader(fs))
                {
                    br.Read(buffer, 0, (int)fs.Length);
                }
            }
            string ext = "";
            byte[] content = default;
            string final = "";
            switch (method.Trim())
            {
                case "zigzag":
                    if (Convert.ToInt32(key) < 2)
                    {
                        throw new Exception();
                    }
                    BibliotecaDeClases.Cifrados.ZigZag ZigZag = new BibliotecaDeClases.Cifrados.ZigZag();
                    content = ZigZag.CifrarData(buffer, key);
                    ext = ".zz";
                    final = fileName + ext;
                    break;
                case "cesar":
                    ext = ".csr";
                    final = fileName + ext;
                    break;
                default:
                    throw new Exception();
            }

            string resultado = Directory.GetCurrentDirectory() + "\\Cifrados\\" + fileName + ext;
            using (var fs = new FileStream(resultado, FileMode.OpenOrCreate))
            {
                fs.Write(content, 0, content.Length);
            }
            return final;
        }

        public static string Decrypt(string filePath, string fileName, string method, string key)
        {
            byte[] buffer;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                buffer = new byte[fs.Length];
                using (var br = new BinaryReader(fs))
                {
                    br.Read(buffer, 0, (int)fs.Length);
                }
            }
            string ext = ".txt";
            byte[] content = default;
            string final = "";
            switch (method.Trim())
            {
                case "zigzag":
                    if (Convert.ToInt32(key) < 2)
                    {
                        throw new Exception();
                    }
                    BibliotecaDeClases.Cifrados.ZigZag ZigZag = new BibliotecaDeClases.Cifrados.ZigZag();
                    content = ZigZag.DescifrarData(buffer, key);
                    final = fileName + ext;
                    break;
                case "cesar":
                    final = fileName + ext;
                    break;
                default:
                    throw new Exception();
            }

            string resultado = Directory.GetCurrentDirectory() + "\\Descifrados\\" + fileName + ext;
            using (var fs = new FileStream(resultado, FileMode.OpenOrCreate))
            {
                fs.Write(content, 0, content.Length);
            }
            return final;
        }
    }
}
