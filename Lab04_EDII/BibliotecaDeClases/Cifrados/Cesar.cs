using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaDeClases.Cifrados
{
    public class Cesar
    {
        private static Dictionary<int, int> DirAlpha = new Dictionary<int, int>();
        private static string routeDirectory = Environment.CurrentDirectory;

        static void CollectDic(string key, int opc)
        {
            DirAlpha = new Dictionary<int, int>();
            key = key.ToUpper();
            var CountOriginal = 65;
            var CountNew = 65;

            if (opc == 1)
            {
                do
                {
                    if (key.Length > 0)
                    {
                        if (!DirAlpha.ContainsValue(key[0]))
                        {
                            DirAlpha.Add(CountOriginal, key[0]);
                            CountOriginal++;
                        }
                        key = key.Substring(1, key.Length - 1);
                    }
                    else
                    {
                        if (!DirAlpha.ContainsValue(CountNew))
                        {
                            DirAlpha.Add(CountOriginal, CountNew);
                            CountOriginal++;
                        }
                        CountNew++;
                    }
                } while (CountOriginal < 91);
            }
            else
            {
                do
                {
                    if (key.Length > 0)
                    {
                        if (!DirAlpha.ContainsKey(key[0]))
                        {
                            DirAlpha.Add(key[0], CountOriginal);
                            CountOriginal++;
                        }
                        key = key.Substring(1, key.Length - 1);
                    }
                    else
                    {
                        if (!DirAlpha.ContainsKey(CountNew))
                        {
                            DirAlpha.Add(CountNew, CountOriginal);
                            CountOriginal++;
                        }
                        CountNew++;
                    }
                } while (CountOriginal < 91);
            }
        }

    }
}
