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
    [SerializeField] public GameObject _pawnSelected;

    [SerializeField] GameObject[] _enemyTeam;
    [SerializeField] GameObject[] _playerTeam;

    // Start is called before the first frame update
    void Start()
    {
        _enemyTeam = GameObject.FindGameObjectsWithTag("EnemyPawn");
        _playerTeam = GameObject.FindGameObjectsWithTag("PlayerPawn");
    }

    // Update is called once per frame
    void Update()
    {
        DetectWinLoseState();
        TrackEnemyandPlayerPawns();

        if (_stateMachine.CurrentState == _stateMachine.GetComponent<PlayerTurnState>())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _pause.Pause();
            }
        }
        else
        {
            //_pawnSelected = null;
        }
    }

    public void TrackEnemyandPlayerPawns()
    {
        _enemyTeam = GameObject.FindGameObjectsWithTag("EnemyPawn");
        _playerTeam = GameObject.FindGameObjectsWithTag("PlayerPawn");
        if (_enemyTeam.Length == 0)
        {
            _PlayerWon = true;
            Debug.Log("PlayerWins?");
        }
        else if (_playerTeam.Length == 0)
        {
            _PlayerLost = true;
        }
    }

    public void DetectWinLoseState()
    {
        if (_PlayerWon == true)
        {
            _stateMachine.ChangeState<WinState>();
        }
        else if (_PlayerLost == true)
        {
            _stateMachine.ChangeState<LoseState>();
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
