namespace MissionaryProblem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BfsAlgorithm
    {
        public static State BFS(State start)
        {
            //List<State> allSolution = new List<State>();
            Queue<State> Open = new Queue<State>();
            List<State> Closed = new List<State>();
            Open.Enqueue(start);

            while (Open.Count > 0)
            {
                State X = Open.Dequeue();
                if (CheckIfGoalNode(X))
                {
                    //allSolution.Add(X);
                    //if (Open.Count != 0)
                    //{
                    //    continue;
                    //}
                    //else
                    //{
                    return X;
                    //}
                }
                if (IsRepeat(Closed, X))
                {
                    continue;
                }

                for (int i = 0; i < 5; i++)
                {
                    int[] arr = null;
                    if (X.boatSide)
                    {
                        switch (i)
                        {
                            case 0: arr = new int[] { -1, 0, 1, 0 }; break;    //One missionar
                            case 1: arr = new int[] { 0, -1, 0, 1 }; break;    //One cannibal
                            case 2: arr = new int[] { -2, 0, 2, 0 }; break;   //Two missionary
                            case 3: arr = new int[] { 0, -2, 0, 2 }; break;   //Two cannibals
                            case 4: arr = new int[] { -1, -1, 1, 1 }; break;  //One missionar and one cannibal
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            //same like above but for another bank of river 
                            case 0: arr = new int[] { 1, 0, -1, 0 }; break;
                            case 1: arr = new int[] { 0, 1, 0, -1 }; break;
                            case 2: arr = new int[] { 2, 0, -2, 0 }; break;
                            case 3: arr = new int[] { 0, 2, 0, -2 }; break;
                            case 4: arr = new int[] { 1, 1, -1, -1 }; break;
                        }
                    }
                    State next = new State(X.numberMissionaryLeft + arr[0], X.numberCannibalLeft + arr[1], X.numberMissionaryRight + arr[2], X.numberCannibalRight + arr[3], !X.boatSide, X);
                    if (CheckIfValidNode(next))
                    {
                        Open.Enqueue(next);
                    }
                }
                //if (!IsContentInOpen(Open, X))
                //{
                Closed.Add(X);
                //}               
            }
            return null;
        }

        public static bool IsRepeat(List<State> list, State X)
        {
            foreach (var item in list)
            {
                if (item.numberCannibalLeft == X.numberCannibalLeft && item.numberCannibalRight == X.numberCannibalRight && item.numberMissionaryLeft == X.numberMissionaryLeft && item.numberMissionaryRight == X.numberMissionaryRight && item.boatSide == X.boatSide)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsContentInOpen(Queue<State> list, State X)
        {
            foreach (var item in list)
            {
                if (item.numberCannibalLeft == X.numberCannibalLeft && item.numberCannibalRight == X.numberCannibalRight && item.numberMissionaryLeft == X.numberMissionaryLeft && item.numberMissionaryRight == X.numberMissionaryRight && item.boatSide == X.boatSide)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CheckIfValidNode(State X)
        {
            if ((X.numberMissionaryLeft > 0 && X.numberCannibalLeft > X.numberMissionaryLeft) || (X.numberMissionaryRight > 0 && X.numberCannibalRight > X.numberMissionaryRight))
            {
                return false;
            }
            if (X.numberCannibalLeft < 0 || X.numberMissionaryLeft < 0 || X.numberCannibalRight < 0 || X.numberMissionaryRight < 0)
            {
                return false;
            }
            return true;
        }
        public static bool CheckIfGoalNode(State X)
        {
            if (X.numberMissionaryLeft == 0 && X.numberCannibalLeft == 0 && X.boatSide == false)
            {
                return true;
            }
            return false;
        }
    }
}
