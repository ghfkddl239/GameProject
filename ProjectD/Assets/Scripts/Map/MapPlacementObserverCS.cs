using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;

public class MapPlacementObserverCS : MonoBehaviour, IObserver
{
    MapPlacementSubjectCS subject;
    private enum mapRole
    {
        NOMAL,
        PLAYERSPAWN,
        EXIT
    }

    private mapRole _curRole;

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
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateData(int state)
    {
        //if (0 == state) subject.UpdateData(0);
        subject.UpdateData(CheckDistance());
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 prentPos = transform.parent.position;
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

        float distance = Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(prentPos.x, prentPos.z));

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

    private int CheckDistance()
    {
        // placementType 0 = Nomal, 1 = PlaterSpawnRoom, 2 = ExitRoom
        int placementType = 0;
        Vector3 prentPos = transform.parent.position;
        Vector3 pos = transform.position;
        float distance = Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(prentPos.x, prentPos.z));

        if (150 < distance)
        {
            placementType = 2;
        }
        else if (30 > distance)
        {
            placementType = 1;
        }

        return placementType;
    }
}
