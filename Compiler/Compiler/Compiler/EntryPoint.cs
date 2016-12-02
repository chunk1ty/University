namespace Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
     

    //TODO : add logic for else operator
    public class EntryPoint
    {
        public static BinaryTreeImp<string, int> tree = new BinaryTreeImp<string, int>();

        static void Main()
        {            
            List<Node<string, int>> list = new List<Node<string, int>>();

            string input = @"  
using ank;

if ( a => c)
{
    while ( ankk => 6)
    {
        a = 5 + 6;
    }
}

end";

            list = LexicalValidation.LexValidation(input);

            SyntacticValidation.SemValidation(list);

            SemanticValidation.SemValidation(list);

            SemanticValidation.PrintMF();

            SemanticValidation.CreateASMFile();
        }
    }
}
