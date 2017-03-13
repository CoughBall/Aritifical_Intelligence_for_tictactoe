using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

    public void LoadScene(int level) {
    Application.LoadLevel(level);
    }
}
