using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] TurnBasedGameSM _stateMachine = null;
    [SerializeField] GameObject[] _pawnSelected;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<EnemyTurnState>())
        {
            //picks a pawn at random from an array of pawns
            //wait 5 seconds
            //calculate tile distance from pawn
            //if tiles are within distance of pawn, move into array
            //select one tile from array and move pawn to the position of tile
            //wait 5 seconds
            //calculate pawn distance from player pawns
            //if enemy pawn is within attacking distance of player pawns, attack
            //wait 5 seconds
            //end turn
        }
    }



}
