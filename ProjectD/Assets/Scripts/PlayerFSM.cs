using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour, ICharacterFSM
{
    private enum state
    {
        IDLE,
        MOVE,
        ATTACK,
        DEFENSE,
        DAMAGED
    }

    private state _curState;
    private FSM _fsm;
    private Animator animator;
    public string test = "";


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _curState = state.IDLE;
        _fsm = new FSM(new IdleState(this));
        test = "IDLE";
    }

    private void Update()
    {
        switch(_curState)
        {
            case state.IDLE:
                if ("MOVE" == test)
                {
                    ChangeState(state.MOVE);
                }
                else if ("ATTACK" == test)
                {
                    ChangeState(state.ATTACK);
                }
                break;
            case state.MOVE:
                if ("ATTACK" == test)
                {
                    ChangeState(state.ATTACK);
                }
                break;
            case state.ATTACK:
                if ("MOVE" == test)
                {
                    ChangeState(state.MOVE);
                }
                else if ("IDLE" == test)
                {
                    ChangeState(state.IDLE);
                }
                break;
            case state.DEFENSE:
                print("방어상태");
                break;
        }

        _fsm.UpdateState();
    }

    private void ChangeState(state nextState)
    {
        _curState = nextState;
        switch(_curState)
        {
            case state.IDLE:
                _fsm.ChangeState(new IdleState(this));
                animator.SetTrigger("Idle");
                break;
            case state.MOVE:
                _fsm.ChangeState(new MoveState(this));
                animator.SetTrigger("Walk");
                break;
            case state.ATTACK:
                _fsm.ChangeState(new AttackState(this));
                animator.SetTrigger("Attack");
                break;
        }
    }

}
