using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITitleButtons : UI
{
    [SerializeField]
    Transform buttonText;

    Transform leftEffect;
    Transform rightEffect;

    Animator textAnimator;

    private void Awake()
    {
        leftEffect = transform.Find("LeftEffect");
        rightEffect = transform.Find("RightEffect");
        textAnimator = buttonText.gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        leftEffect.gameObject.SetActive(false);
        rightEffect.gameObject.SetActive(false);
        textAnimator.speed = 0;
    }

    protected override void Action(PointerEventData eventData, string EventName)
    {
        switch(EventName)
        {
            case "Enter" :
                leftEffect.gameObject.SetActive(true);
                rightEffect.gameObject.SetActive(true);
                break;
            case "Exit":
                leftEffect.gameObject.SetActive(false);
                rightEffect.gameObject.SetActive(false);
                break;
            case "Click":
                textAnimator.speed = 1;
                textAnimator.Play("ButtonClick", -1, 0f);
                break;
            case "BeginDrag":
                break;
            case "EndDrag":
                break;
        }
    }
}
