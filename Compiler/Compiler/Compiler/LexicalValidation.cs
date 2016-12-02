namespace Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class LexicalValidation
    {
        public static List<Node<string, int>> LexValidation(string input)
        {
            BinaryTreeImp<string, int> tree = new BinaryTreeImp<string, int>();
            MyConstants(tree);            
            List<Node<string, int>> list = new List<Node<string, int>>();
            
            StringBuilder word = new StringBuilder();
            StringBuilder digit = new StringBuilder();
            StringBuilder signs = new StringBuilder();

            for (int index = 0; index < input.Length - 1; index++)
            {
                char currentSymbol = input[index];

                if (Char.IsLetter(currentSymbol))
                {
                    if (Char.IsDigit(input[index + 1]))
                    {
                        throw new ArgumentException("Can not contain digit !" + input[index + 1]);
                    }
                    if (digit.Length > 0)
                    {
                        string currentWord = digit.ToString();
                        list.Add(tree.FindTab(currentWord, 3, tree));
                        digit = new StringBuilder();
                    }

                    if (signs.Length > 0)
                    {
                        string currentWord = signs.ToString();
                        list.Add(tree.FindTab(currentWord, 1, tree));
                        signs = new StringBuilder();
                    }
                    word.Append(currentSymbol);
                }
                else if (Char.IsDigit(currentSymbol))
                {
                    if (Char.IsLetter(input[index + 1]))
                    {
                        throw new ArgumentException("Can not contain letters !" + input[index + 1]);
                    }
                    if (word.Length > 0)
                    {
                        string currentWord = word.ToString();
                        list.Add(tree.FindTab(currentWord, 1, tree));
                        word = new StringBuilder();
                    }

                    if (signs.Length > 0)
                    {
                        string currentWord = signs.ToString();
                        list.Add(tree.FindTab(currentWord, 1, tree));
                        signs = new StringBuilder();
                    }
                    digit.Append(currentSymbol);    
                }
                else if (currentSymbol == '(' || currentSymbol == ')' || currentSymbol == ';' || currentSymbol == '<' || currentSymbol == '>' || currentSymbol == '=' || currentSymbol == '+' || currentSymbol == '-' || currentSymbol == '*' || currentSymbol == '/' || currentSymbol == '}' || currentSymbol == '{')
	            {
                    if (word.Length > 0)
                    {
                        string currentWord = word.ToString();
                        list.Add(tree.FindTab(currentWord, 1, tree));
                        word = new StringBuilder();
                    }

                    if (digit.Length > 0)
                    {
                        string currentWord = digit.ToString();
                        list.Add(tree.FindTab(currentWord, 2, tree));
                        digit = new StringBuilder();
                    }

                    if (signs.Length > 0)
                    {
                        string currentWord = signs.ToString();
                        list.Add(tree.FindTab(currentWord, 1, tree));
                        signs = new StringBuilder();
                    }

                    if (currentSymbol == '=')
                    {
                        if (input[index+1] == '=')
                        {
                            signs.Append("==");
                            index++;
                        }
                        else if (input[index+1] == '<')
                        {
                            signs.Append("=<");
                            index++;
                        }
                        else if (input[index + 1] == '>')
                        {
                            signs.Append("=>");
                            index++;
                        }
                        else
                        {
                            signs.Append('=');
                        }
                    }
                    else if (currentSymbol == '+')
                    {
                        if (input[index + 1] == '+')
                        {
                            signs.Append("++");
                            index++;
                        }
                        else
                        {
                            signs.Append('+');
                        }
                    }
                    else
                    {
                        signs.Append(currentSymbol);
                    }   
	            }

                else if (currentSymbol == ' ')
                {
                    if (word.Length > 0)
                    {
                        string currentWord = word.ToString();
                        list.Add(tree.FindTab(currentWord, 1, tree));
                        word = new StringBuilder();
                    }

                    if (digit.Length > 0)
                    {
                        string currentWord = digit.ToString();
                        list.Add(tree.FindTab(currentWord, 2, tree));
                        digit = new StringBuilder();
                    }

                    if (signs.Length > 0)
                    {
                        string currentWord = signs.ToString();
                        list.Add(tree.FindTab(currentWord, 1, tree));
                        signs = new StringBuilder();
                    }
                }
                else
                {
                    if (char.IsWhiteSpace(currentSymbol))
                    {
                        continue;
                    }
                    else
	                {
                         throw new ArgumentException("Symbol {0}!!"+ currentSymbol);
	                }
                   
                }
            }
            if (digit.Length > 0)
            {
                string currentWord = digit.ToString();
                list.Add(tree.FindTab(currentWord, 2, tree));
                digit = new StringBuilder();
            }

            if (word.Length > 0)
            {
                string currentWord = word.ToString();
                list.Add(tree.FindTab(currentWord, 1, tree));
                word = new StringBuilder();
            }

            if (signs.Length > 0)
            {
                string currentWord = signs.ToString();
                list.Add(tree.FindTab(currentWord, 1, tree));
                signs = new StringBuilder();
            }

            return list;
        }

        public static void MyConstants(BinaryTreeImp<string, int> tree)
        {
            tree.Add("using", 100);
            tree.Add("while", 101);
            tree.Add("if", 102);
            tree.Add("en", 103);
            tree.Add("else", 104);
            tree.Add(";", 200);
            tree.Add(">", 201);
            tree.Add("<", 202);
            tree.Add("=", 203);
            tree.Add("==", 204);
            tree.Add("+", 205);
            tree.Add("-", 206);
            tree.Add("*", 207);
            tree.Add("/", 208);
            tree.Add("(", 209);
            tree.Add(")", 210);
            tree.Add("{", 211);
            tree.Add("}", 212);
            tree.Add("=<", 213);
            tree.Add("=>", 213);
        }
    }
}
