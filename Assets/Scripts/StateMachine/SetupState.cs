using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupState : TurnBasedGameState
{
    [SerializeField] int _numberOfPlayers = 2;
    [SerializeField] int _numberOfPawns = 10;

    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("Setup: ...Entering");
        Debug.Log("Creating " + _numberOfPlayers + " players.");
        Debug.Log("Creating " + _numberOfPawns + " pawns.");
        //CAN'T change state while still in Enter()/Exit() transition!
        //DON'T put ChangeStat<> here.
        _activated = false;
    }

    public override void Update()
    {
        if (_activated == false)
        {
            _activated = true;
            StateMachine.ChangeState<PlayerTurnState>();
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("Setup: Exiting...");
    }
}
