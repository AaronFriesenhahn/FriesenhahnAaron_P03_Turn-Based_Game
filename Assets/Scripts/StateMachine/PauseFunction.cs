using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFunction : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _restartLevel;
    [SerializeField] GameObject _continueButton;
    [SerializeField] GameObject _playerTurnTextUI;
    [SerializeField] GameObject _playerEndTurnButton;
    [SerializeField] GameObject _TempPlayerWinButton;
    [SerializeField] GameObject _TempPlayerLoseButton;

    public bool Paused = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        
    }

    public void Pause()
    {
        Debug.Log("Pause State: ...Entering");
        _pauseMenu.SetActive(true);
        _restartLevel.SetActive(true);
        _continueButton.SetActive(true);
        //player Turn Stuff
        _playerTurnTextUI.gameObject.SetActive(false);
        _playerEndTurnButton.gameObject.SetActive(false);

        //--------temporary--------
        _TempPlayerWinButton.gameObject.SetActive(false);
        _TempPlayerLoseButton.gameObject.SetActive(false);
        //
        Paused = true;
    }

    public void Resume()
    {
        Debug.Log("Pause State: Exiting...");
        _pauseMenu.SetActive(false);
        _restartLevel.SetActive(false);
        _continueButton.SetActive(false);
        //player Turn Stuff
        _playerTurnTextUI.gameObject.SetActive(true);
        _playerEndTurnButton.gameObject.SetActive(true);

        //--------temporary--------
        _TempPlayerWinButton.gameObject.SetActive(true);
        _TempPlayerLoseButton.gameObject.SetActive(true);
        //
        Paused = false;
    }
}
