using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Action(eventData, "Click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Action(eventData, "Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Action(eventData, "Exit");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Action(eventData, "BeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Action(eventData, "EndDrag");
    }

    protected virtual void Action(PointerEventData eventData, string EventName)
    {

    }

}
