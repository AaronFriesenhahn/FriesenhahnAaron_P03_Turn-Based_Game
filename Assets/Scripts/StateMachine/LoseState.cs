using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseState : TurnBasedGameState
{
    [SerializeField] Text _LosingText;
    [SerializeField] GameManager _gameManager;

    [SerializeField] Button _RestartGameButton;
    [SerializeField] Button _ReturnToMainMenuButton;

    [SerializeField] MusicAndAudioController _musicPlayer;

    bool _activated = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void Enter()
    {
        Debug.Log("Lose State: ...Entering");
        Debug.Log("Player has lost all of their units.");
        //Display winning text
        _LosingText.gameObject.SetActive(true);
        _RestartGameButton.gameObject.SetActive(true);
        _ReturnToMainMenuButton.gameObject.SetActive(true);
        _LosingText.text = "You lost!";
        Debug.Log("You Lose!");
        _musicPlayer.DefeatMusicPlay();
    }

    public override void Exit()
    {
        Debug.Log("Lose State: Exiting...");
        _gameManager._PlayerLost = false;
        _RestartGameButton.gameObject.SetActive(false);
        _ReturnToMainMenuButton.gameObject.SetActive(false);
        _LosingText.gameObject.SetActive(false);
    }
}
