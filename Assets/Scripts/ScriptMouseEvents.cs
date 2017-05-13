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
                findAFreePositionOnTheBoardAndClickIt();
                other.incrementPositionScore(parent.name, GameController.opponentBoard);
                other.isGameOver();
            }
        }
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
