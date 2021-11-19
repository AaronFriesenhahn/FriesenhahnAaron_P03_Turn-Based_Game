using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnState : TurnBasedGameState
{
    [SerializeField] Text _playerTurnTextUI = null;
    [SerializeField] Button _playerEndTurnButton = null;

    [SerializeField] Button _TempPlayerWinButton = null;
    [SerializeField] Button _TempPlayerLoseButton = null;

    [SerializeField] MusicAndAudioController _musicPlayer;

    bool _activated = true;

    int _playerTurnCount = 0;

    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        _playerTurnTextUI.gameObject.SetActive(true);
        _playerEndTurnButton.gameObject.SetActive(true);

        //--------temporary--------
        //_TempPlayerWinButton.gameObject.SetActive(true);
        //_TempPlayerLoseButton.gameObject.SetActive(true);
        //--------------------------

        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();
        //hook into events

        //ends player turn
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        if (_activated == true)
        {
            _activated = false;
            _musicPlayer.PlayerTurnMusicPlay();
        }

    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);
        _playerEndTurnButton.gameObject.SetActive(false);

        //--------temporary--------
        _TempPlayerWinButton.gameObject.SetActive(false);
        _TempPlayerLoseButton.gameObject.SetActive(false);
        //--------------------------

        _activated = true;

        //unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        Debug.Log("Player Turn: Exiting...");
    }

    public void OnPressedConfirm()
    {
        //change to enemy turn state
        StateMachine.ChangeState<EnemyTurnState>();
    }
}
