using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maturita
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Chessboard chessboard = new Chessboard();
            try
            {
                chessboard.GetLocationOfPieces();
                List<(int, int)> pathSoFar = new List<(int, int)>();
                pathSoFar.Add(chessboard.startingPos);
                List<(int, int)> bestPathSoFar = new List<(int, int)>();
                int bestPathLength = int.MaxValue;
                bool pathWasFound = false;
                (int, List<(int, int)>) ans = chessboard.Step(chessboard.startingPos, pathSoFar, ref bestPathSoFar, ref pathWasFound, ref bestPathLength, ref bestPathLength);

                if (pathWasFound)
                {
                    Console.WriteLine(ans.Item1);
                    Console.WriteLine(string.Join(" –> ", ans.Item2));
                }
                else
                {
                    Console.WriteLine("Nelze");
                }
            }
            catch
            {
                Console.WriteLine("Špatný formát vstupu");
            }
        }
        
        class Chessboard
        {
            private int[,] board = new int[8,8];
            public (int row, int col) startingPos { get; private set; }
            private (int row, int col) endingPos;
            private List<(int row, int col)> listOfPieces = new List<(int, int)>();

            enum namesOfPieces
            {
                emptySquare = 0,
                piece = 1
            }
            struct KnightMove
            {
                public KnightMove(int r, int c)
                {
                    row = r; 
                    col = c;
                }
                public int row;
                public int col;
            }
            List<KnightMove> moves = new List<KnightMove>()
            {
                new KnightMove(2, 1),
                new KnightMove(1, 2),
                new KnightMove(-1, 2),
                new KnightMove(-2, 1),
                new KnightMove(-2, -1),
                new KnightMove(-1, -2),
                new KnightMove(1, -2),
                new KnightMove(2, -1),
            };


            /// <summary>
            /// Získá polohu figurek, začáteční polohu, konečnou polohu.
            /// </summary>
            public void GetLocationOfPieces()
            {
                int numOfPieces = int.Parse(Console.ReadLine());
                if(numOfPieces > 64 || numOfPieces < 0)
                {
                    throw new Exception("number of pieces exceeds the number of squares avaiable");
                }
                for (int i = 0; i < numOfPieces; i++)
                { 
                    int[] xyAxis = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                    listOfPieces.Add((xyAxis[0], xyAxis[1]));
                    if(xyAxis.Length != 2)
                    {
                        throw new Exception("Wrong number of coordinates");
                    }
                    board[xyAxis[0], xyAxis[1]] = (int)namesOfPieces.piece;
                }
                
                int[] xyStart = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                if(xyStart.Length != 2)
                {
                    throw new Exception("Wrong number of coordinates");
                }
                
                int[] xyEnd = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                if (xyEnd.Length != 2)
                {
                    throw new Exception("Wrong number of coordinates");
                }

                startingPos = (xyStart[0], xyStart[1]);
                endingPos = (xyEnd[0], xyEnd[1]);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="currentPos"></param>
            /// <param name="pathSoFar"></param>
            /// <param name="bestPathSoFar"></param>
            /// <param name="pathWasFound"></param>
            /// <param name="bestPathLength"></param>
            /// <param name="maxSuccessfulValue">Pomyslný branch, který jako první úspěšně našel cestu do endingPos, implikuje, že žádný branch, který je delší, nemá smysl býr prohledán</param>
            public (int foundPath, List<(int, int)> path) Step((int row, int col) currentPos,  List<(int row, int col)>  pathSoFar,  ref List<(int row, int col)> bestPathSoFar, ref bool pathWasFound, ref int bestPathLength, ref int maxSuccessfulValue) 
            {
                if(pathSoFar.Count > maxSuccessfulValue)
                {
                    return (bestPathLength - 1, bestPathSoFar);
                }
                if(currentPos == endingPos)
                {
                    if(pathSoFar.Count <= bestPathLength)
                    {
                        bestPathSoFar = new List<(int row, int col)>(pathSoFar);
                        bestPathLength = bestPathSoFar.Count;
                        maxSuccessfulValue = bestPathLength;
                    }
                    pathWasFound = true;
                    return (bestPathLength - 1, bestPathSoFar);
                }
                
                int x = currentPos.row;
                int y = currentPos.col;
                board[x, y] = (int)namesOfPieces.piece;

                foreach (KnightMove offset in moves)
                {
                    if(offset.row + x < 8 & offset.row + x > -1 &  offset.col + y > -1 & offset.col + y < 8)
                    {
                        if (board[x + offset.row, y + offset.col] == (int)namesOfPieces.emptySquare)
                        {
                            pathSoFar.Add((x + offset.row, y + offset.col));
                            Step((x + offset.row, y + offset.col), pathSoFar, ref bestPathSoFar, ref pathWasFound, ref bestPathLength, ref maxSuccessfulValue);
                        }
                    }
                }
                return (bestPathLength - 1, bestPathSoFar);
            }
        }
    }
}