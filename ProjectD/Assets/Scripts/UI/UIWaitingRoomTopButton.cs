using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ObserverPattern
{
    public class UIWaitingRoomTopButton : MonoBehaviour, IObserver, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Image highlight;
        [SerializeField] WaitingRoomTopButtonSubject subject;
        [SerializeField] Color hightlightText = Color.yellow;
        [SerializeField] Color nomalText = Color.white;

        Text text;
        bool isSelected = false;
        void OnEnable()
        {
            subject.ResisterObserver(this);
        }

        void OnDisable()
        {
            subject.RemoveObserver(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            highlight.GetComponent<Image>().enabled = false;
            text = gameObject.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateData(bool isSelected)
        {
            this.isSelected = isSelected;
            if (isSelected) return;
            highlight.GetComponent<Image>().enabled = isSelected;
            text.color = nomalText;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isSelected) return;
            highlight.GetComponent<Image>().enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isSelected) return;
            highlight.GetComponent<Image>().enabled = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            isSelected = true;
            subject.UpdateData();
            highlight.GetComponent<Image>().enabled = true;
            text.color = hightlightText;
        }
    }

}
