using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class hoverOverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string message;
    //private string message = "���d��T \n�W������:12345 \n �̰�����:12345";

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("Hover over text show up");
        hoverOverManager._instance.SetAndShowHoverOver(message);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log("Hover over text gone");
        hoverOverManager._instance.HideHoverOver();
    }

}
