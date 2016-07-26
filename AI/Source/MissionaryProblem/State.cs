namespace MissionaryProblem
{
    public class State
    {
        public int numberMissionaryLeft;
        public int numberCannibalLeft;
        public int numberMissionaryRight;
        public int numberCannibalRight;
        public bool boatSide;
        public State previousState;

        public State(int ml, int cl, int mr, int cr, bool side, State previous)
        {
            this.numberMissionaryLeft = ml;
            this.numberMissionaryRight = mr;
            this.numberCannibalLeft = cl;
            this.numberCannibalRight = cr;
            this.boatSide = side;
            this.previousState = previous;
        }
        public override string ToString()
        {
            return (boatSide ? "Left " : "Right") + "  " + numberMissionaryLeft + "/" + numberCannibalLeft + "  " + numberMissionaryRight + "/" + numberCannibalRight;
        }

    }
}
