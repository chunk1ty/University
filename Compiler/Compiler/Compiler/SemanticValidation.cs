namespace Compiler
{
    using System.Collections.Generic;
    using System;
    using System.IO;

    public static class SemanticValidation
    {
        private static int currentIndexAnk = -1;
        private static int indexAnk = -1;
        private static string variable;
        private static string result;
        private static string IforWhile;
        private static string sys;
        private static List<object> operant17 = new List<object>();
        private static List<int> whileRowBRZ = new List<int>();
        private static List<int> BrzRow = new List<int>();
        private static List<int> ifBrzRow = new List<int>();
        private static string operant1;  
        private static string operant2;        
        private static string sysVar;
        private static int N;
        private static object[,] Tet = new object[30, 5];
        private static int row;
        private static int BRZplace = 0;
        private static int JMPplace = 0;
        private static int doBegin = 0;

        public static void SemValidation(List<Node<string, int>> tokens)
        {
            tokens.RemoveRange(0, 3);

            currentIndexAnk++;
            Block(tokens);

            AddOnTet("BRK", "", "", "");
        }

        private static void Block(List<Node<string, int>> tokens)
        {
            indexAnk++;
            CurrentOperator(tokens);
            indexAnk = currentIndexAnk;

            while (tokens[indexAnk].Code == 200)
            {
                //end
                if (tokens[indexAnk+1].Code == 103)
                {
                    currentIndexAnk++;
                    break;
                }

                currentIndexAnk++;
                if (tokens[currentIndexAnk].Symbol == "}")
                {
                    currentIndexAnk++;
                }
                CurrentOperator(tokens);
                indexAnk = currentIndexAnk;
            }
        }

        private static void CurrentOperator(List<Node<string, int>> tokens)
        {
            variable = string.Empty;
            result = string.Empty;
            IforWhile = string.Empty;

            //variables
            if (tokens[currentIndexAnk].Code == 1)
            {
                variable = tokens[currentIndexAnk].Symbol;
                indexAnk++;
                currentIndexAnk++;
                //validation for ( or )
                currentIndexAnk++;
                myExpression(tokens, result);
                AddOnTet("=", result, "", variable);
            }
            // WHILE
            else if(tokens[currentIndexAnk].Code == 101)
            {
                whileRowBRZ.Add(row);   
                currentIndexAnk +=2;
                doBegin = row;
                IfOperator(tokens, IforWhile);
                BRZplace = row;
                //check parametes
                AddOnTet("BRZ", "", sysVar, "");
                BrzRow.Add(row - 1);

                currentIndexAnk += 2;
                indexAnk = currentIndexAnk;
                
                CurrentOperator(tokens);

                int indexWhileList = whileRowBRZ.Count;
                int currentWhileRow = whileRowBRZ[indexWhileList - 1];
                whileRowBRZ.RemoveAt(indexWhileList - 1);
                indexWhileList--;

                AddOnTet("JMP", currentWhileRow, "", "");
                //AddJump(doBegin);
                JMPplace = row;

                int brzRowIndex = BrzRow.Count;
                BRZplace = BrzRow[brzRowIndex - 1];
                BrzRow.RemoveAt(brzRowIndex - 1);
                brzRowIndex--;

                AddIf(BRZplace, "BRZ", row);
                
            }
            // IF
            else if (tokens[currentIndexAnk].Code == 102)
            {
                currentIndexAnk += 2;
                IfOperator(tokens, IforWhile);
                indexAnk = currentIndexAnk;

                BRZplace = row;
                AddOnTet("BRZ", "", sysVar, "");
                ifBrzRow.Add(row - 1);
                currentIndexAnk += 2;
                indexAnk = currentIndexAnk;
                CurrentOperator(tokens);
                indexAnk = currentIndexAnk;

                int ifBrzRowIndex = ifBrzRow.Count;
                BRZplace = ifBrzRow[ifBrzRowIndex - 1];
                ifBrzRow.RemoveAt(ifBrzRowIndex - 1);
                ifBrzRowIndex--;

                //if there is no Else brz shoud jump after operator
                AddIf(BRZplace,"BRZ",row);

                currentIndexAnk += 2;
                indexAnk = currentIndexAnk;

                //if (tokens.Count >= )
                //{
                    
                //}
                // check for else
                if (tokens[indexAnk].Code == 104)
                {
                    JMPplace = row;
                    AddOnTet("JMP", "", "", "");
                    //if there is else redirect brz here,after jmp
                    AddJump(row);
                    AddIf(BRZplace, "BRZ", row);

                    currentIndexAnk +=2;
                    CurrentOperator(tokens);

                    AddJump(row);
                    AddIf(JMPplace, "JMP", row);
                }
            }
        }

       

        private static void IfOperator(List<Node<string, int>> tokens, string ifwHHile)
        {
            variable = string.Empty;
            result = string.Empty;
            sys = string.Empty;

            variable = tokens[currentIndexAnk].Symbol;
            myExpression(tokens, tokens[currentIndexAnk].Symbol);

            currentIndexAnk++;
            myExpression(tokens, result);
            
            int index = operant17.Count;
            result = (string) operant17[index - 1];
            operant17.RemoveAt(index - 1);
            SysVar();

            //tokens[pl].Symbol
            string sign = tokens[indexAnk - 1].Symbol;
            AddOnTet(sign, variable, result, sysVar);
            operant1 = sysVar;
        }

        private static void myExpression(List<Node<string, int>> tokens, string currentSymbol)
        {
            operant2 = string.Empty;
            result = string.Empty;         

            indexAnk++;            
            Term(tokens, currentSymbol);

            // + or -
            while (tokens[currentIndexAnk].Code == 205 || tokens[currentIndexAnk].Code == 206)
            {
                if (tokens[currentIndexAnk].Code == 205)
                {
                    currentIndexAnk++;
                    Term(tokens, operant2);
                    indexAnk++;
                    
                    int index = operant17.Count;
                    operant2 = (string)operant17[index - 1];
                    operant17.RemoveAt(index - 1);
                    index--;
                    operant1 = (string)operant17[index - 1];
                    operant17.RemoveAt(index - 1);
                    
                    SysVar();
                    result = sysVar;
                    AddOnTet("+", operant1, operant2, result);
                    operant1 = result;

                    operant17.Add(sysVar);   
                }
                else 
                {
                    currentIndexAnk++;
                    Term(tokens, operant2);
                    indexAnk++;
                   
                    int index = operant17.Count;
                    operant2 = (string)operant17[index - 1];
                    operant17.RemoveAt(index - 1);
                    index--;
                    operant1 = (string)operant17[index - 1];
                    operant17.RemoveAt(index - 1);

                    SysVar();
                    result = sysVar;
                    AddOnTet("-", operant1, operant2, result);
                    operant1 = result;

                    operant17.Add(sysVar);   
                }
            }
        }

        private static void Term(List<Node<string, int>> tokens, string currentSymbol)
        {
            operant2 = string.Empty;
            result = string.Empty;

            Factor(tokens,currentSymbol);
            indexAnk++;
            // check for * or /
            while (tokens[currentIndexAnk].Code == 207 || tokens[currentIndexAnk].Code == 208)
            {
                if (tokens[currentIndexAnk].Code == 207)
                {
                    currentIndexAnk++;
                    Factor(tokens, operant2);
                    indexAnk++;

                                        int index = operant17.Count;
                    operant2 = (string) operant17[index - 1];
                    operant17.RemoveAt(index - 1);
                    index--;
                    operant1 = (string)operant17[index - 1];
                    operant17.RemoveAt(index - 1);
                   
                    SysVar();
                    result = sysVar;
                    AddOnTet("*", operant1, operant2, result);
                    operant17.Add(sysVar);                   
            }
                else
                {
                    currentIndexAnk++;
                    Factor(tokens, operant2);
                    indexAnk++;
                   
                    int index = operant17.Count;
                    operant2 = (string)operant17[index - 1];
                    operant17.RemoveAt(index - 1);
                    index--;
                    operant1 = (string)operant17[index - 1];
                    operant17.RemoveAt(index - 1);
                   
                    SysVar();
                    result = sysVar;
                    AddOnTet("/", operant1, operant2, result);
                    operant1 = result;

                    operant17.Add(sysVar);  
                }
            }
        }

        private static void Factor(List<Node<string, int>> tokens, string currentSymbol)
        {
            //numbers and variables
            if(tokens[currentIndexAnk].Code == 2 || tokens[currentIndexAnk].Code == 1)
            {                
                operant17.Add(tokens[currentIndexAnk].Symbol);                           
                
                currentIndexAnk++;
            }
        }

        private static void SysVar()
        {
            sysVar = string.Format("&{0}",N);
            N++;
        }

        private static void AddOnTet(string sign, object oprOne, string oprTwo, string result)
        {
            Tet[row,1] = sign;
            Tet[row,2] = oprOne;
            Tet[row,3] = oprTwo;
            Tet[row,4] = result;
            //Console.WriteLine("{0,10} {1,10} {2,10} {3,10} {4,10} {5,10}", row + 1, (string)Tet[row, 0], (string)Tet[row, 1], (string)Tet[row, 2], (string)Tet[row, 3], (string)Tet[row, 4]);
            row++;
           
        }

        private static void AddIf(int place,string command, object op1)
        {
            Tet[place, 1] = command;
            Tet[place, 2] = op1;
        }

        private static void AddJump(int roww)
        {
            //Tet[roww, 0] = "jmp";
        }

        public static void PrintMF()
        {
            for (int i = 0; i < row; i++)
            {
                Console.WriteLine("{0,10} {1,10} {2,10} {3,10} {4,10} {5,10}",i, Tet[i, 0], Tet[i, 1], Tet[i, 2], Tet[i, 3], Tet[i, 4]);
            }
        }

        public static void CreateASMFile()
        {
            StreamWriter write = new StreamWriter("../../ank.asm");

            using (write)
            {
                for (int i = 0; i < row; i++)
                {
                    string probai0 = (string) Tet[i, 0];
                    string probai1 = (string)Tet[i, 1];
                    string probai3 = (string)Tet[i, 3];

                    if (probai0 == "jmp")
                    {
                        write.WriteLine("ET {0} ", i);                       
                    }
                    else
                    {
                       write.WriteLine( "\t ");
                    }
                    if (probai1 == "BRZ")
                    {
                        write.WriteLine("LDA\t {0}", Tet[i, 3]);
                        write.WriteLine("BRZ\tET {0}", Tet[i, 2]);                        
                    }
                    else if (probai1 == "JMP")
                    {
                        write.WriteLine("JMP\tET {0}", Tet[i, 2]);                        
                    }
                    else if (probai1 == "BRK")
                    {
                        write.WriteLine("BRK\t\t");                         
                    }
                    else if (probai1 == "+")
                    {
                        write.WriteLine("LDA\t {0}", Tet[i, 2]);
                        write.WriteLine("ADD\t {0}", Tet[i, 3]);
                        write.WriteLine("STA\t {0}", Tet[i, 4]);                        
                    }
                    else if (probai1 == "-")
                    {
                        write.WriteLine("LDA\t {0}", Tet[i, 2]);
                        write.WriteLine("SUB\t {0}", Tet[i, 3]);
                        write.WriteLine("STA\t {0}", Tet[i, 4]);
                    }
                    else if (probai1 == "*")
                    {
                        write.WriteLine("LDA\t {0}", Tet[i, 2]);
                        write.WriteLine("MULL\t {0}", Tet[i, 3]);
                        write.WriteLine("STA\t {0}", Tet[i, 4]);
                    }
                    else if (probai1 == "/")
                    {
                        write.WriteLine("LDA\t {0}", Tet[i, 2]);
                        write.WriteLine("DIV\t {0}", Tet[i, 3]);
                        write.WriteLine("STA\t {0}", Tet[i, 4]);
                    }
                    else if (probai1 == "=")
                    {
                        if (probai3 == "")
                        {
                            write.WriteLine("LDA\t {0}", Tet[i, 2]);
                            write.WriteLine("STA\t {0}", Tet[i, 4]);                            
                        }
                        else
                        {
                            write.WriteLine("LDA\t {0}", Tet[i, 2]);
                            write.WriteLine("SUB\t {0}", Tet[i, 3]);
                            write.WriteLine("BRZ\t {0}", i);
                            write.WriteLine("LDA\t");
                            write.WriteLine("JMP\t {0}", i);                            
                        }
                    }
                }
            }
        }
    }
}
