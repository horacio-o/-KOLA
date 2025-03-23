using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("vstupy.txt");
            for(string line; (line = sr.ReadLine()) != null;)
            {
                sr.ReadLine();
                if(line == "")
                {
                    Console.WriteLine("prázdná posloupnost");
                    continue;
                }
                NRP(line.Split(' ').Select(int.Parse).ToArray());
            }

            Console.ReadLine();
        }
        static void NRP(int[] input)
        {
            int LM = input.Length;
            int[] V = new int[LM]; // kolik jsme toho zlepšili
            int[] I = new int[LM]; // index předchožího úspěšného čísla
            V[LM-1] = 1;
            I[LM-1] = -1;

            for (int i = LM - 2; i > -1; i--)
            {
                int max = 0;
                int JMS = -1;
                for (int j = i + 1; j < LM; j++)
                {
                    if (input[i] < input[j] && V[j] > max)
                    {
                        max = V[j];
                        JMS = j;
                    }                    
                }
                V[i] = max + 1;
                I[i] = JMS;
            }
            Console.WriteLine(PutTogether(input, V, I));
        }
        static string PutTogether(int[] input, int[] V, int[] I)
        {
            StringBuilder sb = new StringBuilder();
            int LM = input.Length;
            int index = Array.IndexOf(V, V.Max());
            
            while (index != -1)
            {
                sb.Append(input[index] + " ");
                index = I[index];
            }

            return sb.ToString();
        }
    }
}
