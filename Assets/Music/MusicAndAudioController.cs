using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAndAudioController : MonoBehaviour
{
    [SerializeField] AudioClip PlayerTurn;
    [SerializeField] AudioClip EnemyTurn;
    [SerializeField] AudioClip Introduction;
    [SerializeField] AudioClip Victory;
    [SerializeField] AudioClip Defeat;

    [SerializeField] StateMachine _stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupMusicPlay()
    {
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<SetupState>())
        {
            MusicPlayerScript.Instance.Play(Introduction);
            MusicPlayerScript.Instance._source.loop = true;
        }
    }

    public void PlayerTurnMusicPlay()
    {
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<PlayerTurnState>())
        {
            MusicPlayerScript.Instance.Play(PlayerTurn);
            MusicPlayerScript.Instance._source.loop = true;
        }
    }

    public void EnemyTurnMusicPlay()
    {
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<EnemyTurnState>())
        {
            MusicPlayerScript.Instance.Play(EnemyTurn);
            MusicPlayerScript.Instance._source.loop = true;
        }
    }

    public void VictoryMusicPlay()
    {
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<WinState>())
        {
            MusicPlayerScript.Instance.Play(Victory);
            MusicPlayerScript.Instance._source.loop = false;
        }
    }

    public void DefeatMusicPlay()
    {
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<LoseState>())
        {
            MusicPlayerScript.Instance.Play(Defeat);
            MusicPlayerScript.Instance._source.loop = false;
        }
    }
}
