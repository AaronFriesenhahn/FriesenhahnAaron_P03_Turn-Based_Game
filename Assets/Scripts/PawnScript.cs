using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour
{
    [SerializeField] TurnBasedGameSM _stateMachine = null;
    [SerializeField] Canvas _PlayerOptions = null;
    [SerializeField] PauseFunction _pause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<PlayerTurnState>())
        {
            if (_pause.Paused == false)
            {
                //TODO if pawn is selected, turn on _PlayerOptions
                _PlayerOptions.gameObject.SetActive(true);
            }
            else
            {
                _PlayerOptions.gameObject.SetActive(false);
            }
        }
        else
        {
            _PlayerOptions.gameObject.SetActive(false);
        }
    }
}
