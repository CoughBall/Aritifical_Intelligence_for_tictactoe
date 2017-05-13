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
    private Dictionary<string, bool> gameBoard; //TODO: fill this with the gameboard postions and if they are clicked - send for minimax
    public static Dictionary<BoardPossibleWinScenarios, int> playerBoard = new Dictionary<BoardPossibleWinScenarios, int>();
    public static Dictionary<BoardPossibleWinScenarios, int> opponentBoard = new Dictionary<BoardPossibleWinScenarios, int>();
    public enum BoardPossibleWinScenarios { firstRow, secondRow, thirdRow, firstColumn, secondColumn, thirdColumn, forwardDiagonal, backDiagonal };
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

    public void incrementPositionScore(string positionToIncrement, Dictionary<BoardPossibleWinScenarios, int> board)
    {
        switch (positionToIncrement)
        {
            case "[0,0]":
                board[BoardPossibleWinScenarios.firstRow]++;
                board[BoardPossibleWinScenarios.firstColumn]++;
                board[BoardPossibleWinScenarios.backDiagonal]++;
                break;
            case "[0,1]":
                board[BoardPossibleWinScenarios.firstRow]++;
                board[BoardPossibleWinScenarios.secondColumn]++;
                break;
            case "[0,2]":
                board[BoardPossibleWinScenarios.firstRow]++;
                board[BoardPossibleWinScenarios.thirdColumn]++;
                board[BoardPossibleWinScenarios.forwardDiagonal]++;
                break;
            case "[1,0]":
                board[BoardPossibleWinScenarios.secondRow]++;
                board[BoardPossibleWinScenarios.firstColumn]++;
                break;
            case "[1,1]":
                board[BoardPossibleWinScenarios.secondRow]++;
                board[BoardPossibleWinScenarios.secondColumn]++;
                board[BoardPossibleWinScenarios.forwardDiagonal]++;
                board[BoardPossibleWinScenarios.backDiagonal]++;
                break;
            case "[1,2]":
                board[BoardPossibleWinScenarios.secondRow]++;
                board[BoardPossibleWinScenarios.thirdColumn]++;
                break;
            case "[2,0]":
                board[BoardPossibleWinScenarios.thirdRow]++;
                board[BoardPossibleWinScenarios.firstColumn]++;
                board[BoardPossibleWinScenarios.forwardDiagonal]++;
                break;
            case "[2,1]":
                board[BoardPossibleWinScenarios.thirdRow]++;
                board[BoardPossibleWinScenarios.secondColumn]++;
                break;
            case "[2,2]":
                board[BoardPossibleWinScenarios.thirdRow]++;
                board[BoardPossibleWinScenarios.thirdColumn]++;
                board[BoardPossibleWinScenarios.backDiagonal]++;
                break;
        }
    }

    // Update is called once per frame
    void Update() {
	
	}
}
