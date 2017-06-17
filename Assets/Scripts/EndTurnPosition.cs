using UnityEngine;
using UnityEditor;

/// <summary>
/// Class to hold values for a situation where we reached an end turn (<see cref="EndGameScenario"/>)
/// </summary>
public class EndTurnPosition
{
    public int isWin = (int)EndGameScenario.Lose;
    public int position = 0;
    public int depth = 8;

    public EndTurnPosition() { }

    public EndTurnPosition(EndGameScenario isWin, int depth, int position)
    {
        this.isWin = (int)isWin;
        this.depth = depth;
        this.position = position;
    }
}