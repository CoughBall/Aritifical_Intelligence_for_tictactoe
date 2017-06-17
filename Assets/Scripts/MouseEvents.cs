using UnityEngine;

/// <summary>
/// Handles all the mouse events of the game object
/// </summary>
public class MouseEvents : MonoBehaviour
{

    private GameObject parent;
    private ColorControl colorControl;
    private Position parentPosition;

    /// <summary>
    /// Sets the parent and color control of the gameobject
    /// </summary>
    private void Awake()
    {
        //get parent
        parent = GameObject.Find(gameObject.transform.parent.gameObject.name);
        parentPosition = parent.GetComponent<Position>();
        //get renderer
        colorControl = GetComponent<ColorControl>();
    }

    /// <summary>
    /// When hovering with mouse, highlight the selection
    /// </summary>
    private void OnMouseEnter()
    {
        if (parentPosition.isClicked == false && !colorControl.IsPositionInvisible())
        {
            colorControl.SetColor(colorControl.BaseColor);
        }
    }

    /// <summary>
    /// When hovering out of focus with mouse, unhighlight the selection
    /// </summary>
    private void OnMouseExit()
    {
        if (parentPosition.isClicked == false && !colorControl.IsPositionInvisible())
        {
            colorControl.SetColor(colorControl.AlphaColor);
        }
    }

    /// <summary>
    /// 1.Set player choice for the first time<para/> 
    /// 2.Render the position clicked, visually and logically<para/> 
    /// 3.play the AI turn<para/> 
    /// 4.check if the game is over<para/> 
    /// </summary>
    private void OnMouseDown()
    {
        if (!isPositionAlreadyClicked())
        {
            GameObject scripts = GameObject.Find("Scripts");
            GameController gameController = (GameController)scripts.GetComponent(typeof(GameController));
            if (parentPosition.isClicked == false)
            {
                setPlayerXorCircleChoice();
                if (isCurrentSelectionPlayersChoice())
                {
                    renderPositionClicked(gameObject);
                    gameController.IncrementPositionScore(parent.name, GameController.playerBoard);
                }
                gameController.PlayAiTurn(gameObject);
                gameController.IncrementPositionScore(parent.name, GameController.opponentBoard);
                gameController.IsGameOver();
            }
        }
    }

    private bool isPositionAlreadyClicked()
    {
        return colorControl.InvisibleColor.ToString().Equals(colorControl.GetColor().ToString());
    }

    /// <summary>
    /// Set position clicked and highlight its color so the player sees it
    /// </summary>
    public void renderPositionClicked(GameObject position)
    {
        position.GetComponent<ColorControl>().SetColor(position.GetComponent<ColorControl>().BaseColor);
        parent = GameObject.Find(position.transform.parent.gameObject.name);
        parent.GetComponent<Position>().isClicked = true;
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
            colorControl.ClearBoardOfNonePlayerAvailableChoices();
        }
    }

}
