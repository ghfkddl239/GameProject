using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCS : MonoBehaviour
{
    public float raycastDistance = 3.0f;
    public LayerMask layerMask;

    private RaycastHit hit;
    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward); //보고있는 방향으로 살펴보기

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red);

        if (Physics.Raycast(ray, out hit, raycastDistance, layerMask)) //인식할 수 있는 범위 안에서 물체 확인
        {
            IAction actionCS = hit.collider.gameObject.GetComponent<IAction>(); //주변 물체의 정보를 가져옵니다.

            if (actionCS is not null) //물체가 있을 경우
            {
                actionCS.ShowOutline();

                if (Input.GetKeyDown(KeyCode.G))
                {
                    actionCS.Action(transform);
                }
            }
        }
    }
}
