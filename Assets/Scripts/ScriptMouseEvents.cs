using UnityEngine;
using System.Collections;

/*
 *Handles mouse events
 */
public class ScriptMouseEvents : MonoBehaviour
{

    private bool isClicked = false;
    private ColorControl render;
    private ScriptPosition parentPosition;
    private bool isPicked;
    private GameObject parent;

    // Use this for initialization
    void Start() {

        }

    // Update is called once per frame
    void Update() {

        }

    void Awake() 
    {
        //get parent
        parent = GameObject.Find(gameObject.transform.parent.gameObject.name);
        parentPosition = parent.GetComponent<ScriptPosition>();
        //get renderer
        render = GetComponent<ColorControl>();
    }

    void OnMouseEnter()
    {
        if (parentPosition.isClicked == false && !isPositionInvisible())
        {
            render.SetColor(render.BaseColor);
        }
    }

    void OnMouseExit()
    {
        if (parentPosition.isClicked == false && !isPositionInvisible())
        {
            render.SetColor(render.AlphaColor);
        }
    }

    private bool isPositionInvisible()
    {
        Color32 positionCurrentColor = render.GetColor();
        Color32 invisibleColor = GetComponent<ColorControl>().InvisibleColor;
        return positionCurrentColor.ToString().Equals(invisibleColor.ToString());
    }

