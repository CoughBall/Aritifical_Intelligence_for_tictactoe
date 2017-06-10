using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;

public class MiniMax
{

    int win = 1;
    int lose = -1;
    int tie = 0;

    private enum endGameScenarios { oWins = -1, tie = 0, xWins = 1 };

    public Position getBestPosition(int calculateBestRouteFor, int[] gameBoard, int calculateIsWinFor, int position)
    {
        int isWinGame = this.isWin(gameBoard, calculateIsWinFor);
        if (isWinGame == 1 )
        {
            return new Position(1, position);
        }
        else if (isWinGame == -1)
        {
            return new Position(-1, position);
        }
        else if(isBoardFull(gameBoard))
        {
            return new Position(0, position);
        }

        Position bestPosition = new Position();
        for (int i = 0; i < gameBoard.Length; i++)
        {
            if (gameBoard[i] == 0)
            {
                gameBoard[i] = calculateBestRouteFor;
                //invert calculation between the opponents, clone the board so it wont reflect in other positions
                Position isPositionWin = getBestPosition(calculateBestRouteFor * -1, (int[])gameBoard.Clone(), calculateIsWinFor, i); 
                if (isPositionWin.isWin >= bestPosition.isWin)
                {
                    bestPosition.position = isPositionWin.position;
                    bestPosition.isWin = isPositionWin.isWin;
                }
               gameBoard[i] = 0;
            }
        }
        return bestPosition;
    }

    public class Position
    {
        public int isWin = -1;
        public int position = 0;

        public Position()
        {

        }

        public Position(int isWin, int position)
        {
            this.isWin = isWin;
            this.position = position;
        }
    }

    private bool isBoardFull(int[] gameBoard)
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

    //the method gets a board and just checks if its a win for 'x' 'o' or tie, thats it
    private int isWin(int[] gameBoard, int calculateBestRouteFor)
    {
        //win = 1, lose = -1, tie =0
        //x == 1, o == -1, tie == 0

        int isWinRow = 0;
        int isWinForwardDiagonal = 0;   // /
        int isWinBackDiagonal = 0;      // \
        int rowNumber = 1;
        int rowSize = (int)Math.Sqrt(gameBoard.Length);
        int[] isWinColumnArr = new int[rowSize];
        int winCondition = createWinCondition(calculateBestRouteFor);
        int loseCondition = winCondition * -1;

        for (int i = 0; i < gameBoard.Length; i++)
        {
            rowNumber = isNewRow(rowSize, i) ? ++rowNumber : rowNumber;
            isWinRow = resetWinConditionForRow(isWinRow, rowSize, i);
        
            calculateIsColumnWin(gameBoard, rowSize, isWinColumnArr, i);
            isWinRow = calculateIsRowWin(gameBoard, isWinRow, i);
            isWinBackDiagonal = calculateIsBackDiagonalWin(gameBoard, isWinBackDiagonal, rowNumber, rowSize, i);
            isWinForwardDiagonal = calculateIsForwardDiagonalWin(gameBoard, isWinForwardDiagonal, rowNumber, rowSize, i);

            if (isWinForwardDiagonal == winCondition || isWinBackDiagonal == winCondition || isWinRow == winCondition || isWinColumnArr[i % rowSize] == winCondition)
            {
                return 1;
            }
            else if (isWinForwardDiagonal == loseCondition || isWinBackDiagonal == loseCondition || isWinRow == loseCondition || isWinColumnArr[i % rowSize] == loseCondition)
            {
                return -1;
            }
        }
        return 0;
    }

    private void calculateIsColumnWin(int[] gameBoard, int rowSize, int[] isWinColumnArr, int i)
    {
        isWinColumnArr[i % rowSize] += gameBoard[i];
    }

    private int calculateIsRowWin(int[] gameBoard, int isWinRow, int i)
    {
        isWinRow += gameBoard[i];
        return isWinRow;
    }

    private int calculateIsForwardDiagonalWin(int[] gameBoard, int isWinForwardDiagonal, int rowNumber, int rowSize, int i)
    {
        if (i != 0 && i == ((rowSize - 1) * rowNumber))
        {
            isWinForwardDiagonal += gameBoard[i];
        }
        return isWinForwardDiagonal;
    }

    private int calculateIsBackDiagonalWin(int[] gameBoard, int isWinBackDiagonal, int rowNumber, int rowSize, int i)
    {
        if ((i == ((rowSize + 1) * (rowNumber - 1))))
        {
            isWinBackDiagonal += gameBoard[i];
        }
        return isWinBackDiagonal;
    }

    private int resetWinConditionForRow(int isWinRow, int rowSize, int i)
    {
        if (isNewRow(rowSize, i))
        {
            isWinRow = 0;
        }
        return isWinRow;
    }

    private bool isNewRow(int rowSize, int i)
    {
        return i != 0 && (i % rowSize) == 0;
    }

    private int createWinCondition(int calculateBestRouteFor)
    {
        int win = 3;
        if (calculateBestRouteFor == -1)
        {
            win *= calculateBestRouteFor;
        }
        return win;
    }

}