using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActionCS : MonoBehaviour, IAction
{
    float nowDeg;
    float vector;
    bool isShow = false;
    bool isOpen = true;
    float showTimer = 1;

    public float objSpeed; //원운동 속도
    public Transform rotatePos;
    public Material outlineHighlight;

    private readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        nowDeg = 0f;
        vector = 1f;
        meshRenderer = GetComponent<MeshRenderer>();
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
        bool enter = true;
        while (enter)
        {
            yield return waitForFixedUpdate;
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
                enter = false;
            }

            if (!isOpen) break;
        }
    }

    private IEnumerator DoorOpen()
    {
        bool enter = true;
        while (enter)
        {
            yield return waitForFixedUpdate;

            nowDeg += objSpeed * Time.deltaTime * vector;
            transform.RotateAround(rotatePos.position, Vector3.up, nowDeg);

            if (Mathf.Abs(transform.localRotation.y) > Quaternion.Euler(0f, 89f, 0f).y)
            {
                transform.localRotation = Quaternion.Euler(0f, 90.0f * vector, 0f);
                enter = false;
            }

            if (isOpen) break;
        }
    }

    public void ShowOutline()
    {
        showTimer = 1;
        if (isShow) return;
        if (meshRenderer is not null)
        {
            isShow = true;
            meshRenderer.materials = new Material[2] { meshRenderer.material, outlineHighlight };
            StartCoroutine(ShowCheck());
        }
    }

    private IEnumerator ShowCheck()
    {
        bool enter = true;
        while(enter)
        {
            yield return waitForFixedUpdate;
            showTimer -= Time.deltaTime;
            if (showTimer <= 0)
            {
                meshRenderer.materials = new Material[2] { meshRenderer.material, null };
                isShow = false;
                enter = false;
            }
        }
    }

    public void GetAngle(Transform playerTr)
    {
        Vector3 v1 = transform.position - rotatePos.position;
        Vector3 v2 = transform.position - playerTr.position;

        float crossProduct = v1.x * v2.z - v1.z * v2.x;

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
