﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTurnState : TurnBasedGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] float _pauseDuration = 3f;

    public override void Enter()
    {
        Debug.Log("Enemy Turn: ...Enter");
        EnemyTurnBegan?.Invoke();

        //temporary
        //StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("Enemy Turn: Exit...");
        //StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
        PauseBeforeExit();
    }

    public void PauseBeforeExit()
    {
        if (_pauseDuration > 0)
        {
            _pauseDuration -= Time.deltaTime;
        }
        else
        {
            EnemyTurnEnded?.Invoke();
            StateMachine.ChangeState<PlayerTurnState>();
            _pauseDuration = 3f;
        }
    }


    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy thinking...");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Enemy performs action");
        //TODO Enemy moves
        //Code for enemy action

        EnemyTurnEnded?.Invoke();
        //turn over. Go back to Player.
        StateMachine.ChangeState<PlayerTurnState>();
    }
}
