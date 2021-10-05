using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using BibliotecaDeClases.Interfaces;
namespace BibliotecaDeClases.Cifrados
{
    public class Cesar : IAlgoritmoCifrado
    {
        List<byte> Alphabet_Uppercase = new List<byte>() { 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90 };
        List<byte> Alphabet_Lowercase = new List<byte>() { 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122 };

        List<byte> Alphabet_Disorganized_Uppercase = new List<byte>();
        List<byte> Alphabet_Disorganized_Lowercase = new List<byte>();

        public byte[] DescifrarData(byte[] content, string key)
        {
            SetAlphabets(ConvertToByte(key));

            List<byte> result = new List<byte>(content.Length);
            for (int i = 0; i < content.Length; i++)
            {
                if (Alphabet_Disorganized_Uppercase.Contains(content[i]))
                {
                    int position = Alphabet_Disorganized_Uppercase.IndexOf(content[i]);
                    result.Add(Alphabet_Uppercase[position]);
                }
                else if (Alphabet_Disorganized_Lowercase.Contains(content[i]))
                {
                    int position = Alphabet_Disorganized_Lowercase.IndexOf(content[i]);
                    result.Add(Alphabet_Lowercase[position]);
                }
                else
                {
                    result.Add(content[i]);
                }
            }
            return result.ToArray();
        }

        public byte[] CifrarData(byte[] content, string key)
        {
            SetAlphabets(ConvertToByte(key));

            List<byte> result = new List<byte>(content.Length);
            for (int i = 0; i < content.Length; i++)
            {
                if (Alphabet_Uppercase.Contains(content[i]))
                {
                    int position = Alphabet_Uppercase.IndexOf(content[i]);
                    result.Add(Alphabet_Disorganized_Uppercase[position]);
                }
                else if (Alphabet_Lowercase.Contains(content[i]))
                {
                    int position = Alphabet_Lowercase.IndexOf(content[i]);
                    result.Add(Alphabet_Disorganized_Lowercase[position]);
                }
                else
                {
                    result.Add(content[i]);
                }
            }
            return result.ToArray();
        }

        public void SetAlphabets(byte[] key)
        {
            for (int i = 0; i < key.Length; i++)
            {
                string Upper_letter = Convert.ToString((char)key[i]).ToUpper();
                string Lower_letter = Convert.ToString((char)key[i]).ToLower();

                byte UpperLetter = Convert.ToByte(Convert.ToChar(Upper_letter));
                Alphabet_Disorganized_Uppercase.Add(UpperLetter);

                byte LowerLetter = Convert.ToByte(Convert.ToChar(Lower_letter));
                Alphabet_Disorganized_Lowercase.Add(LowerLetter);
            }
            for (int i = 0; i < Alphabet_Uppercase.Count; i++)
            {
                if (!Alphabet_Disorganized_Uppercase.Contains(Alphabet_Uppercase[i]))
                {
                    Alphabet_Disorganized_Uppercase.Add(Alphabet_Uppercase[i]);
                }
                if (!Alphabet_Disorganized_Lowercase.Contains(Alphabet_Lowercase[i]))
                {
                    Alphabet_Disorganized_Lowercase.Add(Alphabet_Lowercase[i]);
                }
            }
        }
        public static byte[] ConvertToByte(string content)
        {
            byte[] array = new byte[content.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Convert.ToByte(content[i]);
            }
            return array;
        }
    }

    }