using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    private BaseState _curState;
    public FSM(BaseState initstate)
    {
        _curState = initstate;
    }
    public void ChangeState(BaseState nextState)
    {
        if (nextState == _curState) return;

        if (_curState is not null) _curState.OnStateExit();

        _curState = nextState;
        _curState.OnStateEnter();
    }

    public void UpdateState()
    {
        if (_curState is not null) _curState.OnStateUpdate();
    }
}
