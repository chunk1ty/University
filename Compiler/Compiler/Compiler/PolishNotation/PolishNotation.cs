using System;
using System.Collections.Generic;
using System.Text;

public static class PolishNotation
{
    public static List<char> arithmeticOperations = new List<char> { '+', '-', '/', '*' };
    public static List<char> bracets = new List<char> { '(', ')' };
    public static List<string> function = new List<string> { "pow", "sqrt", "ln" };

    // separate tokens (how to understand pow,sqrt,ln...)
    public static List<string> SeparateTokens(string input)
    {
        List<string> result = new List<string>();
        var number = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
           
            if (input[i] == '-' && (i == 0 || input[i - 1] == ',' || input[i - 1] == '('))
            {
                number.Append('-');
            }

            else if (char.IsDigit(input[i]) || input[i] == '.')
            {
                number.Append(input[i]);
            }

            else if (!char.IsDigit(input[i]) && input[i] != '.' && number.Length != 0)
            {
                result.Add(number.ToString());
                number.Clear();
                i--;
            }
            //check whether '(', ')' exist in input string
            else if (bracets.Contains(input[i]))
            {
                result.Add(input[i].ToString());
            }

            //check whether '+', '-', '/', '*' exist in input string
            else if (arithmeticOperations.Contains(input[i]))
            {
                result.Add(input[i].ToString());
            }
            else if (input[i] == ',')
            {
                result.Add(",");
            }
            // ToLower se izpolza zashtoto moje da e vuvedeno s glavni bukvi
            else if (i + 1 < input.Length && input.Substring(i, 2).ToLower() == "ln")
            {
                result.Add("ln");
                // increase index with one because i read two character
                i++;
            }

            //purvata proverka ni garantira che sme v stringa (something like exception out of range)
            else if (i + 2 < input.Length && input.Substring(i, 3).ToLower() == "pow")
            {
                result.Add("pow");
                i += 2;
            }

            else if (i + 3 < input.Length && input.Substring(i, 4).ToLower() == "sqrt")
            {
                result.Add("sqrt");
                i += 3;
            }
            else
            {
                throw new ArgumentException("Invalid Expression!");
            }
        }

        if (number.Length != 0)
        {
            result.Add(number.ToString());
        }
        return result;
    }
    // step 4 Reverse Polish notation
    public static Queue<string> ConvertToReversePolishNotation(List<string> tokens)
    {
        Stack<string> stack = new Stack<string>();
        Queue<string> queue = new Queue<string>();

        for (int i = 0; i < tokens.Count; i++)
        {
            var currentToken = tokens[i];
            double number;

            // if token is number put in the queue
            if (double.TryParse(currentToken, out number))
            {
                queue.Enqueue(currentToken);
            }
            // if token is function put in stack
            else if (function.Contains(currentToken))
            {
                stack.Push(currentToken);
            }

            else if (currentToken == ",")
            {
                if (!stack.Contains("(") || stack.Count == 0)
                {
                    throw new ArgumentException("Invalid brackets or function separator!");
                }

                while (stack.Peek() != "(")
                {
                    queue.Enqueue(stack.Pop());
                }
            }

            else if (arithmeticOperations.Contains(currentToken[0]))
            {
                while (stack.Count != 0 && arithmeticOperations.Contains(stack.Peek()[0]) && Precedence(currentToken) <= Precedence(stack.Peek()))
                {
                    queue.Enqueue(stack.Pop());
                }
                stack.Push(currentToken);
            }

            else if (currentToken == "(")
            {
                stack.Push("(");
            }

            else if (currentToken == ")")
            {
                if (!stack.Contains("(") || stack.Count == 0)
                {
                    throw new ArgumentException("Invalid brackets position!");
                }

                while (stack.Peek() != "(")
                {
                    string value = stack.Pop();
                    queue.Enqueue(value);
                }

                stack.Pop();

                if (stack.Count != 0 && function.Contains(stack.Peek()))
                {
                    queue.Enqueue(stack.Pop());
                }
            }
        }
        while (stack.Count != 0)
        {
            if (bracets.Contains(stack.Peek()[0]))
            {
                throw new ArgumentException("Invalid brackets position!");
            }

            queue.Enqueue(stack.Pop());
        }

        return queue;
    }

    public static int Precedence(string arithmeticOperator)
    {
        if (arithmeticOperator == "+" || arithmeticOperator == "-")
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public static double GetResultFromRPN(Queue<string> queue)
    {
        Stack<double> stack = new Stack<double>();
        while (queue.Count != 0)
        {
            string currentToken = queue.Dequeue();
            double number;

            if (double.TryParse(currentToken, out number))
            {
                stack.Push(number);
            }
            else if (arithmeticOperations.Contains(currentToken[0]) || function.Contains(currentToken))
            {
                if (currentToken == "+")
                {
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException("Invalid expression");
                    }

                    double firstValue = stack.Pop();
                    double secondValue = stack.Pop();
                    stack.Push(firstValue + secondValue);
                }
                else if (currentToken == "-")
                {
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException("Invalid expression");
                    }

                    double firstValue = stack.Pop();
                    double secondValue = stack.Pop();
                    stack.Push(secondValue - firstValue);
                }
                else if (currentToken == "*")
                {
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException("Invalid expression");
                    }

                    double firstValue = stack.Pop();
                    double secondValue = stack.Pop();
                    stack.Push(secondValue * firstValue);
                }
                else if (currentToken == "/")
                {
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException("Invalid expression");
                    }

                    double firstValue = stack.Pop();
                    double secondValue = stack.Pop();
                    stack.Push(secondValue / firstValue);
                }
            }
        }

        if (stack.Count == 1)
        {
            return stack.Pop();
        }
        else
        {
            throw new ArgumentException("Invalid expression");
        }
    }

    public static void StartCalculating(string expresion)
    {
        string input = expresion.Trim();        
        while (input.ToLower() != "end")
        {
            try
            {
                string trimmedInput = input.Replace(" ", string.Empty);  //remove all whitespace (Step 2) second solution
                var separatedTokens = SeparateTokens(trimmedInput);
                var reversePolishNotation = ConvertToReversePolishNotation(separatedTokens);
                var final = GetResultFromRPN(reversePolishNotation);
                Console.WriteLine(final);
            }
            catch (ArgumentException exeption)
            {
                Console.WriteLine(exeption.Message);
            }
            input = Console.ReadLine().Trim();
        }
    }
}

