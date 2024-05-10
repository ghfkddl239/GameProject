using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheckerCS : MonoBehaviour
{
    [Header("Boxcast Property")]
    [SerializeField] private Vector3 boxSize;
    //[SerializeField] private float maxDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform zeroPos;

    [Header("Debug")]
    [SerializeField] private bool drawGizmo;

    RaycastHit hit;
    Vector3 fixedPos;
    float maxDistance;

    private void OnDrawGizmos()
    {
        if (!drawGizmo) return;
        fixedPos = zeroPos.position;
        maxDistance = zeroPos.position.y - transform.position.y + 0.1f;
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
        if (Physics.BoxCast(fixedPos, boxSize / 2, Vector3.down, out hit, transform.rotation, maxDistance, groundLayer))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(fixedPos, Vector3.down * hit.distance);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(fixedPos + Vector3.down * hit.distance, boxSize);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
    }

    public bool IsGrounded()
    {
        fixedPos = zeroPos.position;
        maxDistance = zeroPos.position.y - transform.position.y + 0.1f;
        return Physics.BoxCast(fixedPos, boxSize / 2, Vector3.down, transform.rotation, maxDistance, groundLayer);
    }
}