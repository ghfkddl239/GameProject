using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActionCS : MonoBehaviour, IAction
{
    float deg; //각도
    float nowDeg;
    float vector;
    bool isShow = false;
    bool isOpen = false;
    float showTimer = 1;

    public float objSpeed; //원운동 속도
    public Transform rotatePos;
    public Material outlineHighlight;

    // Start is called before the first frame update
    void Start()
    {
        deg = 90.0f;
        nowDeg = 0f;
        vector = 1f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Action(Transform playerTr)
    {
        GetAngle(playerTr);
        StartCoroutine(DoorOpenAndClose());
    }

    private IEnumerator DoorOpenAndClose()
    {
        yield return new WaitForFixedUpdate();
        print(isOpen);
        if (isOpen)
        {
            if (transform.localEulerAngles.y < 0)
            {
                nowDeg += objSpeed * Time.deltaTime;
            }
            else
            {
                nowDeg -= objSpeed * Time.deltaTime;
            }
            print("AAA / " + nowDeg);
            transform.RotateAround(rotatePos.position, Vector3.up, nowDeg);

            if (Mathf.Abs(transform.localEulerAngles.y) < 0.1f)
            {
                nowDeg = 0;
                isOpen = false;
            }
            else
            {
                StartCoroutine(DoorOpenAndClose());
            }
        }
        else
        {
            nowDeg += objSpeed * Time.deltaTime * vector;
            print("BBB / " + nowDeg);
            transform.RotateAround(rotatePos.position, Vector3.up, nowDeg);
            if (Mathf.Abs(transform.localEulerAngles.y) > 89.9f)
            {
                nowDeg = 0f;
                isOpen = true;
            }
            else
            {
                StartCoroutine(DoorOpenAndClose());
            }
            print(Mathf.Abs(transform.localEulerAngles.y));
        }
    }

    public void ShowOutline()
    {
        showTimer = 1;
        if (isShow) return;
        if (GetComponent<MeshRenderer>() is not null)
        {
            isShow = true;
            GetComponent<MeshRenderer>().materials = new Material[2] { GetComponent<MeshRenderer>().material, outlineHighlight };
            StartCoroutine(ShowCheck());
        }
    }

    private IEnumerator ShowCheck()
    {
        showTimer -= 0.5f;
        if (showTimer <= 0)
        {
            isShow = false;
            GetComponent<MeshRenderer>().materials = new Material[2] { GetComponent<MeshRenderer>().material, null };
        }
        yield return new WaitForSeconds(0.5f);
        if (isShow)
        {
            StartCoroutine(ShowCheck());
        }        
    }

    public void GetAngle(Transform playerTr)
    {
        Vector3 v1 = rotatePos.forward;
        Vector3 v2 = rotatePos.position - playerTr.position;

        Vector3 v = v2 - v1;
        float angle = Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            vector = -1f;
        }
        else if (angle >= 0)
        {
            vector = 1f;
        }
    }
}
