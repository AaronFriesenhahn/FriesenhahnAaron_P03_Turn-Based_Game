using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] TurnBasedGameSM _stateMachine = null;
    [SerializeField] EnemyTurnState _enemyState = null;
    [SerializeField] GameObject[] _enemyPawns;
    [SerializeField] GameObject[] _Tiles;
    [SerializeField] List<GameObject> _activeTiles;
    public GameObject _enemyPawnToMove;
    public GameObject _tileToMoveTo;
    public GameManager _gameManager;
    [SerializeField] GameObject[] _TilesEnemyCanWalkTo;

    Vector3 pawnHeight = new Vector3(0f, 1f, 0f);

    int x = 0;
    int y = 0;
    int pawnMovement = 0;


    //tile stuff
    int z = 0;
    int a = 0;
    int b = 0;
    int c = 0;

    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        y = 0;
        z = 0;
        a = 0;
        b = 0;
        c = 0;
        pawnMovement = 0;

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
                _gameManager._pawnSelected = _enemyPawnToMove;
                Debug.Log(_enemyPawnToMove);
                y++;
            }
            //send to choose movement
            StartCoroutine(PauseAfterChoosingPawn());
        }
        else
        {

        }
    }

    //pawn AI
    //get movement for pawn, then send to choose attack
    public void CalculateDistanceFromPawn(GameObject _pawn)
    {
        _enemyPawnToMove.GetComponent<PawnScript>().Moving = true;
        _Tiles = GameObject.FindGameObjectsWithTag("Tile");

        // this works
        //pick an enemy pawn
        if (z != 1)
        {
            int randompick = Random.Range(0, _enemyPawns.Length);
            _enemyPawnToMove = _enemyPawns[randompick];
            _gameManager._pawnSelected = _enemyPawnToMove;
            Debug.Log(_enemyPawnToMove);
            z++;
        }

        //THIS SOMETIMES WORKS
        //find tiles
        if (a != _Tiles.Length && _enemyPawnToMove !=null)
        {
            //Debug.Log("List of Tiles: " + _Tiles[a]);
            //if tile is movable, add to active tiles
            if (_Tiles[a].GetComponent<TileScript>()._movementCanvas.gameObject.activeSelf == true)
            {
                _activeTiles.Add(_Tiles[a]);
                //Debug.Log(_activeTiles[a]);
            }
            a++;
        }

        //THIS SOMETIMES WORKS
        //find size of tiles and display tiles in activeTiles
        int sizeOfActiveTiles = _activeTiles.Count;
        if (b != sizeOfActiveTiles)
        {
            Debug.Log(_activeTiles[b]);
            b++;
        }
        Debug.Log(_activeTiles.Count);


        if (pawnMovement != _enemyPawnToMove.GetComponent<PawnScript>()._TilesToMove)
        {
            //THIS SOMETIMES WORKS
            //pick a random tile to move pawn to
            if (c != 1 && _activeTiles.Count !=0)
            {
                int randompick2 = Random.Range(0, _activeTiles.Count);
                Debug.Log(randompick2);
                _tileToMoveTo = _activeTiles[randompick2];
                Debug.Log("tile to move to: " + _tileToMoveTo);
                c++;
            }

            //move pawn to chosen tile
            _enemyPawnToMove.transform.position = _tileToMoveTo.transform.position + pawnHeight;
            pawnMovement++;

            //look for enemies, then destroy (end movement);
            if (_enemyPawnToMove.GetComponent<PawnScript>().ObjectToSouth.tag == "PlayerPawn" && _enemyPawnToMove.GetComponent<PawnScript>().ObjectToSouth != null)
            {
                Destroy(_enemyPawnToMove.GetComponent<PawnScript>().ObjectToSouth.gameObject);
                pawnMovement++;
            }
            else if (_enemyPawnToMove.GetComponent<PawnScript>().ObjectToNorth.tag == "PlayerPawn" && _enemyPawnToMove.GetComponent<PawnScript>().ObjectToNorth != null)
            {
                Destroy(_enemyPawnToMove.GetComponent<PawnScript>().ObjectToNorth.gameObject);
                pawnMovement++;
            }
            else if (_enemyPawnToMove.GetComponent<PawnScript>().ObjectToWest.tag == "PlayerPawn" && _enemyPawnToMove.GetComponent<PawnScript>().ObjectToWest != null)
            {
                Destroy(_enemyPawnToMove.GetComponent<PawnScript>().ObjectToWest.gameObject);
                pawnMovement++;
            }
            else if (_enemyPawnToMove.GetComponent<PawnScript>().ObjectToEast.tag == "PlayerPawn" && _enemyPawnToMove.GetComponent<PawnScript>().ObjectToEast != null)
            {
                Destroy(_enemyPawnToMove.GetComponent<PawnScript>().ObjectToEast.gameObject);
                pawnMovement++;
            }
            else
            {

            }
        }
        else
        {
            StartCoroutine(PauseAfterChoosingMovement());
        }
        
        ////find an active moveable tile in _Tiles
        //if (_Tiles.Length > a)
        //{
        //    if (_Tiles[a].GetComponent<TileScript>()._movementCanvas.gameObject.activeSelf == true)
        //    {
        //        _activeTiles[a] = _Tiles[a];
        //    }
        //    a++;
        //}

        //Debug.Log("Length of _activeTiles: " + _activeTiles.Length);
        //int randompick2 = Random.Range(0, _activeTiles.Length);
        //_tileToMoveTo = _activeTiles[randompick2];

        //_enemyPawnToMove.transform.position = _tileToMoveTo.transform.position;

        //calculate tile distance from pawn
        //if tiles are within distance of pawn, move into array
        //select one tile from array and move pawn to the position of tile

    }
    //get attack decision, then send to end turn
    public void CalculateAttack(GameObject _pawn)
    {
        _enemyPawnToMove.GetComponent<PawnScript>().Moving = false;

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
        _gameManager._pawnSelected = null;
        Debug.Log("Pausing, hopefully.");
        yield return new WaitForSeconds(2f);
        _enemyState.Exit();

    }



}
