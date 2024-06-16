using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActionCS : MonoBehaviour, IAction
{
    float deg; //각도
    float nowDeg;
    float vector;
    bool isShow = false;
    bool isOpen = true;
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
        Init(isOpen == false ? true : false);
        GetAngle(playerTr);
        if (isOpen)
        {
            StartCoroutine(DoorClose());
        }
        else
        {
            StartCoroutine(DoorOpen());
        }
    }

    private void Init(bool isOpen)
    {
        nowDeg = 0;
        this.isOpen = isOpen;
    }

    private IEnumerator DoorClose()
    {
        if (!isOpen) yield break;
        yield return new WaitForFixedUpdate();
        if (transform.localRotation.y < 0)
        {
            nowDeg += objSpeed * Time.deltaTime;
        }
        else
        {
            nowDeg -= objSpeed * Time.deltaTime;
        }
        transform.RotateAround(rotatePos.position, Vector3.up, nowDeg);

        if (Mathf.Abs(transform.localRotation.y) < Quaternion.Euler(0f, 1f, 0f).y)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            StartCoroutine(DoorClose());
        }
    }

    private IEnumerator DoorOpen()
    {
        if (isOpen) yield break;
        yield return new WaitForFixedUpdate();

        nowDeg += objSpeed * Time.deltaTime * vector;
        transform.RotateAround(rotatePos.position, Vector3.up, nowDeg);
        if (Mathf.Abs(transform.localRotation.y) > Quaternion.Euler(0f, 89f, 0f).y)
        {
            transform.localRotation = Quaternion.Euler(0f, 90.0f * vector, 0f);
        }
        else
        {
            StartCoroutine(DoorOpen());
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
        Vector3 v1 = transform.position - rotatePos.position;
        Vector3 v2 = transform.position - playerTr.position;

        float crossProduct = v1.x * v2.z - v1.z * v2.x;

        print(crossProduct);
        if (crossProduct >= 0)
        {
            vector = -1f;
        }
        else
        {
            vector = 1f;
        }
    }
}