    void OnMouseDown()
    {
        if (!isPositionAlreadyClicked())
        {
            GameObject go = GameObject.Find("Scripts");
            GameController other = (GameController)go.GetComponent(typeof(GameController));
            if (parentPosition.isClicked == false)
            {
                setPlayerXorCircleChoice();
                if (isCurrentSelectionPlayersChoice())
                {
                    renderPositionClicked(gameObject);
                    other.incrementPositionScore(parent.name, GameController.playerBoard);
                }


                MiniMax m = new MiniMax();
                int position;
                if (GameController.playersChoice.Equals("X"))
                {
                    position = -1;
                }
                else
                    position = 1;

                System.Boolean bla = true;
                //MiniMax.Position bestPosition = new MiniMax.Position();
                MiniMax.Position p = m.getBestPosition(position, convertBoardToInt(), position, 0, 0/*, bestPosition*/);
                int newPosition = p.position;
                Debug.Log(p.isWin);
                foreach (GameObject position1 in GameController.positionArr)
                {
                    if(newPosition == 0 && position1.name == "[0,0]")
                    {
                        if (GameController.playersChoice != position1.transform.GetChild(0).gameObject.name)
                        {
                            renderPositionClicked(position1.transform.GetChild(0).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                        else
                        {
                            renderPositionClicked(position1.transform.GetChild(1).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                    }
                    if (newPosition == 1 && position1.name == "[0,1]")
                    {
                        if (GameController.playersChoice != position1.transform.GetChild(0).gameObject.name)
                        {
                            renderPositionClicked(position1.transform.GetChild(0).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                        else
                        {
                            renderPositionClicked(position1.transform.GetChild(1).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                    }
                    if (newPosition == 2 && position1.name == "[0,2]")
                    {
                        if (GameController.playersChoice != position1.transform.GetChild(0).gameObject.name)
                        {
                            renderPositionClicked(position1.transform.GetChild(0).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                        else
                        {
                            renderPositionClicked(position1.transform.GetChild(1).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                    }
                    if (newPosition == 3 && position1.name == "[1,0]")
                    {
                        if (GameController.playersChoice != position1.transform.GetChild(0).gameObject.name)
                        {
                            renderPositionClicked(position1.transform.GetChild(0).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                        else
                        {
                            renderPositionClicked(position1.transform.GetChild(1).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                    }
                    if (newPosition == 4 && position1.name == "[1,1]")
                    {
                        if (GameController.playersChoice != position1.transform.GetChild(0).gameObject.name)
                        {
                            renderPositionClicked(position1.transform.GetChild(0).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                        else
                        {
                            renderPositionClicked(position1.transform.GetChild(1).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                    }
                    if (newPosition == 5 && position1.name == "[1,2]")
                    {
                        if (GameController.playersChoice != position1.transform.GetChild(0).gameObject.name)
                        {
                            renderPositionClicked(position1.transform.GetChild(0).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                        else
                        {
                            renderPositionClicked(position1.transform.GetChild(1).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                    }
                    if (newPosition == 6 && position1.name == "[2,0]")
                    {
                        if (GameController.playersChoice != position1.transform.GetChild(0).gameObject.name)
                        {
                            renderPositionClicked(position1.transform.GetChild(0).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                        else
                        {
                            renderPositionClicked(position1.transform.GetChild(1).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                    }
                    if (newPosition == 7 && position1.name == "[2,1]")
                    {
                        if (GameController.playersChoice != position1.transform.GetChild(0).gameObject.name)
                        {
                            renderPositionClicked(position1.transform.GetChild(0).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                        else
                        {
                            renderPositionClicked(position1.transform.GetChild(1).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                    }
                    if (newPosition == 8 && position1.name == "[2,2]")
                    {
                        if (GameController.playersChoice != position1.transform.GetChild(0).gameObject.name)
                        {
                            renderPositionClicked(position1.transform.GetChild(0).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                        else
                        {
                            renderPositionClicked(position1.transform.GetChild(1).gameObject);
                            renderAiOtherChoiceInvisible(position1);
                        }
                    }
                }


                   // findAFreePositionOnTheBoardAndClickIt();
                m = null;
                other.incrementPositionScore(parent.name, GameController.opponentBoard);
                other.isGameOver();
            }
        }
    }

    private void renderAiOtherChoiceInvisible(GameObject position)
    {
        Color32 alphaColor = render.AlphaColor;
        Color32 color;
        for (int i = 0; i < position.transform.childCount; i++)
        {
            GameObject c = (GameObject)position.transform.GetChild(i).gameObject;
            color = c.GetComponent<ColorControl>().GetColor();
            if (color.ToString().Equals(alphaColor.ToString()))
             c.GetComponent<ColorControl>().SetColor(render.InvisibleColor);
        }
    }

    private int[] convertBoardToInt()
    {
        Color32 invisibleColor = GetComponent<ColorControl>().InvisibleColor;
        int[] board = new int[9];
        foreach (GameObject position in GameController.positionArr)
        {
                switch (position.name)
                {
                    case "[0,0]":
                    if (!position.GetComponent<ScriptPosition>().isClicked)
                    {
                        board[0] = 0;
                    }
                    else
                    {
                        if (!position.transform.GetChild(0).GetComponent<ColorControl>().GetColor().Equals(invisibleColor) && position.transform.GetChild(0).gameObject.name == "X")
                        {
                            board[0] = 1;

                        }
                        else
                            board[0] = -1;
                    }
                        break;
                    case "[0,1]":
                        if (!position.GetComponent<ScriptPosition>().isClicked)
                        {
                            board[1] = 0;
                        }
                        else
                        {
                            if (!position.transform.GetChild(0).GetComponent<ColorControl>().GetColor().Equals(invisibleColor) && position.transform.GetChild(0).gameObject.name == "X")
                            {
                                    board[1] = 1;
                                
                            }
                             else
                            board[1] = -1;
                    }
                    break;
                    case "[0,2]":
                        if (!position.GetComponent<ScriptPosition>().isClicked)
                        {
                            board[2] = 0;
                        }
                        else
                        {
                        if (!position.transform.GetChild(0).GetComponent<ColorControl>().GetColor().Equals(invisibleColor) && position.transform.GetChild(0).gameObject.name == "X")
                        {
                            board[2] = 1;

                        }
                        else
                            board[2] = -1;
                    }
                    break;
                    case "[1,0]":
                        if (!position.GetComponent<ScriptPosition>().isClicked)
                        {
                            board[3] = 0;
                        }
                        else
                        {
                        if (!position.transform.GetChild(0).GetComponent<ColorControl>().GetColor().Equals(invisibleColor) && position.transform.GetChild(0).gameObject.name == "X")
                        {
                            board[3] = 1;

                        }
                        else
                            board[3] = -1;
                    }
                    break;
                    case "[1,1]":
                        if (!position.GetComponent<ScriptPosition>().isClicked)
                        {
                            board[4] = 0;
                        }
                        else
                        {
                        if (!position.transform.GetChild(0).GetComponent<ColorControl>().GetColor().Equals(invisibleColor) && position.transform.GetChild(0).gameObject.name == "X")
                        {
                            board[4] = 1;

                        }
                        else
                            board[4] = -1;
                    }
                    break;
                    case "[1,2]":
                        if (!position.GetComponent<ScriptPosition>().isClicked)
                        {
                            board[5] = 0;
                        }
                        else
                        {
                            if (!position.transform.GetChild(0).GetComponent<ColorControl>().GetColor().Equals(invisibleColor) && position.transform.GetChild(0).gameObject.name == "X")
                            {
                                board[5] = 1;

                            }
                            else
                                board[5] = -1;
                    }
                    break;
                    case "[2,0]":
                        if (!position.GetComponent<ScriptPosition>().isClicked)
                        {
                            board[6] = 0;
                        }
                        else
                        {
                            if (!position.transform.GetChild(0).GetComponent<ColorControl>().GetColor().Equals(invisibleColor) && position.transform.GetChild(0).gameObject.name == "X")
                            {
                                board[6] = 1;

                            }
                            else
                                board[6] = -1;
                    }
                    break;
                    case "[2,1]":
                        if (!position.GetComponent<ScriptPosition>().isClicked)
                        {
                            board[7] = 0;
                        }
                        else
                        {
                            if (!position.transform.GetChild(0).GetComponent<ColorControl>().GetColor().Equals(invisibleColor) && position.transform.GetChild(0).gameObject.name == "X")
                            {
                                board[7] = 1;

                            }
                            else
                                board[7] = -1;
                    }
                    break;
                    case "[2,2]":
                        if (!position.GetComponent<ScriptPosition>().isClicked)
                        {
                            board[8] = 0;
                        }
                        else
                        {
                            if (!position.transform.GetChild(0).GetComponent<ColorControl>().GetColor().Equals(invisibleColor) && position.transform.GetChild(0).gameObject.name == "X")
                            {
                                board[8] = 1;

                            }
                            else
                                board[8] = -1;
                    }
                    break;
            }
        }
        return board;
    }

    private bool isPositionAlreadyClicked()
    {
        return render.InvisibleColor.ToString().Equals(render.GetColor().ToString());
    }

    private void renderPositionClicked(GameObject position)
    {
        position.GetComponent<ColorControl>().SetColor(position.GetComponent<ColorControl>().BaseColor);
        parent = GameObject.Find(position.transform.parent.gameObject.name);
        parent.GetComponent<ScriptPosition>().isClicked = true;
    }

    private void findAFreePositionOnTheBoardAndClickIt()
    {
        foreach (GameObject position in GameController.positionArr)
        {
            if(!position.GetComponent<ScriptPosition>().isClicked)
            {
                if (GameController.playersChoice != position.transform.GetChild(0).gameObject.name)
                {
                    renderPositionClicked(position.transform.GetChild(0).gameObject);
                }
                else
                {
                    renderPositionClicked(position.transform.GetChild(1).gameObject);
                }
                break;
            }
        }
    }

    private bool isCurrentSelectionPlayersChoice()
    {
        return GameController.playersChoice == gameObject.name;
    }

    private void setPlayerXorCircleChoice()
    {
        if (GameController.playersChoice == "")
        {
            GameController.playersChoice = gameObject.name;
            clearBoardOfNonePlayerAvailableChoices();
        }
    }

    private void clearBoardOfNonePlayerAvailableChoices()
    {
        for (int i = 0; i < GameController.positionArr.Length; i++)
        {
            for (int j = 0; j < GameController.positionArr[i].transform.childCount; j++)
            {
                GameObject xOrCircle = GameController.positionArr[i].transform.GetChild(j).gameObject;
                if (GameController.playersChoice != xOrCircle.name)
                {
                    xOrCircle.GetComponent<ColorControl>().SetColor(render.InvisibleColor);
                }
            }
        }
    }
}
