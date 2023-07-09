using System;

namespace Project
{
    class RandomElementFromMatrix
    {
        public static void Main()
        {
            //Matrix example:
            byte[,] matrix = new byte[,] { { 0, 1, 0, 0, 0 },
                                           { 0, 1, 1, 1, 0 },
                                           { 0, 0, 0, 0, 0 },
                                           { 0, 1, 1, 1, 0 },
                                           { 0, 0, 1, 0, 0 },
                                           { 0, 0, 0, 0, 0 } };
            
            //Code for demonstration:
            for(int i = 0;  i < 8; i++) //8 is the number of matrix elements equal to 1
            {
                short[] coordinates = generateRandom(matrix);
                Console.WriteLine($"{coordinates[0]} {coordinates[1]}");//
                matrix[coordinates[0], coordinates[1]] = 0;
            }
        }

        static short[] generateRandom(byte[,] matrix) //Returns x and y coordinate of matrix
        {
            Random rnd = new Random();
            short[] result = new short[2];

            short[] rows = parseRows(matrix);
            short numerOfSelectedRow = rows[rnd.Next(0, rows.Length)];
            result[0] = numerOfSelectedRow;

            byte[] selectedRow = new byte[matrix.GetLength(1)];
            for(int i = 0; i < matrix.GetLength(1); i++) //C# doesn't allow me to write something like this "selectedRow = matrix[numerOfSelectedRow]". Therefore I have to do this:
            {
                selectedRow[i] = matrix[numerOfSelectedRow, i];
            }
            short[] columns =  parseColumn(selectedRow);
            result[1] = columns[rnd.Next(0, columns.Length)];

            return result;
        }

        static short[] parseRows(byte[,] matrix) //Returns array of numbers which are contained number of not empty rows
        {
            short[] subResult = new short[matrix.GetLength(0)]; //Second name is "State of row". This array contains as many elements as rows the matrix contains.
                                                                //Element will equal to (number of row + 1) if row contain 1. If row has only 0 (is empty), element will equal to 0
                                                                //This array we will use to random row.
            short numberOfEmptyRows = (short)matrix.GetLength(0);

            for (short a = 0; a < matrix.GetLength(0); a++)
            {
                bool isRowСompleted = false;
                for(short b = 0; b < matrix.GetLength(1); b++)
                {
                    if (matrix[a, b] == 1) //If row contain 1
                    {
                        isRowСompleted = true;
                        break;
                    }
                }

                if (isRowСompleted)
                {
                    subResult[a] = (short)(a + 1); // I add 1 to protect row number 1 which has index 0. Because after I wil deleate all 0 from array
                    numberOfEmptyRows--; //If row isn't empty we reduce variable
                }
            }
            short[] result = clearZeroInArray(numberOfEmptyRows, subResult); //Our sub result contain 0 that will prevent us. So it must be cut off.

            return result;
        }

        static short[] parseColumn(byte[] rowOfMatrix) //It has the same principle of operation like parseRows (even easier) so I will not explain it
        {
            short[] subResult = new short[rowOfMatrix.Length];
            short numberOfZero = (short)rowOfMatrix.Length;

            for (short i = 0; i < rowOfMatrix.Length; i++)
            {
                if (rowOfMatrix[i] == 1)
                {
                    subResult[i] = (short)(i + 1);
                    numberOfZero--;
                }
            }

            short[] result = clearZeroInArray(numberOfZero, subResult);

            return result;
        }

        static short[] clearZeroInArray(short numOfZero, short[] array)
        {
            short[] result = new short[array.Length - numOfZero];
            short iteration = 0;

            for(short i = 0; i < array.Length; i++)
            {
                if(array[i] != 0)
                {
                    result[iteration++] = --array[i];
                }
            }

            return result;
        }
    }
}