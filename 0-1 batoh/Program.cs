using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_1_batoh
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] weights = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int[] values = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int max_value = int.Parse(Console.ReadLine());
        }

        static void Batoh(int[] weights, int[] values, int max_value, int current_sum, int index, List<int> values_so_far, ref bool something_found)
        {
            if(current_sum > max_value)
            {
                return;
            }
        }
    }
}
