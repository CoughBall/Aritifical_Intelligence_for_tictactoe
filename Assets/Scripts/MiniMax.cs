using System;

public class MiniMax
{

    public EndTurnPosition GetBestPosition(int calculateBestRouteFor, int[] gameBoard, int calculateIsWinFor, int position, int depth)
    {
        EndGameScenario isGameOver = IsGameOver(gameBoard, calculateIsWinFor);
        if (isGameOver == EndGameScenario.Lose && depth == 2)   //to stop player from winning on his first move - trick the AI to think he won with the best possible Position to prevent opponent from winning
        {
            return new EndTurnPosition(EndGameScenario.Win, 2, position);    //if it doesnt work give it lowest position like -1
        }
        else if (isGameOver == EndGameScenario.Win)
        {
            return new EndTurnPosition(EndGameScenario.Win, depth, position);
        }
        else if (isGameOver == EndGameScenario.Lose)
        {
            return new EndTurnPosition(EndGameScenario.Lose, depth, position);
        }
        else if (IsBoardFull(gameBoard))
        {
            return new EndTurnPosition(EndGameScenario.Tie, depth, position);
        }
        depth++;

        EndTurnPosition bestPosition = new EndTurnPosition();
        for (int i = 0; i < gameBoard.Length; i++)
        {
            if (gameBoard[i] == 0)
            {
                gameBoard[i] = calculateBestRouteFor;
                //invert calculation between the opponents, clone the board so it wont reflect in other positions
                EndTurnPosition isPositionWin = GetBestPosition(calculateBestRouteFor * -1, (int[])gameBoard.Clone(), calculateIsWinFor, i, depth);
                if ((isPositionWin.isWin >= bestPosition.isWin && isPositionWin.depth <= bestPosition.depth))
                {
                    bestPosition.position = isPositionWin.position;
                    bestPosition.isWin = isPositionWin.isWin;
                    bestPosition.depth = isPositionWin.depth;
                }
                gameBoard[i] = 0;
            }
        }
        depth--;
        return bestPosition;
    }

    private bool IsBoardFull(int[] gameBoard)
    {
        foreach (int gameNode in gameBoard)
        {
            if (gameNode == 0)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Checks if the current status of the game board is an end game
    /// </summary>
    private EndGameScenario IsGameOver(int[] gameBoard, int calculateBestRouteFor)
    {
        //win = 1, lose = -1, tie =0
        //x == 1, o == -1, tie == 0

        int isWinRow = 0;
        int isWinForwardDiagonal = 0;   // /
        int isWinBackDiagonal = 0;      // \
        int rowNumber = 1;
        int rowSize = (int)Math.Sqrt(gameBoard.Length);
        int[] isWinColumnArr = new int[rowSize];
        int winCondition = CreateWinCondition(calculateBestRouteFor);
        int loseCondition = winCondition * -1;

        for (int i = 0; i < gameBoard.Length; i++)
        {
            rowNumber = IsNewRow(rowSize, i) ? ++rowNumber : rowNumber;
            isWinRow = ResetWinConditionForRow(isWinRow, rowSize, i);

            CalculateIsColumnWin(gameBoard, rowSize, isWinColumnArr, i);
            isWinRow = CalculateIsRowWin(gameBoard, isWinRow, i);
            isWinBackDiagonal = CalculateIsBackDiagonalWin(gameBoard, isWinBackDiagonal, rowNumber, rowSize, i);
            isWinForwardDiagonal = CalculateIsForwardDiagonalWin(gameBoard, isWinForwardDiagonal, rowNumber, rowSize, i);

            if (isWinForwardDiagonal == winCondition || isWinBackDiagonal == winCondition || isWinRow == winCondition || isWinColumnArr[i % rowSize] == winCondition)
            {
                return EndGameScenario.Win;
            }
            else if (isWinForwardDiagonal == loseCondition || isWinBackDiagonal == loseCondition || isWinRow == loseCondition || isWinColumnArr[i % rowSize] == loseCondition)
            {
                return EndGameScenario.Lose;
            }
        }
        return EndGameScenario.Tie;
    }

    private void CalculateIsColumnWin(int[] gameBoard, int rowSize, int[] isWinColumnArr, int i)
    {
        isWinColumnArr[i % rowSize] += gameBoard[i];
    }

    private int CalculateIsRowWin(int[] gameBoard, int isWinRow, int i)
    {
        isWinRow += gameBoard[i];
        return isWinRow;
    }

    private int CalculateIsForwardDiagonalWin(int[] gameBoard, int isWinForwardDiagonal, int rowNumber, int rowSize, int i)
    {
        if (i != 0 && i == ((rowSize - 1) * rowNumber))
        {
            isWinForwardDiagonal += gameBoard[i];
        }
        return isWinForwardDiagonal;
    }

    private int CalculateIsBackDiagonalWin(int[] gameBoard, int isWinBackDiagonal, int rowNumber, int rowSize, int i)
    {
        if ((i == ((rowSize + 1) * (rowNumber - 1))))
        {
            isWinBackDiagonal += gameBoard[i];
        }
        return isWinBackDiagonal;
    }

    private int ResetWinConditionForRow(int isWinRow, int rowSize, int i)
    {
        if (IsNewRow(rowSize, i))
        {
            isWinRow = 0;
        }
        return isWinRow;
    }

    private bool IsNewRow(int rowSize, int i)
    {
        return i != 0 && (i % rowSize) == 0;
    }

    private int CreateWinCondition(int calculateBestRouteFor)
    {
        int win = 3;
        if (calculateBestRouteFor == -1)
        {
            win *= calculateBestRouteFor;
        }
        return win;
    }

}