// C# program to demonstrate working
// of Gaussian Elimination method
using System;
namespace good{
class GFG
    {
	
        public static int numOfEquations;

        static void Main(string[] args)
        {
            numOfEquations = 3;
            double[,] matrix = new double[numOfEquations ,numOfEquations + 1];
			matrix = new double[,] { { 1, -1, 3, 7 }, 
                                     { 1, 1, 4, 2 },
                                     { 1, 1, 0, 4 } };
            gaussianElimination(matrix);

        }

        public static void gaussianElimination(double [,]matrix)
        {
            int rowCount = numOfEquations;
            int colCount = numOfEquations + 1;

            Console.WriteLine("Augmented Matrix:");
            printAug(matrix);
            Console.WriteLine();
            // Iterate on rows in the system
            for (int col = 0; col < colCount; col++)
            {
                // Iterate on cols in the system
                for (int row = 0; row < rowCount; row++)
                {
                    // Find the first non-zero element in the first non-zero column
                    if (matrix[row, col] == 0)
                        continue;

                    else if (matrix[row, col] == 1)
                    {
                        found: // After swapping a row that has 1
                        // Make a leading one in the diagonal
                        if (row == col)
                        {
                            for (int sourceRow = 0; sourceRow + 1 < colCount; sourceRow++)
                            {
                                for (int destRow = sourceRow + 1; destRow < rowCount; destRow++)
                                {
                                    // Make the lower elements equal to zero
                                    double df = matrix[sourceRow, sourceRow];
                                    double sf = matrix[destRow, sourceRow];
                                    for (int i = 0; i < rowCount + 1; i++)
                                    matrix[destRow, i] = matrix[destRow, i] * df - matrix[sourceRow, i] * sf;
                                }
                                
                            }
                            Console.WriteLine("Gaussian Elimination:");
                            printAug(matrix);
                            Console.WriteLine();
                        }
                        else if (row > col)
                        {
                            swapRow(matrix, row, col);
                            goto found;
                        }
                        else continue;
                    }
                    // Check for non-zero element and make it one
                    else
                    {   found: // After swapping a row that has non-zero element
                        if (row == col)
                        {
                            double d = matrix[row, col];
                            for (int i = 0; i < colCount; i++)
                                {matrix[row, i] *= 1 / d;}
                              
                            printAug(matrix);
                            Console.WriteLine();  
                        }
                        else if (row > col)
                        {
                            swapRow(matrix, row, col);
                            goto found;
                        }
                        else continue;
                    }
                }
            }
            backSub(matrix);
        }

        private static void swapRow(double [,]matrix, int i, int j)
        {
            for (int k = 0; k < numOfEquations + 1; k++)
            {
                double temp = matrix[i, k];
                matrix[i, k] = matrix[j, k];
                matrix[j, k] = temp;
            }
        }

        private static void printAug(double [,]matrix)
        {
            for (int row = 0; row < numOfEquations; row++)
            {
                for (int col = 0; col < numOfEquations + 1; col++)
                {
                    Console.Write(matrix[row, col] + "  ");
                    if (col == numOfEquations - 1) Console.Write("| ");
                }
                Console.WriteLine();
            }
        }

        public static void backSub(double[,] matrix)
        {
            // An array to store solution
            double[] x = new double[numOfEquations];

            // Start calculating from last equation up to the first
            for (int i = numOfEquations - 1; i >= 0; i--)
            {
                // Start with the RHS of the equation
                x[i] = matrix[i, numOfEquations];

                // Initialize j to i+1 since matrix is upper triangular
                for (int j = i + 1; j < numOfEquations; j++)
                {
                    /* Subtract all the lhs values
                    * except the coefficient of the variable
                    * whose value is being calculated */
                    x[i] -= matrix[i, j] * x[j];
                }

                // Divide the RHS by the coefficient of the unknown being calculated 
                if(matrix[i, i] == 0) continue; // For not dividing by zero
                x[i] = x[i] / matrix[i, i];
            }
            Console.WriteLine($"X = {(float)x[0]}");
            Console.WriteLine($"Y = {(float)x[1]}");
            Console.WriteLine($"Z = {(float)x[2]}");
        }
    }
}
