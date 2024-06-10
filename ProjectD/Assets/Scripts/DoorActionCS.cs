using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActionCS : MonoBehaviour, IAction
{
    float deg; //각도
    float nowDeg;

    public float objSpeed; //원운동 속도
    public Transform rotatePos;

    // Start is called before the first frame update
    void Start()
    {
        print(transform.localEulerAngles.y);
        deg = 90.0f;
        nowDeg = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.localEulerAngles.y) < deg)
        {
            nowDeg += objSpeed * Time.deltaTime;
            DoorOpen(nowDeg);
        }
    }

    public void Action()
    {
        deg = deg == 90.0f ? 0 : 90.0f;
    }

    private void DoorOpen(float deg)
    {
        transform.RotateAround(rotatePos.position, Vector3.up, deg);
    }
}
