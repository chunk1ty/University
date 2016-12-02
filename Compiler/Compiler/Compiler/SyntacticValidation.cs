using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    //TODO : end if -> validation for }
    //TODO : expression validation
    //TODO : else validation
    
    public class SyntacticValidation
    {
        private static int i = 0;
        private static int ifOpenBracets = 0;

        public static void SemValidation(List<Node<string, int>> tokens)
        {
            FirstLineValidation(tokens);

            for (i = 3; i < tokens.Count - 1; i++)
            {
                //TODO : currentToken validation (check i ) it is possible to lose current token
                var currentToken = tokens[i].Symbol;

                if (currentToken == "if")
                {
                    CheckIforWhileStatment(tokens);
                }
                else if (currentToken == "else")
                {
                    CheckElse(tokens);
                }
                else if (tokens[i].Code < 2)
                {
                    //CheckVariables(tokens);
                    CheckExpression(tokens);
                }
                else if (currentToken == "while")
                {
                    CheckIforWhileStatment(tokens);
                }
                else if (currentToken == "}" && ifOpenBracets > 0)
                {
                    ifOpenBracets--;
                }
                else if(currentToken == "}")
                {
                    throw new ArgumentException("Incorrect { " + currentToken);
                }  
                    // add logic for while bracets
                else
                {
                    throw new ArgumentException("Incorrect symbol" + currentToken);
                }
            }

            if (ifOpenBracets != 0)
            {
                throw new ArgumentException("Missing } ");
            }
        }

        private static void CheckElse(List<Node<string, int>> tokens)
        {
            i++;
            if (tokens[i].Symbol != "{")
            {
                throw new ArgumentException("Missing {" + tokens[i].Symbol);
            }
            else
            {
                ifOpenBracets++;

            }
        }

        private static void CheckVariables(List<Node<string, int>> tokens)
        {
            i++;
            if (tokens[i].Symbol != "=")
            {
                throw new ArgumentException("Missing = operator" + tokens[i].Symbol);
            }

            i++;
            if (!IsDigit(tokens[i].Symbol) && tokens[i].Symbol != "true" && tokens[i].Symbol != "false")
            {
                throw new ArgumentException("Incorrect assign value" + tokens[i].Symbol);
            }

            i++;
            if (tokens[i].Symbol != ";")
            {
                throw new ArgumentException("Missing ; " + tokens[i].Symbol);
            }
        }

        

        private static void CheckIforWhileStatment(List<Node<string, int>> tokens)
        {
            i++;
            if (tokens[i].Symbol != "(")
            {
                throw new ArgumentException("Missing (" + tokens[i].Symbol);
            }

            //TODO : change logic for more than 1 nested if
            i++;
            //CheckExpression(tokens);
            IsWord(tokens[i].Symbol);

            i++;
            if (tokens[i].Symbol != "==" && tokens[i].Symbol != "=<" && tokens[i].Symbol != "=>")
            {
                throw new ArgumentException("Missing operator ==, => or =<" + tokens[i].Symbol);
            }

            i++;
            if (!IsWord(tokens[i].Symbol) && !IsDigit(tokens[i].Symbol) && tokens[i].Symbol != "true" && tokens[i].Symbol != "false")
            {
                throw new ArgumentException("Incorrect expression" + tokens[i].Symbol);
            }

            i++;
            if (tokens[i].Symbol != ")")
            {
                throw new ArgumentException("Missing )" + tokens[i].Symbol);
            }

            i++;
            if (tokens[i].Symbol != "{")
            {
                throw new ArgumentException("Missing {" + tokens[i].Symbol);
            }
            else
            {
                ifOpenBracets++;

            }
        }

        private static void CheckExpression(List<Node<string, int>> tokens)
        {            
                i++;
                if (tokens[i].Symbol != "=")
                {                    
                    throw new ArgumentException("Missing = operator or incorrect operator " + tokens[i].Symbol);
                }

                i++;
                while (tokens[i].Symbol != ";" || tokens[i].Symbol != ")")
                {     
                    //check for assaing value to variable
                    if (tokens[i-1].Symbol == "=" && IsDigit(tokens[i].Symbol) && tokens[i+1].Symbol == ";")
                    {
                        i++;
                        break;
                    }

                    if (!IsDigit(tokens[i].Symbol) && !IsWord(tokens[i].Symbol) && tokens[i].Symbol != "true" && tokens[i].Symbol != "false" && tokens[i].Symbol != "(" && tokens[i].Symbol != ")")
                    {
                        throw new ArgumentException("Incorrect assign value" + tokens[i].Symbol);
                    }

                    i++;
                    if (tokens[i].Symbol == ";")
                    {
                        break;
                    }

                    if (tokens[i].Symbol == ")")
                    {
                        break;
                    }

                    if (tokens[i].Symbol != "+" && tokens[i].Symbol != "-" && tokens[i].Symbol != "*" && tokens[i].Symbol != "/")
                    {
                        throw new ArgumentException("Missing sign " + tokens[i].Symbol);
                    }
                    i++;
                }
        }

        private static  void FirstLineValidation(List<Node<string, int>> tokens)
        {
            if (tokens[0].Symbol != "using")
            {
                throw new ArgumentException("The program must start with using" + tokens[i].Symbol);
            }

            IsWord(tokens[1].Symbol);

            if (tokens[2].Symbol != ";")
            {
                throw new ArgumentException("Missing ; " + tokens[i].Symbol);
            }

        }

        private static bool IsWord(string word)
        {
            foreach (var ch in word)
            {
                if (!Char.IsLetter(ch))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsDigit(string number)
        {
            foreach (var ch in number)
            {
                if (!Char.IsDigit(ch))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
