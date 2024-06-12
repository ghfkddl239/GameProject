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
        ray = new Ray(transform.position, transform.forward); //�����ִ� �������� ���캸��

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red);

        if (Physics.Raycast(ray, out hit, raycastDistance, layerMask)) //�ν��� �� �ִ� ���� �ȿ��� ��ü Ȯ��
        {
            IAction actionCS = hit.collider.gameObject.GetComponent<IAction>(); //�ֺ� ��ü�� ������ �����ɴϴ�.

            if (actionCS is not null) //��ü�� ���� ���
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
