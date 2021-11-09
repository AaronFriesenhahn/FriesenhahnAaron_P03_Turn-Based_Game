using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] TurnBasedGameSM _stateMachine = null;
    [SerializeField] EnemyTurnState _enemyState = null;
    [SerializeField] GameObject[] _enemyPawns;
    GameObject _enemyPawnToMove;
    [SerializeField] GameObject[] _TilesEnemyCanWalkTo;

    int x = 0;
    int y = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<EnemyTurnState>())
        {
            //Finds enemy pawns and places them in array
            _enemyPawns = GameObject.FindGameObjectsWithTag("EnemyPawn");

            //while loop code if i want to have enemy move all of its pawns (eventually?)
            //while (x <= _enemyPawns.Length)
            //{
            //    //picks a pawn at random from an array of pawns
            //    int randomPick = Random.Range(0, _enemyPawns.Length);
            //    _enemyPawnToMove = _enemyPawns[randomPick];
            //    Debug.Log(_enemyPawnToMove);
            //    x++;
            //}

            //picks a pawn at random from an array of pawns
            if (y != 1)
            {
                int randompick = Random.Range(0, _enemyPawns.Length);
                _enemyPawnToMove = _enemyPawns[randompick];
                Debug.Log(_enemyPawnToMove);
                y++;
            }
            //send to choose movement
            StartCoroutine(PauseAfterChoosingPawn());
        }
    }

    //get movement for pawn, then send to choose attack
    public void CalculateDistanceFromPawn(GameObject _pawn)
    {
        //calculate tile distance from pawn
        //if tiles are within distance of pawn, move into array
        //select one tile from array and move pawn to the position of tile
        StartCoroutine(PauseAfterChoosingMovement());
    }
    //get attack decision, then send to end turn
    public void CalculateAttack(GameObject _pawn)
    {
        //calculate pawn distance from player pawns
        //if enemy pawn is within attacking distance of player pawns, attack
        //end turn
        StartCoroutine(PauseForEffect());
    }
    
    //choose movement
    IEnumerator PauseAfterChoosingPawn()
    {
        Debug.Log("Pausing...will now calculate move distance of pawn.");
        yield return new WaitForSeconds(2f);
        CalculateDistanceFromPawn(_enemyPawnToMove);
    }

    //choose attack
    IEnumerator PauseAfterChoosingMovement()
    {
        Debug.Log("Pausing...will now calculate attack.");
        yield return new WaitForSeconds(2f);
        CalculateAttack(_enemyPawnToMove);
    }

    //end turn
    IEnumerator PauseForEffect()
    {
        Debug.Log("Pausing, hopefully.");
        yield return new WaitForSeconds(2f);
        _enemyState.Exit();

    }



}
