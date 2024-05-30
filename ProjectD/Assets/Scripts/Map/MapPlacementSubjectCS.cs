using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using UnityEngine.SceneManagement;

public class MapPlacementSubjectCS : MonoBehaviour, Isubject
{
    private readonly List<IObserver> _observers = new List<IObserver>();
    private List<GameObject> rooms = new List<GameObject>();
    int curStep = 0;
    int callbackRooms = 0;
    bool notifyIsOver = false;
    public static int resetTest = 0;

    [Header("Debug")]
    [SerializeField] private bool drawGizmo;
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

    public bool IsDebuged()
    {
        return drawGizmo;
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
        callbackRooms = 0;

        foreach (IObserver observer in _observers)
        {
            // step
            //  0 = playerSpawnCheck && exitCheck,
            //  1 = player && objSpawn
            observer.UpdateData(curStep);
        }

        curStep++;
        notifyIsOver = true;
    }

    IEnumerator NotifyObserversCoroutine()
    {
        yield return null;

        while (!notifyIsOver)
        {
            yield return new WaitForEndOfFrame();
        }

        notifyIsOver = false;
        NotifyObservers();
    }

    public void UpdateData()
    {
        callbackRooms++;

        if (_observers.Count == callbackRooms)
        {
            switch (curStep)
            {
                case 0:
                    if (!RoomsCheck())
                    {
                        ResetMap();
                        return;
                    }
                    break;
                case 1:
                    //return;
                    break;
            }
            StartCoroutine(NotifyObserversCoroutine());
        }
    }

    private bool RoomsCheck()
    {
        int exitRooms = 0;
        if (_observers.Count < 20)
        {
            return false;
        }
        else
        {
            foreach (MapPlacementObserverCS observer in _observers)
            {
                if (MapPlacementObserverCS.MapRole.EXIT == observer.curRole)
                {
                    exitRooms++;
                }
            }

            if (0 == exitRooms)
            {
                return false;
            }
        }
        return true;
    }
    private void ResetMap()
    {
        if (resetTest < 3)
        {
            resetTest++;            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            //LoadingSceneManager.LoadScene("WaitingRoom");
        }
    }
}
