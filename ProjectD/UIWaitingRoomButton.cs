using System.Collections;
using System.Collections.Generic;
using ObserverPattern;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIWaitingRoomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, Isubject
{
    [SerializeField]
    GameObject highlightImage;

    bool isSelected = false;
    // Start is called before the first frame update

    private readonly List<IOpserver> _observers = new List<IOpserver>();

    void Start()
    {
        highlightImage.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResisterObserver(IOpserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IOpserver observer)
    {
        _observers.Remove(observer);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        highlightImage.GetComponent<Image>().enabled = true;
        isSelected = true;
    }

    public void NotifyObservers()
    {
        foreach(IOpserver observer in _observers)
        {

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightImage.GetComponent<Image>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlightImage.GetComponent<Image>().enabled = false;
    }
}
