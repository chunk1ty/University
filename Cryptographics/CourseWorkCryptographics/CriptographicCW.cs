using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CourseWorkCryptographics
{
    class CriptographicCW
    {
        static void Main()
        {
            StreamReader reader = new StreamReader(@"../../Input.txt");
            StreamWriter writer = new StreamWriter(@"../../Output.txt");
            string plaintext = string.Empty;
            using(reader)
	        {
                plaintext = reader.ReadToEnd();
	        }

            Console.Write("Enter a key for Play-Fair cipher : ");
            string firstKey = Console.ReadLine();            

            char[,] allSymbols = new char [5,5];
            allSymbols = FillCellsWithAllAlphabetaSymbols(firstKey);
            //PrintMatrix(allSymbols);
             
            EncryptPainText(allSymbols, plaintext);
            string messageAfterFirstEncrypt = EncryptPainText(allSymbols,plaintext);
            
            //string messageAfterFirstEncrypt = "tobeornottobe";
            Console.Write("Enter a key for Row-transposition cipher : ");
            string secondKey = Console.ReadLine();            

            NumberOfColumn(secondKey);
            FillMatrixWithPaintext(secondKey, messageAfterFirstEncrypt);
            ReverseColumnAndPrintMessage(secondKey, messageAfterFirstEncrypt);
            string encrypthMessage = ReverseColumnAndPrintMessage(secondKey, messageAfterFirstEncrypt);

            using (writer)
            {
                writer.WriteLine(encrypthMessage);
            }
            Console.WriteLine("Done!The message was saved on the Output file!");
            
        }
        static char[,] FillCellsWithAllAlphabetaSymbols(string input)
        {
            string inputToUpper = input.ToUpper();
            char[] alhpabet = new char[26];
            bool[] checkAlphabet = new bool[26];
            char[,] allSymbolWithKey = new char[5, 5];

            for (int i = 0; i < 26; i++)
            {                
                alhpabet[i] = (char)(i + 'A');                
            }
            
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {                    
                    if (inputToUpper.Length != 0)
                    {
                        char currentLetter = inputToUpper[0];
                        inputToUpper = inputToUpper.Remove(0,1);
                        int index = Array.IndexOf(alhpabet, currentLetter);
                        bool checkPosition = checkAlphabet[index];
                        if (!checkPosition)
                        {
                            allSymbolWithKey[row, col] = currentLetter;
                        }
                        else
                        {
                            col--;
                        }
                        checkAlphabet[index] = true;
                    }
                    else
                    {
                        for (int i = 0; i < alhpabet.Length; i++)
                        {
                            if (i == 9)
                            {
                                checkAlphabet[i] = true;
                            }
                            if (!checkAlphabet[i])
                            {
                                allSymbolWithKey[row, col] = (char)(i + 'A');
                                checkAlphabet[i] = true;                                
                                break;
                            }
                            
                        }
                    }
                }
            }
            return allSymbolWithKey;
        } 
        static string EncryptPainText (char[,] symbols,string input)
        {
            StringBuilder sb = new StringBuilder(input.Length);
            input = RemoveIntervalsFromString(input);
            if (input.Length % 2 == 1)
            {
                input = input.Insert(input.Length, "x");
            }            
            
            while (input.Length != 0)
            {
                string currentSymbols = input.Substring(0, 2).ToUpper();
                input = input.Remove(0, 2);
                char firstChar = currentSymbols[0];
                char secondChar = currentSymbols[1];
                int firstLetterRow = 0;
                int firstLetterCol = 0;
                int secondLetterRow = 0;
                int secondLetterCol = 0;

                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        if (firstChar == symbols[row,col])
                        {
                            firstLetterRow = row;
                            firstLetterCol = col;
                        }
                        if (secondChar == symbols[row,col])
                        {
                            secondLetterRow = row;
                            secondLetterCol = col;
                        }
                    }
                }

                if (firstLetterRow == secondLetterRow)
                {
                    sb.Append(AppendSymbolsAtRow(firstLetterRow, firstLetterCol, secondLetterCol, symbols));
                }
                else if (firstLetterCol == secondLetterCol)
                {
                    sb.Append(AppendSymbolsAtCol(firstLetterRow, firstLetterCol, secondLetterRow, symbols));
                }
                else
                {
                    sb.Append(AppendSymolsAtDiagonal(firstLetterRow, firstLetterCol, secondLetterRow, secondLetterCol, symbols));
                }

            }
      
            return sb.ToString();
        }
        static string AppendSymbolsAtRow(int firstRow,int firstCol,int secondCol,char[,] matrix)
        {
            int encryphtFirstLetterCol = firstCol;
            int encryphtSecondLetterCol = secondCol;            

            if (firstCol == 4)
            {
                encryphtFirstLetterCol = 0;
            }
            else
            {
                encryphtFirstLetterCol = firstCol + 1;
            }

            if (secondCol == 4)
            {
                encryphtSecondLetterCol = 0;
            }
            else
            {
                encryphtSecondLetterCol = secondCol + 1;
            }

            string resultFirstChar = matrix[firstRow, encryphtFirstLetterCol].ToString();
            string resultSecondChar = matrix[firstRow, encryphtSecondLetterCol].ToString();

            return resultFirstChar + resultSecondChar;
        }
        static string AppendSymbolsAtCol(int firstRow,int firsrCol,int secondRow,char[,] matrix)
        {
            int encryphtFirstLetterRow = firstRow;            
            int encryphtSecondLetterRow = secondRow;

            if (firstRow == 4)
            {
                encryphtFirstLetterRow = 0;
            }
            else
            {
                encryphtFirstLetterRow++;
            }

            if (secondRow == 4)
            {
                encryphtSecondLetterRow = 0;
            }
            else
            {
                encryphtSecondLetterRow++;
            }
            string firstResult = matrix[encryphtFirstLetterRow, firsrCol].ToString();
            string secondResult = matrix[encryphtSecondLetterRow, firsrCol].ToString();
            
            return firstResult+secondResult;
        }
        static string AppendSymolsAtDiagonal(int firstRow,int firstCol,int secondRow,int secondCol,char[,] matrix)
        {
            string firstLetter = matrix[firstRow,secondCol].ToString();
            string secondLetter = matrix[secondRow, firstCol].ToString();
            return firstLetter + secondLetter;
        }
        static void PrintMatrix (char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(" {0} ",matrix[row,col]);
                }
                Console.WriteLine();
            }
        }
        static string RemoveIntervalsFromString(string input)
        {
            string[] array = input.Split();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i]);
            }
            return sb.ToString();
        }
        //Row Transposition
        static List<int> NumberOfColumn (string key)
        {
            List<int> list = new List<int>();
            char[] alhpabet = new char[26];
            for (int i = 0; i < 26; i++)
            {
                alhpabet[i] = (char)(i + 'A');
            }

            string priority = key.ToUpper();
            string toUpper = key.ToUpper();            
            char[] sortKey = toUpper.ToCharArray();
            Array.Sort(sortKey);

            for (int i = 0; i < sortKey.Length; i++)
            {
                for (int j = 0; j < sortKey.Length; j++)
                {
                    if (sortKey[j] == priority[i])
                    {
                        list.Add(j);
                    }
                }
            }

            return list;
        }
        static char[,] FillMatrixWithPaintext (string key,string paintext)
        {                        
            int row = 0;
            int difference = 0;
            int len = paintext.Length;
            while (true)
            {               
                if (len <= 0)
                {
                    break;
                }
                difference = len - key.Length;
                len = difference;
                row++;
            }
            char[,] matrix = new char[row, key.Length];
            string paintextUpper = paintext.ToUpper();

            for (row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (paintextUpper.Length != 0)
                    {
                        matrix[row, col] = paintextUpper[0];
                        paintextUpper = paintextUpper.Remove(0, 1);
                    }
                    else
                    {
                        matrix[row, col] = 'X';
                    }
                }
            }
            return matrix;
        }
        static string ReverseColumnAndPrintMessage (string key,string paintext)
        {
            char[,] matrix = FillMatrixWithPaintext(key, paintext);
            List<int> list = NumberOfColumn(key);
            StringBuilder sb = new StringBuilder();
            char[,] reverseMatrix = new char[matrix.GetLength(0),matrix.GetLength(1)];

            int colIndex = 0;
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                while (col != list[colIndex])
                {
                    colIndex++;
                }
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    reverseMatrix[row, col] = matrix[row, colIndex];                    
                }
                colIndex = 0;
            }

            for (int row = 0; row < reverseMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < reverseMatrix.GetLength(1); col++)
                {
                    sb.Append(reverseMatrix[row, col]);
                }
            }
            return sb.ToString();
        }
    }
}
