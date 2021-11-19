using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorScript : MonoBehaviour
{
    [SerializeField] TurnBasedGameSM _stateMachine = null;
    [SerializeField] EnemyTurnState _enemyState = null;
    [SerializeField] GameObject[] _enemyPawns;
    [SerializeField] GameObject[] _Tiles;
    [SerializeField] List<GameObject> _ViableTiles;
     
    [SerializeField] GameObject _checkingForTile;
    public GameObject _enemyPawnToMove;
    public GameObject _tileToMoveTo;
    public GameManager _gameManager;
    [SerializeField] GameObject[] _TilesEnemyCanWalkTo;

    Vector3 pawnHeight = new Vector3(0f, 1f, 0f);

    [SerializeField] float _pauseDuration = 1f;

    [SerializeField] AudioSource _attackSound;
    [SerializeField] AudioSource _moveSound;
    [SerializeField] ParticleSystem _pawnDeathParticles;
    ParticleSystem ParticleCopy;

    int pickEnemyPawn = 0;
    int findTiles = 0;
    int findViableTiles = 0;
    int moveToTile = 0;

    int allPawnsMove = 0;

    int enemyTurnOver = 0;

    int findClosestPlayerPawn = 0;
    bool hasAttacked = false;

    int enemyPawnsLength = 0;

    int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        _Tiles = GameObject.FindGameObjectsWithTag("Tile");
        //_ViableTiles = new GameObject[_Tiles.Length];
        enemyPawnsLength = _enemyPawns.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<EnemyTurnState>())
        {
            _enemyPawns = GameObject.FindGameObjectsWithTag("EnemyPawn");
            StartEnemyTurn();

            //reset variables
            if (enemyTurnOver == 0)
            {
                pickEnemyPawn = 0;
                findTiles = 0;
                findViableTiles = 0;
                moveToTile = 0;
                allPawnsMove = 0;
                findClosestPlayerPawn = 0;                
            }
            else if (_stateMachine.CurrentState == _stateMachine.GetComponent<LoseState>())
            {
                //do nothing
            }
            else if (enemyTurnOver == 2)
            {
                _enemyState.Exit();
            }
        }
        else
        {
            enemyTurnOver = 0;
            i = 0;
        }
    }
    
    //calls coroutine once
    public void StartEnemyTurn()
    {
        if(enemyTurnOver == 0)
        {
            StartCoroutine(EnemyTurnPause());
            enemyTurnOver++;
        }
    }

    IEnumerator EnemyTurnPause()
    {
        while (i < _enemyPawns.Length)
        {
            yield return new WaitForSeconds(2f);
            _enemyPawnToMove = _enemyPawns[i];
            CheckForPawnToAttack();
            findClosestPlayerPawn = 0;
            FindViableTiles();
            TeleportSelectedPawntoRandomViableTile();
            CheckForPawnToAttack();

            pickEnemyPawn = 0;
            findTiles = 0;
            findViableTiles = 0;
            moveToTile = 0;
            allPawnsMove = 0;
            findClosestPlayerPawn = 0;
            i++;
            if (_stateMachine.CurrentState == _stateMachine.GetComponent<LoseState>())
            {
                i = _enemyPawns.Length;
            }
        }
        enemyTurnOver++;
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

    private void SelectEachPawn()
    {
        if(pickEnemyPawn < _enemyPawns.Length)
        {
            _enemyPawnToMove = _enemyPawns[pickEnemyPawn];
            FindViableTiles();
        }
    }

    private void FindViableTiles()
    {
        _ViableTiles.Clear();
        //find viable tiles within chosen pawn
        //make an array of viable tiles
        while (findTiles < _Tiles.Length)
        {
            float distance = Vector3.Distance(_enemyPawnToMove.transform.position, _Tiles[findTiles].transform.position);

            if (_Tiles[findTiles].GetComponent<TileScript>()._PawnOccupyingTileSpace == false && distance < 6)
            {
                //_ViableTiles[findViableTiles] = _Tiles[findTiles];
                _ViableTiles.Add(_Tiles[findTiles]);
                findViableTiles++;
            }
            //else
            //{
            //    _ViableTiles[findViableTiles] = null;
            //    findViableTiles++;
            //}
            findTiles++;
        }

        if (findTiles == _Tiles.Length)
        {
            //TeleportSelectedPawntoRandomViableTile();
        }
    }

    private void TeleportSelectedPawntoRandomViableTile()
    {
        //teleport chosen pawn to random viable tile
        if (moveToTile != 1 && _ViableTiles.Count != 0)
        {
            //int randompick2 = Random.Range(0, _ViableTiles.Lenght);
            int randompick2 = Random.Range(0, _ViableTiles.Count);
            _tileToMoveTo = _ViableTiles[randompick2];
            if (_tileToMoveTo == null)
            {
                moveToTile = 0;
                TeleportSelectedPawntoRandomViableTile();
            }
            else
            {
                _enemyPawnToMove.transform.position = _tileToMoveTo.transform.position + pawnHeight;
                _moveSound.Play();
                moveToTile++;
            }
        }
        else
        {
            //CheckForPawnToAttack();
        }
    }

    private void CheckForPawnToAttack()
    {
        _enemyPawnToMove.GetComponent<PawnScript>().DetectObject();
        //look for enemies, then destroy (end movement);
        while (findClosestPlayerPawn < _gameManager._playerTeam.Length)
        {
            float distance = Vector3.Distance(_enemyPawnToMove.transform.position, _gameManager._playerTeam[findClosestPlayerPawn].transform.position);
            if (distance < 6)
            {
                //if closest player pawn is south
                if(_gameManager._playerTeam[findClosestPlayerPawn].transform.position.z < _enemyPawnToMove.transform.position.z)
                {
                    //attack
                    _attackSound.Play();
                    ParticleCopy = Instantiate(_pawnDeathParticles, _gameManager._playerTeam[findClosestPlayerPawn].transform.position, Quaternion.identity);
                    Destroy(_gameManager._playerTeam[findClosestPlayerPawn]);
                    hasAttacked = true;
                }
                //if closest player pawn is west
                else if (_gameManager._playerTeam[findClosestPlayerPawn].transform.position.x < _enemyPawnToMove.transform.position.x)
                {
                    //attack
                    _attackSound.Play();
                    ParticleCopy = Instantiate(_pawnDeathParticles, _gameManager._playerTeam[findClosestPlayerPawn].transform.position, Quaternion.identity);
                    Destroy(_gameManager._playerTeam[findClosestPlayerPawn]);
                    hasAttacked = true;
                }
                //if closest player pawn is east
                else if (_gameManager._playerTeam[findClosestPlayerPawn].transform.position.x > _enemyPawnToMove.transform.position.x)
                {
                    //attack
                    _attackSound.Play();
                    ParticleCopy = Instantiate(_pawnDeathParticles, _gameManager._playerTeam[findClosestPlayerPawn].transform.position, Quaternion.identity);
                    Destroy(_gameManager._playerTeam[findClosestPlayerPawn]);
                    hasAttacked = true;
                }
                //if closest player pawn is north
                else if (_gameManager._playerTeam[findClosestPlayerPawn].transform.position.z > _enemyPawnToMove.transform.position.z)
                {
                    //attack
                    _attackSound.Play();
                    ParticleCopy = Instantiate(_pawnDeathParticles, _gameManager._playerTeam[findClosestPlayerPawn].transform.position, Quaternion.identity);
                    Destroy(_gameManager._playerTeam[findClosestPlayerPawn]);
                    hasAttacked = true;
                }                
            }
            if (hasAttacked == true)
            {
                findClosestPlayerPawn = _gameManager._playerTeam.Length;
            }
            findClosestPlayerPawn++;
        }

        //send back to Pick next Pawn
        //pickEnemyPawn++;
        WaitBeforePickingAgain();
        //SelectEachPawn();
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

    public void WaitBeforePickingAgain()
    {
        if (_pauseDuration > 0)
        {
            _pauseDuration -= Time.deltaTime;
        }
        else
        {
            _pauseDuration = 1f;
            SelectEachPawn();
        }
    }
}
