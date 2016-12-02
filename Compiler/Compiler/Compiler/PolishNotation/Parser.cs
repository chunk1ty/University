namespace Compiler
{
    using System.Collections.Generic;

    public static class Parser
    {
        public static string ExpresionToString(List<Node<string, int>> list)
        {
            string result = string.Empty;
            string expression = string.Empty;
            List<string> resultAsList = new List<string>();

            result = ExtractStatments(list, result, resultAsList);

            for (int i = 0; i < resultAsList.Count; i++)
            {
                expression = resultAsList[resultAsList.Count - 1].ToString();

                for (int j = 0; j < resultAsList.Count - 1 ; j++)
                {
                    string currentName = ExtractName(resultAsList[j]);
                    string currentValue = ExtractValue(resultAsList[j]);
                    expression = expression.Replace(currentName, currentValue);
                    i++;
                }
            }

            return ExpressionForPolishNotation(expression);
        }

        private static string ExpressionForPolishNotation(string expression)
        {
            string result = null;

            for (int i = 0; i < expression.Length; i++)
            {
                if (char.IsDigit(expression[i]) || expression[i] == '*' || expression[i] == '/' || expression[i] == '-' || expression[i] == '+')
                {
                    result += expression[i];
                }
            }

            return result;
        }



        private static string ExtractValue(string statment)
        {
            string result = null;
            for (int i = 0; i < statment.Length; i++)
            {
                if (statment[i] == '=')
                {
                   while(statment[i] != ';')
                   {
                       result += statment[i+1];
                       i += 2;
                   }
                }                
            }

            return result;
        }

        private static string ExtractName(string statment)
        {
            string result = null;
            for (int i = 0; i < statment.Length; i++)
            {
                if (statment[i] == '=')
                {
                    break;
                }

                result += statment[i];
            }

            return result;
        }

        private static string ExtractStatments(List<Node<string, int>> list, string result, List<string> resultAsList)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Symbol == "=")
                {
                    result += list[i - 1].Symbol + "=";

                    while (list[i].Symbol != ";")
                    {
                        result += list[i + 1].Symbol;
                        i++;
                    }

                    resultAsList.Add(result);
                    result = string.Empty;
                }
            }
            return result;
        }
    }
}
