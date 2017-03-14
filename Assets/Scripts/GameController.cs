using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/**
 * Handles game logic and AI
 */

public class GameController : MonoBehaviour
{

    private Player player;
    public static GameObject[] positionArr;
    public static string playersChoice = "";
    private Dictionary<BoardPossibleWinScenarios, int> playerBoard = new Dictionary<BoardPossibleWinScenarios, int>();
    private Dictionary<BoardPossibleWinScenarios, int> opponentBoard = new Dictionary<BoardPossibleWinScenarios, int>();
    private enum BoardPossibleWinScenarios { firstRow, secondRow, thirdRow, firstColumn, secondColumn, thirdColumn, forwardDiagonal, backDiagonal };
    public static bool isPlayersTurn = true;

    /*protected static enum boardPicks {
        Axe,
        Circle
        };*/

    // the boards x's and circles
    public GameObject[,] boardArr = new GameObject[3,3];
    
	// Use this for initialization
    void Awake()
    {
        player = new Player();
        initBoard();
        initializePlayersScore();


        if (Application.loadedLevel.Equals(1))
        {
             rollStart();
        }
	}

   

    // fill boardArr with the data in each position
    private void initBoard() {
       
        positionArr = GameObject.FindGameObjectsWithTag("Position");
        for (int i = 0; i < positionArr.Length; i++) {
            switch (positionArr[i].name) {
                case "[0,0]":
                    boardArr[0, 0] = positionArr[i];
                    break;
                case "[0,1]":
                    boardArr[0, 1] = positionArr[i];
                    break;
                case "[0,2]":
                    boardArr[0, 2] = positionArr[i];
                    break;
                case "[1,0]":
                    boardArr[1, 0] = positionArr[i];
                    break;
                case "[1,1]":
                    boardArr[1, 1] = positionArr[i];
                    break;
                case "[1,2]":
                    boardArr[1, 2] = positionArr[i];
                    break;
                case "[2,0]":
                    boardArr[2, 0] = positionArr[i];
                    break;
                case "[2,1]":
                    boardArr[2, 1] = positionArr[i];
                    break;
                case "[2,2]":
                    boardArr[2, 2] = positionArr[i];
                    break;
                }
            }
        } 

    private void rollStart() {
        IEnumerable<int> playerRange;
        int rInt;
        generateRandomNumber(out playerRange, out rInt);

        if (playerRange.Contains(rInt)) {
            Debug.Log("player");
        }
        else {
            Debug.Log("AI");
        }
    }

    private static void generateRandomNumber(out IEnumerable<int> playerRange, out int rInt) {
        playerRange = Enumerable.Range(0, 50);
        IEnumerable<int> aiRange = Enumerable.Range(51, 49);
        System.Random r = new System.Random();
        rInt = r.Next(0, 100);
    }

    public void initializePlayersScore()
    {
        int score = 0;
        foreach (BoardPossibleWinScenarios boardPossibleWinScenario in Enum.GetValues(typeof(BoardPossibleWinScenarios)))
        {
            playerBoard.Add(boardPossibleWinScenario, score);
            opponentBoard.Add(boardPossibleWinScenario, score);
        }
    }

    //TODO: finish, remember to check for tie as well !
    public void isGameOver()
    {
        bool isGameOver = false;
        foreach (KeyValuePair<BoardPossibleWinScenarios, int> boardPosition in playerBoard)
        {
            if(playerBoard[boardPosition.Key].Equals(3))
            {
                Debug.Log("player win");
                isGameOver = true;
            }
        }
        foreach (KeyValuePair<BoardPossibleWinScenarios, int> boardPosition in opponentBoard)
        {
            if (opponentBoard[boardPosition.Key].Equals(3))
            {
                Debug.Log("opponent win");
                isGameOver = true;
            }
        }
        int isTie = 0;
        foreach(GameObject o in positionArr)
        {
            ScriptPosition parentPosition = o.GetComponent<ScriptPosition>();
            isTie = parentPosition.isClicked ? isTie + 1 : isTie;
        }
        if(isTie == 9)
        {
            Debug.Log("tie");
            isGameOver = true;

        }
        if(isGameOver)
        {
            restartGame();
        }
    }

    public void restartGame()
    {
        playersChoice = "";
        //reset wins
        foreach (BoardPossibleWinScenarios boardPossibleWinScenario in Enum.GetValues(typeof(BoardPossibleWinScenarios)))
        {
            playerBoard[boardPossibleWinScenario] = 0;
            opponentBoard[boardPossibleWinScenario] = 0;
        }
        //reset gamobjects
        for (int i = 0; i < GameController.positionArr.Length; i++)
        {
            positionArr[i].GetComponent<ScriptPosition>().isClicked = false;
            for (int j = 0; j < GameController.positionArr[i].transform.childCount; j++)
            {
                GameObject xOrCircle = GameController.positionArr[i].transform.GetChild(j).gameObject;
                xOrCircle.GetComponent<ColorControl>().SetColor(xOrCircle.GetComponent<ColorControl>().AlphaColor);
            }
        }
    }

