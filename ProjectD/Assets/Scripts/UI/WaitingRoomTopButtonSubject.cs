using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class WaitingRoomTopButtonSubject : MonoBehaviour, Isubject
    {
        private readonly List<IObserver> _observers = new List<IObserver>();

        // Start is called before the first frame update
        void Start()
        {

        }
        // Update is called once per frame
        void Update()
        {

        }
        public void ResisterObserver(IObserver opserver)
        {
            _observers.Add(opserver);
        }
        public void RemoveObserver(IObserver opserver)
        {
            _observers.Remove(opserver);
        }
        public void NotifyObservers()
        {
            foreach (IObserver observer in _observers)
            {
                // ÀÎ¼ö 0 = false, 1 = true
                observer.UpdateData(0);
            }
        }
        public void UpdateData()
        {
            NotifyObservers();
        }
    }
}

