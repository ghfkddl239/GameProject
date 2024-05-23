using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    Bounds bounds;
    // Start is called before the first frame update
    void Start()
    {
        bounds = GetComponent<MeshFilter>().mesh.bounds;
        bounds.center = new Vector3(0.5f, 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
