using System;
using System.Collections.Generic;

namespace LCC162
{
    class Program
    {
        public static int OddCells(int n, int m, int[][] indices)
        {
            if (n == 0 || m == 0 || indices.Length == 0)
                return 0;

            int[][] matrix = new int[n][];
            for(int i=0;i<n;i++)
            {
                matrix[i] = new int[m];
            }

            for(int i=0;i<indices.Length;i++)
            {
                int r = indices[i][0];
                int c = indices[i][1];

                // update row
                for(int k = 0;k<m;k++)
                {
                    matrix[r][k] += 1;
                }

                // update column
                for(int k=0;k<n;k++)
                {
                    matrix[k][c] += 1;
                }
            }

            int oddCount = 0;
            
            for(int i=0;i<n;i++)
            {
                for(int j=0;j<m;j++)
                {
                    if(matrix[i][j]%2 == 1)
                    {
                        oddCount++;
                    }
                }
            }

            return oddCount;
        }

        // upper = 5, lower = 5, colsum = [2,1,2,0,1,0,1,2,0,1]
        // 1, *, 1, 0, *, 0, *, 1, 0, *  ==>  1, 1, 1, 0, 1, 0, 0, 1, 0, 0
        // 1, *, 1, 0, *, 0, *, 1, 0, *       1, 0, 1, 0, 0, 0, 1, 1, 0, 1
        public static IList<IList<int>> ReconstructMatrix(int upper, int lower, int[] colsum)
        {
            IList<IList<int>> matrix = new List<IList<int>>();
            matrix.Add(new List<int>());
            matrix.Add(new List<int>());

            for(int i=0;i<colsum.Length;i++)
            {
                switch(colsum[i])
                {
                    case 0:
                        matrix[0].Add(0);
                        matrix[1].Add(0);
                        break;
                    case 1:
                        matrix[0].Add(-1);
                        matrix[1].Add(-1);
                        break;
                    case 2:
                        matrix[0].Add(1);
                        matrix[1].Add(1);
                        upper--;
                        lower--;
                        break;
                    default:
                        break;
                }
            }

            for(int i = 0; i<colsum.Length;i++)
            {
                if(upper > 0 && matrix[0][i] == -1)
                {
                    matrix[0][i] = 1;
                    matrix[1][i] = 0;
                    upper--;
                }
                else if(lower > 0 && matrix[1][i] == -1)
                {
                    matrix[0][i] = 0;
                    matrix[1][i] = 1;
                    lower--;
                }
                else if(matrix[0][i] == -1)
                {
                    return new List<IList<int>>();
                }
            }

            if (upper == 0 && lower == 0)
            {
                return matrix;
            }

            return new List<IList<int>>();
        }

        public static int ClosedIsland(int[][] grid)
        {
            int islands = 0;
            int length = grid.Length;
            int breadth = grid[0].Length;
            
            for(int i=0;i<length;i++)
            {
                if(grid[i][0] == 0)
                {
                    grid[i][0] = -1;
                }

                if(grid[i][breadth-1] == 0)
                {
                    grid[i][breadth - 1] = -1;
                }
            }

            for (int i = 0; i < breadth; i++)
            {
                if (grid[0][i] == 0)
                {
                    grid[0][i] = -1;
                }

                if (grid[length-1][i] == 0)
                {
                    grid[length -1][i] = -1;
                }
            }

            for(int i = 0;i<length;i++)
            {
                for(int j=0;j<breadth;j++)
                {
                    if(grid[i][j] == 0)
                    {
                        if(DFS(ref grid, i, j))
                        {
                            islands++;
                        }
                    }
                }
            }

            return islands;
        }

        private static bool DFS(ref int[][] grid, int x, int y)
        {
            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();
            stack.Push(new Tuple<int, int>(x, y));
            bool result = true;
            while(stack.Count > 0)
            {
                Tuple<int, int> last = stack.Pop();
                int i = last.Item1;
                int j = last.Item2;

                if(grid[i][j] != 0)
                {
                    continue;
                }
                grid[i][j] = 2;

                int left = grid[i][j-1];
                int right = grid[i][j+1];
                int top = grid[i-1][j];
                int bottom = grid[i+1][j];

                if(top == -1 || bottom == -1 || left == -1 || right == -1)
                {
                    result = false;
                }
                if (top == 0)
                    stack.Push(new Tuple<int, int>(i-1, j));
                if (bottom == 0)
                    stack.Push(new Tuple<int, int>(i+1, j));
                if (left == 0)
                    stack.Push(new Tuple<int, int>(i, j-1));
                if (right == 0)
                    stack.Push(new Tuple<int, int>(i, j+1));
            }
            return result;
        }

        static void Main(string[] args)
        {
            //IList<IList<int>> result = ReconstructMatrix(1, 4, new int[] { 2, 1, 2, 0, 0, 2});
            //[[0,0,0,0,0,0,1,1,1,0],[0,1,0,1,0,1,0,1,1,1],[1,0,1,0,1,1,0,0,0,1],[1,1,1,1,1,1,0,0,0,0],[1,1,1,0,0,1,0,1,0,1],[1,1,1,0,1,1,0,1,1,0]]
            int[][] grid = new int[10][];
            grid[0] = new int[] { 0, 0, 1, 1, 0, 1, 0, 0, 1, 0 };
            grid[1] = new int[] { 1, 1, 0, 1, 1, 0, 1, 1, 1, 0 };
            grid[2] = new int[] { 1, 0, 1, 1, 1, 0, 0, 1, 1, 0 };
            grid[3] = new int[] { 0, 1, 1, 0, 0, 0, 0, 1, 0, 1 };
            grid[4] = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 0 };
            grid[5] = new int[] { 0, 1, 0, 1, 0, 1, 0, 1, 1, 1 };
            grid[6] = new int[] { 1, 0, 1, 0, 1, 1, 0, 0, 0, 1 };
            grid[7] = new int[] { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 };
            grid[8] = new int[] { 1, 1, 1, 0, 0, 1, 0, 1, 0, 1 };
            grid[9] = new int[] { 1, 1, 1, 0, 1, 1, 0, 1, 1, 0 };

            int islands = ClosedIsland(grid);
            Console.WriteLine("Hello World!");
        }
    }
}
