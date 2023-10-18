using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public int colorCode;
    public void OnPointerDown(PointerEventData eventData)
    {
        
        GameManager.Instance.CreateCube(colorCode);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.Instance.CancelCreatingCube();
    }
}
