using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool _PlayerWon = false;
    public bool _PlayerLost = false;
    [SerializeField] StateMachine _stateMachine;
    [SerializeField] PauseFunction _pause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_PlayerWon == true)
        {
            _stateMachine.ChangeState<WinState>();
        }
        else if (_PlayerLost == true)
        {
            _stateMachine.ChangeState<LoseState>();
        }

        if (_stateMachine.CurrentState == _stateMachine.GetComponent<PlayerTurnState>())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pause.Pause();
            }
        }
    }

    public void TempWinCondition()
    {
        _PlayerWon = true;
        _PlayerLost = false;
    }

    public void TempLoseCondition()
    {
        _PlayerWon = false;
        _PlayerLost = true;
    }
}
