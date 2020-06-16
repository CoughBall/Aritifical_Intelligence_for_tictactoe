using UnityEngine;
using System.Collections;
using System;

public class TouchEvents : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchPos = new Vector2(touchPosition.x, touchPosition.y);

            // Handle finger movements based on TouchPhase
            if (touch.phase == TouchPhase.Ended)
            {
                GameObject child = getClickedChild(touchPos);
                if (null != child && !isPositionAlreadyClicked(child))
                {
                    if (!GetComponent<Position>().isClicked)
                    {
                        GameObject scripts = GameObject.Find("Scripts");
                        GameController gameController = (GameController)scripts.GetComponent(typeof(GameController));
                        if (GetComponent<Position>().isClicked == false)
                        {
                            setPlayerXorCircleChoice(child);
                            if (isCurrentSelectionPlayersChoice(child))
                            {
                                renderPositionClicked(child);
                                gameController.IncrementPositionScore(name, gameController.playerBoard);
                            }
                            int aiPosition = gameController.PlayAiTurn(child.gameObject);
                            gameController.IncrementPositionScore(gameController.boardIntegerStringMapper[aiPosition], gameController.opponentBoard);
                            gameController.IsGameOver();
                        }
                    }
                    
                }
            }
        }
    }

    private GameObject getClickedChild(Vector2 touchPos)
    {
        foreach(Transform child in gameObject.transform)
        {
            if (child.GetComponent<PolygonCollider2D>().OverlapPoint(touchPos))
                return child.gameObject;
        }
        return null;
    }

    private bool isPositionAlreadyClicked(GameObject child)
    {
        return child.GetComponent<ColorControl>().InvisibleColor.ToString().Equals(child.GetComponent<ColorControl>().GetColor().ToString());
    }

    private void setPlayerXorCircleChoice(GameObject child)
    {
        if (GameController.playersChoice == "")
        {
            GameController.playersChoice = child.name;
            child.GetComponent<ColorControl>().ClearBoardOfNonePlayerAvailableChoices();
        }
    }

    private bool isCurrentSelectionPlayersChoice(GameObject child)
    {
        return GameController.playersChoice == child.name;
    }

    public void renderPositionClicked(GameObject child)
    {
        child.GetComponent<ColorControl>().SetColor(child.GetComponent<ColorControl>().BaseColor);
        GetComponent<Position>().isClicked = true;
    }
}
