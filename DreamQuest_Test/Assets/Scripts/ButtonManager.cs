using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public int colorCode;
    //click on button to drag a cube
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.CreateCube(colorCode);
    }
    //cancle dragging if release the mouse pointer on the button
    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.Instance.CancelCreatingCube();
    }
}
