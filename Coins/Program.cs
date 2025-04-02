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
            //array intů – mince, které jsou na výběr
            //int target – cíl

            Console.WriteLine("");
            //prostě a jednoduše whitespace

            if (target <= 0)
            {
                Console.WriteLine("Nejde to :(");
                return;
            }
            //když je target nula nebo je záporný, nebudeme ho počítat

            bool that = false;
            //bool, který prostě jenom říká, jestli se našla kombinace mincí, která funguje

            Coining(coins, target, new List<int>(), 0, 0, ref that);

            if (!that)
            {
                Console.WriteLine("Nejde to :(");
            }
        }

        //coins                   – mince, které má metoda na výběr
        //target                  – target
        //so_far                  – list dosavadních nalezlých mincí, které můžeme použít např. Kdyby byl target 6 a coins 1 a 2 a my bychom vyhodnotili jenom první dvě možné mince, tak by so_far mohl vypadat takhle: so_far[0] = 2, so_far[1] = 2
        //sum                     – dosavadní součet mincí, které jsme nalezli vlastně to je součet všech prvků v so_far
        //index                   – coins je array integerů. Hodnota na určitém místě nám říká, jakou minci můžeme použít. Pokud máme target 10 a coins 5, 2, 1 – první řešení je "5 5" potom se podle zavolání na stacku vrátíme na "5" a zbytek mincí neznáme, podle indexu ale můžeme určit, že 5 tam už nebude. Je to způsob, jak si pamatovat,které mince nesmíme používat
        //found_something         - našli jsme správnou kombinaci mincí? ne – found_something = false pokud jo – found_something = true
        static void Coining(int[] coins, int target, List<int> so_far, int sum, int index, ref bool found_something)
        {
            if (sum > target) return;
            //jestli je suma mincí té specifické kombinace větší než target, nedává to smysl, a proto dáme return, aby se nic nedělo a na stacku se vyvolá další, takže – target je 11 a coins jsou 5 a 1, program nejdřív dojede k "5 5 " potom zkusí "5 5 5" ale 15>11 a potom to zkusí "5 5 1"....

            if (sum == target)
            {
                Console.WriteLine(string.Join(" ", so_far));
                found_something = true;
                return;
            }
            //jestli je součet roven targetu, vypíše se list so_far a found_something přeskoší na true – když neexistuje řešení, found_something zůstane takové, jaké jsme tomu zadali a my jsme začli s bool that = false; tudíž pokud je to false, napíše se "Nejde to :("

            for (int i = index; i < coins.Length; i++)
            {
                so_far.Add(coins[i]);
                Coining(coins, target, so_far, sum + coins[i], i, ref found_something);
                so_far.RemoveAt(so_far.Count - 1);
            }
            //udělali jsme dva ify, kde se na jejich koncích volá return; to znamená, že for loop nastane, když sum < target
            //for loop začíná na hodnotě i = index, to je to posunutí
            //přidá se vybraný coin z coins
            //rekurzivně voláme metodu Coining() kam zadáme jen věci, které potřebujeme a ty, které nepotřebujeme (ty mince, které jsme už použili a nepotřebujeme je)
            //rekurzí se nám „vytvoří“ takový pomyslný strom, který nejdříve sjede uplně na jednu jeho stranu (zkusí všechny největší mince – target = 15 coins 5, 2, 1 – zkusí "5" pak se rekurzivně volá a jede dál "5 5" a dál "5 5 5" potom "5 5 2" potom "5 5 2 2" atd.)
            //když se dostaneme na řádek 62, znamená to, že je sum je v předchozí zavolání funkce větší nebo rovná. Pokud se rovná, executenuly se řádky 52, 53 a 54 a pak se teprve ze so_far ubere poslední prvek.
            //pokud je větší, ubere ze poslední prvek ze so_far
            //ubíráme poslední prvek, jelikož jsme ho už vyřešili a chceme vyřešit další možný poslední prvek
        }
    }
}