using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceSelectionPointers : MonoBehaviour
{
    void OnMouseEnter()
    {
        GetComponent<Image>().color = new Color(255f/255f,255f/255f,255f/255f,255f/255f);
    }

    void OnMouseExit()
    {
        GetComponent<Image>().color = new Color(175f/255f,175f/255f,175f/255f,255f/255f);
    }
}
