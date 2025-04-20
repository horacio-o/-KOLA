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
                chessboard.Step(chessboard.startingPos, pathSoFar, bestPathSoFar, pathWasFound, bestPathLength, bestPathLength);
                Console.WriteLine();
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

            /// <summary>
            /// Získá polohu figurek, začáteční polohu, konečnou polohu.
            /// </summary>
            public void GetLocationOfPieces()
            {
                int numOfPieces = int.Parse(Console.ReadLine());
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
            public void Step((int row, int col) currentPos, List<(int row, int col)> pathSoFar, List<(int row, int col)> bestPathSoFar, bool pathWasFound, int bestPathLength, int maxSuccessfulValue) 
            {
                if(pathSoFar.Count > maxSuccessfulValue)
                {
                    return;
                }
                if(currentPos == endingPos)
                {
                    if(pathSoFar.Count <= bestPathLength)
                    {
                        bestPathSoFar = pathSoFar;
                        bestPathLength = bestPathSoFar.Count;
                        if(bestPathLength < maxSuccessfulValue)
                        {
                            maxSuccessfulValue = bestPathLength;
                            Console.WriteLine(bestPathLength);
                            Console.WriteLine(string.Join(" –> ", bestPathSoFar));
                        }
                    }
                    pathWasFound = true;
                    return;
                }
                
                int x = currentPos.row;
                int y = currentPos.col;
                
                //stěna if-ů :(
                if (x + 2 < 8 & y + 1 < 8)
                {
                    if (board[x + 2, y + 1] == (int)namesOfPieces.emptySquare)
                    {
                        pathSoFar.Add((x + 2, y + 1));
                        Step((x + 2, y + 1), pathSoFar, bestPathSoFar, pathWasFound, bestPathLength, maxSuccessfulValue);
                    }
                }
                if (x + 1 < 8 & y + 2 < 8)
                {
                    if (board[x + 1, y + 2] == (int)namesOfPieces.emptySquare)
                    {
                        pathSoFar.Add((x + 1, y + 2));
                        Step((x + 1, y + 2), pathSoFar, bestPathSoFar, pathWasFound, bestPathLength, maxSuccessfulValue);
                    }
                }





                //nefunguje to, zkusit inputy:
                //0
                //0 0
                //3 3




                return;
            }
        }
    }
}
