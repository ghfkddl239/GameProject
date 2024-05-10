using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCS : MonoBehaviour
{
    [SerializeField] PlayerCameraMoveCS playerCamera;
    Animator animator;
    CharacterController characterContrl;
    PlayerGroundCheckerCS groundCheckerCS;
    Vector3 dir;

    public float speed = 1.0f;
    bool characterIsGround = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterContrl = GetComponent<CharacterController>();
        groundCheckerCS = GetComponent<PlayerGroundCheckerCS>();
    }
    // Start is called before the first frame update
    void Start()
    {
        characterIsGround = groundCheckerCS.IsGrounded();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterIsGround)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            dir = new Vector3(h, 0, v) * speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                dir.y = 7.5f;
            }
        }
        dir = transform.TransformDirection(dir);
        //ÁÂ¿ì È¸Àü
        transform.eulerAngles = playerCamera.CameraYRotate();
        dir.y += Physics.gravity.y + Time.deltaTime;
        characterContrl.Move(dir * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        characterIsGround = groundCheckerCS.IsGrounded();
    }
}
