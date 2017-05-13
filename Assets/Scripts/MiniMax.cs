using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;

public class MiniMax : MonoBehaviour
{

    //private Dictionary<GameController.BoardPossibleWinScenarios, int> boardPossibleWinScenarios = new Dictionary<GameController.BoardPossibleWinScenarios, int>();
    int win = 1;
    int lose = -1;
    int tie = 0;

    private enum endGameScenarios { oWins = -1, tie = 0, xWins = 1 };

    //because its a 2 player game, the player(or ai) cant predict the opponent 

    //basic solution - search whole tree from current node (can be part of a tree if we are already down)

    //if the AI starts first or sceond it doesnt matter, the root will have a game with 1 position already taken or not by the player

    //TODO: remember to check the ultimate win condition ! if might get higher preference over a normal win, ultime win = always win in certain conditions

    public string calculateNode(int calculateBestRouteFor, int[] gameBoard, Dictionary<GameController.BoardPossibleWinScenarios, int> possibleWinScenarios) //add the game itself to increment to it, reset it when ur back to original node to send it to other tree branches
    {
        if (isBoardFull(gameBoard)) //or when there is win (can be before board is full). tie and lose we want to check later
        {
            return "new best position"; //return the position to take upwards until the node we came from
        }
        else
        {
            //if calculateBestRouteFor = X then send o, and opposite
            findAFreePositionOnTheBoardAndClickIt(calculateBestRouteFor, gameBoard);
            return calculateNode(calculateBestRouteFor, (int []) gameBoard.Clone(), new Dictionary<GameController.BoardPossibleWinScenarios, int>(possibleWinScenarios));
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
    private static int isWin(int[] gameBoard, int calculateBestRouteFor)
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

    private static void calculateIsColumnWin(int[] gameBoard, int rowSize, int[] isWinColumnArr, int i)
    {
        isWinColumnArr[i % rowSize] += gameBoard[i];
    }

    private static int calculateIsRowWin(int[] gameBoard, int isWinRow, int i)
    {
        isWinRow += gameBoard[i];
        return isWinRow;
    }

    private static int calculateIsForwardDiagonalWin(int[] gameBoard, int isWinForwardDiagonal, int rowNumber, int rowSize, int i)
    {
        if (i != 0 && i == ((rowSize - 1) * rowNumber))
        {
            isWinForwardDiagonal += gameBoard[i];
        }
        return isWinForwardDiagonal;
    }

    private static int calculateIsBackDiagonalWin(int[] gameBoard, int isWinBackDiagonal, int rowNumber, int rowSize, int i)
    {
        if ((i == ((rowSize + 1) * (rowNumber - 1))))
        {
            isWinBackDiagonal += gameBoard[i];
        }
        return isWinBackDiagonal;
    }

    private static int resetWinConditionForRow(int isWinRow, int rowSize, int i)
    {
        if (isNewRow(rowSize, i))
        {
            isWinRow = 0;
        }
        return isWinRow;
    }

    private static bool isNewRow(int rowSize, int i)
    {
        return i != 0 && (i % rowSize) == 0;
    }

    private static int createWinCondition(int calculateBestRouteFor)
    {
        int win = 3;
        if (calculateBestRouteFor == -1)
        {
            win *= calculateBestRouteFor;
        }
        return win;
    }

    /// <summary>
    /// 1 = win
    /// 0 = tie
    /// -1 = lose
    /// </summary>
    /// heuristicEvaluation
    /* private int isWin(string calculateWinFor, Dictionary<GameController.BoardPossibleWinScenarios, int> possibleWinScenarios)
     {
         bool isGameOver = false;
         foreach (KeyValuePair<BoardPossibleWinScenarios, int> boardPosition in playerBoard)
         {
             if (playerBoard[boardPosition.Key].Equals(3))
             {
                 Debug.Log("player win");
                 isGameOver = true;
             }
         }
     }*/

    //TODO: is calculate tie
    //TODO: is calculate lose


    private void findAFreePositionOnTheBoardAndClickIt(int calculateBestRouteFor, int[] gameBoard)
    {
        for (int i = 0; i < gameBoard.Length; i++)
        {
            if (gameBoard[i] == 0)
            {
                gameBoard[i] = calculateBestRouteFor;
                break;
            }
        }
    }

    /*private void renderPositionClicked(GameObject position)
    {
        GameObject parent = GameObject.Find(gameObject.transform.parent.gameObject.name);
        parent.GetComponent<ScriptPosition>().isClicked = true;
    }*/

    // Update is called once per frame
    void Update()
    {

    }

    // Use this for initialization
    void Awake()
    {
    }


}