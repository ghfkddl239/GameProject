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

    public float playerSpawnRound;
    public float exitSpawnRound;

    [SerializeField] GameObject player;

    [Header("Boxcast Property")]
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private LayerMask groundLayer;

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
                    PlayerSpawn();
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

    private void PlayerSpawn()
    {
        Vector3 playerSpawnPos = new Vector3();
        Vector3 spawnPos = new Vector3();
        bool isFit = false;
        int max = 0;
        while (!isFit)
        {
            spawnPos = transform.position + (Random.insideUnitSphere * playerSpawnRound);
            spawnPos.y = 4.0f;
            print(spawnPos);
            if (Physics.BoxCast(spawnPos, boxSize / 2, Vector3.down, transform.rotation, 10.0f, groundLayer))
            {
                playerSpawnPos = spawnPos;
                isFit = true;
            }

            if (max > 100)
            {
                playerSpawnPos = Vector3.zero;
                isFit = true;
            }
            max++;
        }
        playerSpawnPos.y = 0.1f;

        if (isFit) Instantiate(player, playerSpawnPos, Quaternion.identity);
    }

    /*
    List<Vector3> poss = new List<Vector3>();

    private void PlayerSpawn()
    {
        Vector3 playerSpawnPos = new Vector3();

        for (int i = 0; i < 10; i++)
        {
            playerSpawnPos = transform.position + (Random.insideUnitSphere * playerSpawnRound);
            playerSpawnPos.y = 4.0f;
            poss.Add(playerSpawnPos);
        }
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmo) return;
        if (0 == poss.Count) return;
        RaycastHit hit;

        for (int i = 0; i < poss.Count; i++)
        {
            Physics.BoxCast(poss[i], boxSize / 2, Vector3.down, out hit, transform.rotation, 10.0f, groundLayer);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(poss[i], Vector3.down * hit.distance);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(poss[i] + Vector3.down * hit.distance, boxSize);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
    }
    */
}
