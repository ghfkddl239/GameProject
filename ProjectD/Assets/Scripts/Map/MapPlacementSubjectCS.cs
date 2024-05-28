using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using UnityEngine.SceneManagement;

public class MapPlacementSubjectCS : MonoBehaviour, Isubject
{
    private readonly List<IObserver> _observers = new List<IObserver>();
    private List<int> rooms = new List<int>();
    int curStep = 0;
    int playerSpawnRooms = 0;
    int exitRooms = 0;

    // Start is called before the first frame update
    void Start()
    {
        NotifyObservers();
    }

    // Update is called once per frame
    void Update()
    {
        //print(transform.childCount + " / " + _observers.Count);
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
        rooms.Clear();
        foreach (IObserver observer in _observers)
        {
            // step 0 = start, 1 = playerSpawnCheck && exitCheck
            observer.UpdateData(curStep);
        }
        curStep++;
    }
    public void UpdateData(int state)
    {
        rooms.Add(state);

        print(_observers.Count + " / " + rooms.Count);
        if (_observers.Count < 20) ResetMap();
        if (_observers.Count == rooms.Count)
        {
            foreach (int i in rooms)
            {
                if (i == 1)
                {
                    playerSpawnRooms++;
                }
                else if(i == 2)
                {
                    exitRooms++;
                }
            }
            print("playerSpawnRooms : " + playerSpawnRooms + " / exitRooms : " + exitRooms);
            if (0 == exitRooms) ResetMap();
        }
    }

    private void ResetMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