    public void incrementOpponentPositionScore(string positionToIncrement)
    {
        switch (positionToIncrement)
        {
            case "[0,0]":
                opponentBoard[BoardPossibleWinScenarios.firstRow]++;
                opponentBoard[BoardPossibleWinScenarios.firstColumn]++;
                opponentBoard[BoardPossibleWinScenarios.backDiagonal]++;
                break;
            case "[0,1]":
                opponentBoard[BoardPossibleWinScenarios.firstRow]++;
                opponentBoard[BoardPossibleWinScenarios.secondColumn]++;
                break;
            case "[0,2]":
                opponentBoard[BoardPossibleWinScenarios.firstRow]++;
                opponentBoard[BoardPossibleWinScenarios.thirdColumn]++;
                opponentBoard[BoardPossibleWinScenarios.forwardDiagonal]++;
                break;
            case "[1,0]":
                opponentBoard[BoardPossibleWinScenarios.secondRow]++;
                opponentBoard[BoardPossibleWinScenarios.firstColumn]++;
                break;
            case "[1,1]":
                opponentBoard[BoardPossibleWinScenarios.secondRow]++;
                opponentBoard[BoardPossibleWinScenarios.secondColumn]++;
                opponentBoard[BoardPossibleWinScenarios.forwardDiagonal]++;
                opponentBoard[BoardPossibleWinScenarios.backDiagonal]++;
                break;
            case "[1,2]":
                opponentBoard[BoardPossibleWinScenarios.secondRow]++;
                opponentBoard[BoardPossibleWinScenarios.thirdColumn]++;
                break;
            case "[2,0]":
                opponentBoard[BoardPossibleWinScenarios.thirdRow]++;
                opponentBoard[BoardPossibleWinScenarios.firstColumn]++;
                opponentBoard[BoardPossibleWinScenarios.forwardDiagonal]++;
                break;
            case "[2,1]":
                opponentBoard[BoardPossibleWinScenarios.thirdRow]++;
                opponentBoard[BoardPossibleWinScenarios.secondColumn]++;
                break;
            case "[2,2]":
                opponentBoard[BoardPossibleWinScenarios.thirdRow]++;
                opponentBoard[BoardPossibleWinScenarios.thirdColumn]++;
                opponentBoard[BoardPossibleWinScenarios.backDiagonal]++;
                break;
        }
    }

    public void incrementPlayerPositionScore(string positionToIncrement)
    {
        switch (positionToIncrement)
        {
            case "[0,0]":
                playerBoard[BoardPossibleWinScenarios.firstRow]++;
                playerBoard[BoardPossibleWinScenarios.firstColumn]++;
                playerBoard[BoardPossibleWinScenarios.backDiagonal]++;
                break;
            case "[0,1]":
                playerBoard[BoardPossibleWinScenarios.firstRow]++;
                playerBoard[BoardPossibleWinScenarios.secondColumn]++;
                break;
            case "[0,2]":
                playerBoard[BoardPossibleWinScenarios.firstRow]++;
                playerBoard[BoardPossibleWinScenarios.thirdColumn]++;
                playerBoard[BoardPossibleWinScenarios.forwardDiagonal]++;
                break;
            case "[1,0]":
                playerBoard[BoardPossibleWinScenarios.secondRow]++;
                playerBoard[BoardPossibleWinScenarios.firstColumn]++;
                break;
            case "[1,1]":
                playerBoard[BoardPossibleWinScenarios.secondRow]++;
                playerBoard[BoardPossibleWinScenarios.secondColumn]++;
                playerBoard[BoardPossibleWinScenarios.forwardDiagonal]++;
                playerBoard[BoardPossibleWinScenarios.backDiagonal]++;
                break;
            case "[1,2]":
                playerBoard[BoardPossibleWinScenarios.secondRow]++;
                playerBoard[BoardPossibleWinScenarios.thirdColumn]++;
                break;
            case "[2,0]":
                playerBoard[BoardPossibleWinScenarios.thirdRow]++;
                playerBoard[BoardPossibleWinScenarios.firstColumn]++;
                playerBoard[BoardPossibleWinScenarios.forwardDiagonal]++;
                break;
            case "[2,1]":
                playerBoard[BoardPossibleWinScenarios.thirdRow]++;
                playerBoard[BoardPossibleWinScenarios.secondColumn]++;
                break;
            case "[2,2]":
                playerBoard[BoardPossibleWinScenarios.thirdRow]++;
                playerBoard[BoardPossibleWinScenarios.thirdColumn]++;
                playerBoard[BoardPossibleWinScenarios.backDiagonal]++;
                break;
        }
    }

    // Update is called once per frame
    void Update() {
	
	}
}
