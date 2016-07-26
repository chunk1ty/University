namespace MissionaryProblem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main()
        {
            State start = new State(3, 3, 0, 0, true, null);
            //List for all solution
            State finish = BfsAlgorithm.BFS(start);
            if (finish == null)
            {
                Console.WriteLine(("No solution was found."));
                return;
            }

            //for (int i = 0; i < finish.Count; i++)
            //{
            //TODO: fix nextState = finish[i];
            State nextState = finish;
            Stack<State> path = new Stack<State>();

            while (nextState != null)
            {
                path.Push(nextState);
                nextState = nextState.previousState;
            }
            Console.WriteLine(("Solution path: "));
            foreach (var item in path)
            {
                Console.WriteLine((item));
            }
            Console.WriteLine();
            //}


        }

    }
}
