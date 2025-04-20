using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_1_batoh
{
    internal class Program
    {
        static void Main()
        {
            int[] weights = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] values = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int capacity = int.Parse(Console.ReadLine());

            bool[] values_used = new bool[values.Length];
            int[] solution = new int[0];
            int solution_value = 0;

            Console.WriteLine("");

            Batoh(values, weights, capacity, 0, new List<int>(), ref solution, ref solution_value, values_used);

            Console.WriteLine(solution_value);
            Console.WriteLine(string.Join(" ", solution.Select(x => x + 1))); //na tento řádek jsem použil externí pomoc, nevěděl jsem, jak udělat x => x + 1 elegantně :(
        }

        static void Batoh(int[] values, int[] weights, int remaining_capacity, int current_value, List<int> used_values, ref int[] solution, ref int solution_value, bool[] values_used)
        {
            if (remaining_capacity < 0)
            {
                return;
            }

            if (remaining_capacity >= 0 && current_value > solution_value)
            {
                solution = used_values.ToArray();
                solution_value = current_value;
            }

            for (int i = 0; i < values.Length; i++)
            {
                if (values_used[i])
                {
                    continue;
                }

                values_used[i] = true;
                used_values.Add(i);
                Batoh(values, weights, remaining_capacity - weights[i], current_value + values[i], used_values, ref solution, ref solution_value, values_used);
                values_used[i] = false;
                used_values.RemoveAt(used_values.Count - 1);
            }
        }
    }
}