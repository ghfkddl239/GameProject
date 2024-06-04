using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnObserver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Vector3 ItemPos = new Vector3(0.1f, 0.1f, 0.1f);
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position + new Vector3(0, ItemPos.y * 0.5f, 0), ItemPos);
    }
}
