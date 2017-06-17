using UnityEngine;

/**
 *Handles color settings
 *
 */
public class ColorControl : MonoBehaviour
{

    public Color32 BaseColor
    {
        get;
        set;
    }

    public Color32 AlphaColor
    {
        get;
        set;
    }

    public Color32 InvisibleColor
    {
        get;
        set;
    }

    public void SetColor(Color color)
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    public Color32 GetColor()
    {
        return gameObject.GetComponent<Renderer>().material.color;
    }

    private void Awake()
    {
        AlphaColor = new Color(1f, 1f, 1f, .1f); //sets alpha
        BaseColor = gameObject.GetComponent<Renderer>().material.color;
        InvisibleColor = new Color(0f, 0f, 0f, 0f);
        gameObject.GetComponent<Renderer>().material.color = AlphaColor;
    }

    public bool IsPositionInvisible()
    {
        Color32 positionCurrentColor = GetColor();
        Color32 invisibleColor = this.InvisibleColor;
        return positionCurrentColor.ToString().Equals(invisibleColor.ToString());
    }

    public void ClearBoardOfNonePlayerAvailableChoices()
    {
        GameObject scripts = GameObject.Find("Scripts");
        GameController gameController = (GameController)scripts.GetComponent(typeof(GameController));
        for (int i = 0; i < gameController.positionArr.Length; i++)
        {
            for (int j = 0; j < gameController.positionArr[i].transform.childCount; j++)
            {
                GameObject xOrCircle = gameController.positionArr[i].transform.GetChild(j).gameObject;
                if (GameController.playersChoice != xOrCircle.name)
                {
                    xOrCircle.GetComponent<ColorControl>().SetColor(this.InvisibleColor);
                }
            }
        }
    }

    /// <summary>
    /// Will render the AI choices as Invisible, the player does not need to see them
    /// </summary>
    public void RenderAiOtherChoiceInvisible(GameObject position)
    {
        Color32 xOrCircleColor;
        for (int i = 0; i < position.transform.childCount; i++)
        {
            GameObject xOrCircle = (GameObject)position.transform.GetChild(i).gameObject;
            xOrCircleColor = xOrCircle.GetComponent<ColorControl>().GetColor();
            if (xOrCircleColor.ToString().Equals(AlphaColor.ToString()))
            {
                xOrCircle.GetComponent<ColorControl>().SetColor(InvisibleColor);
            }
        }
    }

}
