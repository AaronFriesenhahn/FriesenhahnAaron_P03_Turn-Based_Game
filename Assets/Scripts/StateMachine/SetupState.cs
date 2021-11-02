using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupState : TurnBasedGameState
{
    [SerializeField] int _numberOfPlayers = 2;
    [SerializeField] int _numberOfPawns = 10;

    [SerializeField] float _pauseDuration = 1.5f;
    [SerializeField] Text _WinConditionText = null;
    [SerializeField] Text _LoseConditionText = null;

    [SerializeField] Text _PlayerTurnsText = null;
    [SerializeField] GameObject _EndTurnButton = null;
    bool _activated = true;

    public override void Enter()
    {
        Debug.Log("Setup: ...Entering");
        Debug.Log("Creating " + _numberOfPlayers + " players.");
        Debug.Log("Creating " + _numberOfPawns + " pawns.");
        //CAN'T change state while still in Enter()/Exit() transition!
        //DON'T put ChangeStat<> here.

        //If so, add public void to set _activated to false (this will be called from a button)
        //instead of having it be set here

        StartCoroutine(ShowConditions(_pauseDuration));

        //Have the camera moving up and down the field?

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

    IEnumerator ShowConditions(float pauseDuration)
    {
        _PlayerTurnsText.gameObject.SetActive(false);
        _EndTurnButton.SetActive(false);

        Debug.Log("Win Condition: Defeat all of the Enemy's Units.");
        _WinConditionText.gameObject.SetActive(true);
        yield return new WaitForSeconds(pauseDuration);
        Debug.Log("Lose Condition: Lose all of Your Units.");
        _LoseConditionText.gameObject.SetActive(true);
        _WinConditionText.gameObject.SetActive(false);
        yield return new WaitForSeconds(pauseDuration);
        _LoseConditionText.gameObject.SetActive(false);

        _PlayerTurnsText.gameObject.SetActive(true);
        _EndTurnButton.SetActive(true);

        _activated = false;
    }
    



}
