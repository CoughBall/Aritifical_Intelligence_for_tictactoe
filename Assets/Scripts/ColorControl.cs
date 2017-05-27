using UnityEngine;
using System.Collections;

/**
 *Handles color settings
 *
 */

public class ColorControl : MonoBehaviour {

    public Color32 BaseColor {
        get;
        set;
        }
    public Color32 AlphaColor {
        get;
        set;
        }
    public Color32 InvisibleColor
    {
        get;
        set;
    }

    public void SetColor(Color color) {
    gameObject.GetComponent<Renderer>().material.color = color;
        }

    public Color32 GetColor()
    {
        return gameObject.GetComponent<Renderer>().material.color;
    }

    void Awake() {
        BaseColor = gameObject.GetComponent<Renderer>().material.color;
        AlphaColor = new Color(1f, 1f, 1f, .1f); //sets alpha
        InvisibleColor = new Color(0f, 0f, 0f, 0f);
        gameObject.GetComponent<Renderer>().material.color = AlphaColor;
        }

    // Use this for initialization
    void Start() {

        }

    // Update is called once per frame
    void Update() {

        }
}
