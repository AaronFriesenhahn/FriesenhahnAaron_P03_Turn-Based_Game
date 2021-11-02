using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinState : TurnBasedGameState
{
    [SerializeField] Text _WinningText;
    [SerializeField] GameManager _gameManager;

    [SerializeField] Button _RestartGameButton;

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
        Debug.Log("Win State: ...Entering");
        Debug.Log("Player has defeated all of the enemies.");
        //Display winning text
        _WinningText.gameObject.SetActive(true);
        _RestartGameButton.gameObject.SetActive(true);
        _WinningText.text = "You won!";
        Debug.Log("You win!");
    }

    public override void Exit()
    {
        Debug.Log("Win State: Exiting...");
        _gameManager._PlayerWon = false;
        _WinningText.gameObject.SetActive(false);
        _RestartGameButton.gameObject.SetActive(false);
    }
}
