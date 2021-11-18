using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorScript : MonoBehaviour
{
    [SerializeField] TurnBasedGameSM _stateMachine = null;
    [SerializeField] EnemyTurnState _enemyState = null;
    [SerializeField] GameObject[] _enemyPawns;
    [SerializeField] GameObject[] _Tiles;
    [SerializeField] GameObject[] _ViableTiles;
    [SerializeField] GameObject _checkingForTile;
    public GameObject _enemyPawnToMove;
    public GameObject _tileToMoveTo;
    public GameManager _gameManager;
    [SerializeField] GameObject[] _TilesEnemyCanWalkTo;

    Vector3 pawnHeight = new Vector3(0f, 1f, 0f);

    int pickEnemyPawn = 0;
    int findTiles = 0;
    int findViableTiles = 0;
    int moveToTile = 0;

    int allPawnsMove = 0;

    int enemyTurnOver = 0;

    int findClosestPlayerPawn = 0;


    // Start is called before the first frame update
    void Start()
    {
        _Tiles = GameObject.FindGameObjectsWithTag("Tile");
        _ViableTiles = new GameObject[_Tiles.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<EnemyTurnState>())
        {
            _enemyPawns = GameObject.FindGameObjectsWithTag("EnemyPawn");

            //TODO Code to go through all of the pawns
            //while (allPawnsMove < _enemyPawns.Length)
            //{
            //   SelectRandomPawn();
            //   allPawnsMove++;
            //}

            SelectRandomPawn();

            //StartCoroutine(ExitTurn());
            if (enemyTurnOver == 0)
            {
                pickEnemyPawn = 0;
                findTiles = 0;
                findViableTiles = 0;
                moveToTile = 0;
                allPawnsMove = 0;
                enemyTurnOver++;
            }
            _enemyState.Exit();
        }
        else
        {
            enemyTurnOver = 0;
        }
    }
    

    private void SelectRandomPawn()
    {
        //select a random pawn from array
        if (pickEnemyPawn != 1)
        {
            int randompick = Random.Range(0, _enemyPawns.Length);
            _enemyPawnToMove = _enemyPawns[randompick];
            Debug.Log(_enemyPawnToMove);
            pickEnemyPawn++;
        }
        else
        {
            FindViableTiles();
        }
    }

    private void FindViableTiles()
    {
        //find viable tiles within chosen pawn
        //make an array of viable tiles
        while (findTiles < _Tiles.Length)
        {
            float distance = Vector3.Distance(_enemyPawnToMove.transform.position, _Tiles[findTiles].transform.position);

            if (_Tiles[findTiles].GetComponent<TileScript>()._PawnOccupyingTileSpace == false && distance < 6)
            {
                _ViableTiles[findViableTiles] = _Tiles[findTiles];
                findViableTiles++;
            }
            else
            {
                _ViableTiles[findViableTiles] = null;
                findViableTiles++;
            }
            findTiles++;
        }

        if (findTiles == _Tiles.Length)
        {
            TeleportSelectedPawntoRandomViableTile();
        }
    }

    private void TeleportSelectedPawntoRandomViableTile()
    {
        //teleport chosen pawn to random viable tile
        if (moveToTile != 1)
        {
            int randompick2 = Random.Range(0, _ViableTiles.Length);
            _tileToMoveTo = _ViableTiles[randompick2];
            if (_tileToMoveTo == null)
            {
                moveToTile = 0;
                TeleportSelectedPawntoRandomViableTile();
            }
            else
            {
                _enemyPawnToMove.transform.position = _tileToMoveTo.transform.position + pawnHeight;
                moveToTile++;
            }
        }
        else
        {
            CheckForPawnToAttack();
        }
    }

    private void CheckForPawnToAttack()
    {
        _enemyPawnToMove.GetComponent<PawnScript>().DetectObject();
        //look for enemies, then destroy (end movement);
        float distance = Vector3.Distance(_enemyPawnToMove.transform.position, _gameManager._playerTeam[findClosestPlayerPawn].transform.position);

        if (distance < 6)
        {
            if ()
            findViableTiles++;
        }
    }

    IEnumerator ExitTurn()
    {
        //_gameManager._pawnSelected = null;
        yield return new WaitForSeconds(5f);

        _enemyState.Exit();
    }

    IEnumerator PawnMove()
    {
        yield return new WaitForSeconds(1f);
        SelectRandomPawn();
        yield return new WaitForSeconds(1f);
        FindViableTiles();
        yield return new WaitForSeconds(1f);
        //TeleportSelectedPawntoRandomViableTile();
        yield return new WaitForSeconds(1f);
        //AttackNearbyEnemy();
        StartCoroutine(ExitTurn());
    }
}
