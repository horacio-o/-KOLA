using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins
{
    static class Program
    {
        static void Main()
        {
            int[] coins = Console.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            int target = int.Parse(Console.ReadLine());

            Console.WriteLine("");
            if (target <= 0)
            {
                Console.WriteLine("Nejde to :(");
                return;
            }
            bool that = false;
            Coining(coins, target, new List<int>(), 0, 0, ref that);

            if (!that)
            {
                Console.WriteLine("Nejde to :(");
            }
        }
        static void Coining(int[] coins, int target, List<int> so_far, int sum, int index, ref bool found_something)
        {
            if (sum > target) return;
            if (sum == target)
            {
                Console.WriteLine(string.Join(" ", so_far));
                found_something = true;
                return;
            }
            for (int i = index; i < coins.Length; i++)
            {
                so_far.Add(coins[i]);
                Coining(coins, target, so_far, sum + coins[i], i, ref found_something);
                so_far.RemoveAt(so_far.Count - 1);
            }
        }
    }
}