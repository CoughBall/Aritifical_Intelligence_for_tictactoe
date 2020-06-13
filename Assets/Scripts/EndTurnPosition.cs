/// <summary>
/// Class to hold values for a situation where we reached an end turn (<see cref="EndGameScenario"/>)
/// </summary>
public class EndTurnPosition
{
    public int score;
    public int position;

    public EndTurnPosition() { }

    public EndTurnPosition(int score)
    {
        this.score = score;
    }

}