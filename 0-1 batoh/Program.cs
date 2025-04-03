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
            int max_weight = int.Parse(Console.ReadLine());

            bool found_something = false;
            
            Batoh(weights, values, max_weight, 0, 0, new List<int>(), 0, ref found_something, new List<int>(), 0);

            Console.Read();
        }

        static void Batoh(int[] weights, int[] values, int max_weight, int current_weight, int index, List<int> values_so_far, int current_sum, ref bool something_found, List<int> best_sequence_so_far, int best_sum_so_far)
        {
            if(current_weight > max_weight)
            {
                return;
            }
            if(current_sum > best_sum_so_far)
            {
                something_found = true;
                best_sequence_so_far = values_so_far;
                best_sum_so_far = current_sum;
                //return;
            }
            for (int i = index; i < weights.Length; i++)
            {
                values_so_far.Add(i + 1);
                current_sum = current_sum + values[i];
                current_weight = current_weight + weights[i];
                Batoh(weights, values, max_weight, current_weight, i /* to je index*/, values_so_far, current_sum, ref something_found, best_sequence_so_far, best_sum_so_far);
                values_so_far.RemoveAt(values_so_far.Count - 1);
                current_sum = current_sum - values[i];
                current_weight = current_weight - weights[i];
            }
        }
    }
}