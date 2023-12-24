using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace d5
{
    class HailStone
    {
        public (long, long, long) start, velocity;
        public double a, b, c;

        public HailStone((long, long, long) p, (long, long, long) v )
        {
            start = p;
            velocity = v;
            a = v.Item2;
            b = - v.Item1;
            c = v.Item2*p.Item1 - v.Item1 * p.Item2;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            long count = 0;
            List<HailStone> hail = new List<HailStone>();

            for (int i = 0; i < input.Length; i++)
            {
                string[] split1 = input[i].Split('@');
                string[] split2 = split1[0].Split(',');
                string[] split3 = split1[1].Split(',');

                var pos = (long.Parse(split2[0]), long.Parse(split2[1].Substring(1)), long.Parse(split2[2].Substring(1)));
                var vel = (long.Parse(split3[0]), long.Parse(split3[1].Substring(1)), long.Parse(split3[2].Substring(1)));
                HailStone h = new HailStone(pos, vel);
                hail.Add(h);
            }

            List<int> collided = new List<int>();
            for(int i = 0; i < hail.Count; i++)
            {
                for (int j = i+1; j < hail.Count; j++)
                {
                    bool collision = FindCollision(hail[i], hail[j]);

                    if (collision)
                    {
                        if (!collided.Contains(j))
                        {
                            collided.Add(j);
                        }
                        if (!collided.Contains(i))
                        {
                            collided.Add(i);
                        }
                        count++;
                    }
                }
            }

            
            Console.WriteLine("P1: " + count);
            Console.ReadKey();
        }

        static bool FindCollision(HailStone h1, HailStone h2)
        {
            if(h1.a*h2.b == h2.a * h1.b) // parallel
            {
                return false;
            }

            double[,] matrixA = { {h1.a,h1.b }, {h2.a,h2.b} };
            double[] matrixB = { h1.c, h2.c };
            List<double> result = new List<double>();

            double[,] inverseA = Inverse(matrixA);

            for (int i = 0; i < inverseA.GetLength(0); i++)
            {
                double sum = 0;

                for (int j = 0; j < inverseA.GetLength(1); j++)
                {
                        sum += (double)(inverseA[i, j] * matrixB[j]);
                }
                result.Add(sum);
            }
;
            foreach(var item in result)
            {
                if(item < 200000000000000 || item > 400000000000000)
                {
                    return false;
                }
            }


            if (!((result[0] - h1.start.Item1)*h1.velocity.Item1 >=0 && (result[1] - h1.start.Item2) * h1.velocity.Item2 >= 0))
            {
                return false;
            }

            if (!((result[0] - h2.start.Item1) * h2.velocity.Item1 >= 0 && (result[1] - h2.start.Item2) * h2.velocity.Item2 >= 0))
            {
                return false;
            }
            //&& (result[2] - h2.start.Item3) * h2.velocity.Item3 >= 0)

            return true;
        }

        static double[,] Inverse(double[,] matrix)
        {
            double  [,] inverse = new double[matrix.GetLength(0), matrix.GetLength(0)];

            if (matrix.GetLength(0) == 2)
            {
                double det = Determinant(matrix);
                inverse[0, 0] = matrix[1, 1] / det;
                inverse[1, 1] = matrix[0, 0] / det;
                inverse[0, 1] = -matrix[0, 1] / det;
                inverse[1, 0] = -matrix[1, 0] / det;
                return inverse;
            }
            else
            {

                double det = Determinant(matrix);
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (i % 2 == j % 2)
                        {
                            inverse[i, j] = Determinant(Cofactor(matrix, i, j)) / det;
                        }
                        else
                        {
                            inverse[i, j] = (-1) * Determinant(Cofactor(matrix, i, j)) / det;
                        }
                    }
                }

                inverse = Transpose(inverse);
            }

            return inverse;
        }
        static double Determinant(double[,] matrix)
        {
            double det = 0;

            if (matrix.GetLength(0) == 2)
            {
                det += matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
            }
            else
            {
                for (int i = 0; i < matrix.GetLength(0); ++i)
                {
                    double[,] cofactorMatrix = Cofactor(matrix, 0, i);

                    if (i % 2 == 0)
                    {
                        det += matrix[0, i] * Determinant(cofactorMatrix);
                    }
                    else if (i % 2 == 1)
                    {
                        det -= matrix[0, i] * Determinant(cofactorMatrix);
                    }
                }
            }

            return det;
        }
        static double[,] Cofactor(double[,] matrix, double row, double coloum)
        {
            double[,] cofactorMatrix = new double[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];

            bool checkRow = false;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i == row)
                {
                    checkRow = true;
                }
                else
                {
                    bool checkColoum = false;

                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (j == coloum)
                        {
                            checkColoum = true;
                        }
                        else if (checkColoum && checkRow)
                        {
                            cofactorMatrix[i - 1, j - 1] = matrix[i, j];
                        }
                        else if (checkRow)
                        {
                            cofactorMatrix[i - 1, j] = matrix[i, j];
                        }
                        else if (checkColoum)
                        {
                            cofactorMatrix[i, j - 1] = matrix[i, j];
                        }
                        else
                        {
                            cofactorMatrix[i, j] = matrix[i, j];
                        }
                    }
                }
            }

            return cofactorMatrix;
        }
        static double[,] Transpose(double[,] matrix)
        {
            double[,] transposed = new double[matrix.GetLength(0), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    transposed[i, j] = matrix[j, i];
                }
            }

            return transposed;
        }
    }
}

