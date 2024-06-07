using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;

public class MapPlacementObserverCS : MonoBehaviour, IObserver
{
    MapPlacementSubjectCS subject;
    private Vector3 parentPos = Vector3.zero;

    public enum MapRole
    {
        NOMAL,
        PLAYERSPAWN,
        EXIT
    }

    private MapRole _curRole = MapRole.NOMAL;

    public MapRole curRole
    {
        get
        {
            return _curRole;
        }
    }
    private bool drawGizmo = false;

    void Awake()
    {
        subject = gameObject.GetComponentInParent<MapPlacementSubjectCS>();
    }
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
        drawGizmo = subject.IsDebuged();
        parentPos = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateData(int step)
    {
        // step
        //  0 = playerSpawnCheck && exitCheck,
        //  1 = player && objSpawn
        switch (step)
        {
            case 0:
                CheckDistance();
                subject.UpdateData();
                break;
            case 1:
                SpawnObj();
                subject.UpdateData();
                break;
            case 2:
                break;
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmo) return;
        Vector3 pos = transform.position;
        Vector3 debugPos = pos;
        Transform debug = transform.Find("Bounds").GetChild(0);
        Vector3 debugSize = Vector3.one;
        float yRotate = transform.rotation.eulerAngles.y;
        if (debug is not null)
        {
            debugPos = debug.position;
            debugSize = new Vector3(debug.localScale.x, 4, debug.localScale.z);
            if (0 != yRotate)
            {
                debugSize = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * debugSize;
            }
        }

        float distance = Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(parentPos.x, parentPos.z));

        if (150 < distance)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(debugPos, debugSize);
        }
        else if (30 > distance)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(debugPos, debugSize);
        }
    }

    private void CheckDistance()
    {
        Vector3 pos = transform.position;
        float distance = Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(parentPos.x, parentPos.z));
        float exitSpawnRound = subject.exitSpawnRound;
        float playerSpawnRound = subject.playerSpawnRound;

        if (exitSpawnRound < distance)
        {
            _curRole = MapRole.EXIT;
        }
        else if (playerSpawnRound > distance)
        {
            _curRole = MapRole.PLAYERSPAWN;
        }
    }

    private void SpawnObj()
    {
        //print("아이템 생성");
        print("아이템 생성");

        switch (_curRole)
        {
            case MapRole.NOMAL:
                //print("몹 생성");
                break;
            case MapRole.PLAYERSPAWN:
                //print("플레이어 스폰 지점 생성");
                break;
            case MapRole.EXIT:
                //print("나가는 장소 생성");
                //print("몹 생성");
                break;
        }
    }
}
