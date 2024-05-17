using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected ICharacterFSM _character;

    protected BaseState(ICharacterFSM character)
    {
        _character = character;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}
