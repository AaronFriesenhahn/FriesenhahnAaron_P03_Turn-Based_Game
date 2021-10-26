using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TurnBasedGameSM))]
public class TurnBasedGameState : State
{
    protected TurnBasedGameSM StateMachine { get; private set; }

    void Awake()
    {
        StateMachine = GetComponent<TurnBasedGameSM>();
    }
}
