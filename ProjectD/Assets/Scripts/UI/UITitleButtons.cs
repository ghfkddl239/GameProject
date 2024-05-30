using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITitleButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        leftEffect.gameObject.SetActive(true);
        rightEffect.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        leftEffect.gameObject.SetActive(false);
        rightEffect.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        textAnimator.speed = 1;
        textAnimator.Play("ButtonClick", -1, 0f);
    }

    public void GoWaitingRoom()
    {
        LoadingSceneManager.LoadScene("WaitingRoom");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
