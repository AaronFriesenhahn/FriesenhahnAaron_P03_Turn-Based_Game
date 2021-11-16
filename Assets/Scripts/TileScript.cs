using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileScript : MonoBehaviour
{
    [SerializeField] TurnBasedGameSM _stateMachine = null;
    [SerializeField] public Canvas _movementCanvas;
    [SerializeField] Image _movementHighlight;
    [SerializeField] public Canvas _attackCanvas;
    [SerializeField] Image _attackHighlight;
    [SerializeField] public GameObject PawnSelected = null;
    [SerializeField] GameManager _gameManager;

    int distanceFromPawn = 5;

    float distanceX;
    float distanceY;
    float distanceZ;

    float Distance;

    Vector3 pawnHeight = new Vector3(0f, 1f, 0f);

    public GameObject _PawnOccupyingTileSpace;

    //test value
    //allows distance to be only calculated once,
    //should be reset to zero when movement is over for pawn
    public int x = 0;
    //
    //public bool _TileOccupiedByPawn = false;

    // Start is called before the first frame update
    void Start()
    {
        _movementCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PawnSelected = _gameManager._pawnSelected;
        DetectPawnOnTile();

        //players turn
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<PlayerTurnState>())
        {
            //if a pawn can move
            if(_gameManager._pawnSelected != null)
            {
                if (_gameManager._pawnSelected.GetComponent<PawnScript>()._TilesToMove > 0)
                {
                    //player has selected a pawn
                    if (_gameManager._pawnSelected != null)
                    {
                        if (_gameManager._pawnSelected.GetComponent<PawnScript>().Moving == true)
                        {
                            //if a pawn is on a tile already, don't show it is available
                            if (_PawnOccupyingTileSpace != null)
                            {
                                _movementCanvas.gameObject.SetActive(false);
                            }
                            else
                            {
                                Debug.Log("Checking move distance.");
                                CalculateDistanceBetweenObjects(PawnSelected);
                                //check distance of tile from pawn and show highlightCanvas
                                //if (Distance <= PawnSelected.GetComponent<PawnScript>()._movement)
                                if (Distance <= distanceFromPawn)
                                {
                                    _movementCanvas.gameObject.SetActive(true);
                                }
                                //else if (Distance > PawnSelected.GetComponent<PawnScript>()._movement)
                                else if (Distance > distanceFromPawn)
                                {
                                    _movementCanvas.gameObject.SetActive(false);
                                }
                            }
                        }
                        else if (_gameManager._pawnSelected.GetComponent<PawnScript>().Attacking == true)
                        {
                            Debug.Log("Checking attack distance.");
                            CalculateDistanceBetweenObjects(PawnSelected);
                            //check attack distance
                            if (Distance <= PawnSelected.GetComponent<PawnScript>()._attackDistance)
                            {
                                _attackCanvas.gameObject.SetActive(true);
                            }
                            else
                            {
                                _attackCanvas.gameObject.SetActive(false);
                                x = 0;
                            }
                        }
                        else
                        {
                            _movementCanvas.gameObject.SetActive(false);
                            _attackCanvas.gameObject.SetActive(false);
                            x = 0;
                        }
                    }
                }
                else if (_gameManager._pawnSelected.GetComponent<PawnScript>().Attacking == true && _gameManager._pawnSelected.GetComponent<PawnScript>().HasAttacked == false)
                {
                    Debug.Log("Checking attack distance.");
                    CalculateDistanceBetweenObjects(PawnSelected);
                    //check attack distance
                    if (Distance <= PawnSelected.GetComponent<PawnScript>()._attackDistance)
                    {
                        _attackCanvas.gameObject.SetActive(true);
                    }
                    else
                    {
                        _attackCanvas.gameObject.SetActive(false);
                        x = 0;
                    }
                }
                else
                {
                    _movementCanvas.gameObject.SetActive(false);
                    _attackCanvas.gameObject.SetActive(false);
                    x = 0;
                }
            }
        }
        //enemy's turn
        else if (_stateMachine.CurrentState == _stateMachine.GetComponent<EnemyTurnState>())
        {
            //if a pawn can move
            if (_gameManager._pawnSelected != null)
            {
                if (_gameManager._pawnSelected.GetComponent<PawnScript>()._TilesToMove > 0)
                {
                    //player has selected a pawn
                    if (_gameManager._pawnSelected != null)
                    {
                        if (_gameManager._pawnSelected.GetComponent<PawnScript>().Moving == true)
                        {
                            //if a pawn is on a tile already, don't show it is available
                            if (_PawnOccupyingTileSpace != null)
                            {
                                _movementCanvas.gameObject.SetActive(false);
                            }
                            else
                            {
                                Debug.Log("Checking move distance.");
                                CalculateDistanceBetweenObjects(PawnSelected);
                                //check distance of tile from pawn and show highlightCanvas
                                //if (Distance <= PawnSelected.GetComponent<PawnScript>()._movement)
                                if (Distance <= distanceFromPawn)
                                {
                                    _movementCanvas.gameObject.SetActive(true);
                                }
                                //else if (Distance > PawnSelected.GetComponent<PawnScript>()._movement)
                                else if (Distance > distanceFromPawn)
                                {
                                    _movementCanvas.gameObject.SetActive(false);
                                }
                            }
                        }
                        else if (_gameManager._pawnSelected.GetComponent<PawnScript>().Attacking == true)
                        {
                            Debug.Log("Checking attack distance.");
                            CalculateDistanceBetweenObjects(PawnSelected);
                            //check attack distance
                            if (Distance <= PawnSelected.GetComponent<PawnScript>()._attackDistance)
                            {
                                _attackCanvas.gameObject.SetActive(true);
                            }
                            else
                            {
                                _attackCanvas.gameObject.SetActive(false);
                                x = 0;
                            }
                        }
                        else
                        {
                            _movementCanvas.gameObject.SetActive(false);
                            _attackCanvas.gameObject.SetActive(false);
                            x = 0;
                        }
                    }
                }
                else if (_gameManager._pawnSelected.GetComponent<PawnScript>().Attacking == true && _gameManager._pawnSelected.GetComponent<PawnScript>().HasAttacked == false)
                {
                    Debug.Log("Checking attack distance.");
                    CalculateDistanceBetweenObjects(PawnSelected);
                    //check attack distance
                    if (Distance <= PawnSelected.GetComponent<PawnScript>()._attackDistance)
                    {
                        _attackCanvas.gameObject.SetActive(true);
                    }
                    else
                    {
                        _attackCanvas.gameObject.SetActive(false);
                        x = 0;
                    }
                }
                else
                {
                    _movementCanvas.gameObject.SetActive(false);
                    _attackCanvas.gameObject.SetActive(false);
                    x = 0;
                }
            }
        }
        else
        {
            _movementCanvas.gameObject.SetActive(false);
            _attackCanvas.gameObject.SetActive(false);
            x = 0;
        }

        if(_gameManager._pawnSelected == null)
        {
            _movementCanvas.gameObject.SetActive(false);
            _attackCanvas.gameObject.SetActive(false);
            x = 0;
        }
    }

    public void CalculateDistanceBetweenObjects(GameObject _pawn)
    {
        //if (x == 0)
        //{
        //    float distance = Vector3.Distance(_pawn.transform.position, gameObject.transform.position);
        //    //round distance's number down
        //    x += 1;
        //    Distance = Mathf.Round(distance);
        //    Debug.Log(gameObject + " distance to Pawn: " + Distance);
        //}

        if (_gameManager._pawnSelected.GetComponent<PawnScript>()._TilesToMove >0)
        {
            float distance = Vector3.Distance(_pawn.transform.position, gameObject.transform.position);
            //round distance's number down
            //x += 1;
            Distance = Mathf.Round(distance);
            Debug.Log(gameObject + " distance to Pawn: " + Distance);
        }
        else if (_gameManager._pawnSelected.GetComponent<PawnScript>().Attacking == true)
        {
            float distance = Vector3.Distance(_pawn.transform.position, gameObject.transform.position);
            //round distance's number down
            //x += 1;
            Distance = Mathf.Round(distance);
            Debug.Log(gameObject + " distance to Pawn: " + Distance);
        }
    }

    //gets value from collider
    public void DetectPawnOnTile()
    {
        //cast debug ray or whatever straight up to detect an object
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out RaycastHit raycastHit))
        {
            Debug.Log(raycastHit.collider + " was hit by raycast");
            _PawnOccupyingTileSpace = raycastHit.collider.gameObject;
        }
        else
        {
            _PawnOccupyingTileSpace = null;
        }
    }

    //public void OnMouseOver()
    //{
    //    //if a tile is within moving distance
    //    if (_highlightCanvas.gameObject.activeSelf == true)
    //    {
    //        if (_gameManager._pawnSelected != null)
    //        {
    //            if (_gameManager._pawnSelected.GetComponent<PawnScript>().Moving == true)
    //            {
    //                if (Input.GetMouseButtonDown(0))
    //                {
    //                    _gameManager._pawnSelected.transform.position = gameObject.transform.position + pawnHeight;
    //                }
    //            }
    //        }
    //    }
    //}

    //called from MouseRayCast script
    public void HitByRay()
    {
        Debug.Log("Move player here?");
        //if a tile is within moving distance
        if (_movementCanvas.gameObject.activeSelf == true)
        {
            if (_gameManager._pawnSelected != null)
            {
                if (_gameManager._pawnSelected.GetComponent<PawnScript>().Moving == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_gameManager._pawnSelected.GetComponent<PawnScript>()._TilesToMove > 0)
                        {
                            _gameManager._pawnSelected.transform.position = gameObject.transform.position + pawnHeight;
                            _gameManager._pawnSelected.GetComponent<PawnScript>()._TilesToMove -= 1;
                            CalculateDistanceBetweenObjects(PawnSelected);
                        }
                        else if (_gameManager._pawnSelected.GetComponent<PawnScript>()._TilesToMove == 0)
                        {
                            _gameManager._pawnSelected.GetComponent<PawnScript>().HasMoved = true;
                            _gameManager._pawnSelected.GetComponent<PawnScript>()._TilesToMove = 2;
                        }
                    }
                }

            }
        }
        //if an enemmy is within attacking distance
        if(_attackCanvas.gameObject.activeSelf == true)
        {
            if (_gameManager._pawnSelected.GetComponent<PawnScript>().Attacking == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_PawnOccupyingTileSpace != null)
                    {
                        if (_stateMachine.CurrentState == _stateMachine.GetComponent<PlayerTurnState>())
                        {
                            if (_PawnOccupyingTileSpace.tag == "EnemyPawn")
                            {
                                Destroy(_PawnOccupyingTileSpace);
                                _gameManager._pawnSelected.GetComponent<PawnScript>().HasAttacked = true;
                                _gameManager._pawnSelected.GetComponent<PawnScript>().Attacking = false;
                            }
                        }
                        else if (_stateMachine.CurrentState == _stateMachine.GetComponent<EnemyTurnState>())
                        {
                            if (_PawnOccupyingTileSpace.tag == "PlayerPawn")
                            {
                                Destroy(_PawnOccupyingTileSpace);
                                _gameManager._pawnSelected.GetComponent<PawnScript>().HasAttacked = true;
                                _gameManager._pawnSelected.GetComponent<PawnScript>().Attacking = false;
                            }
                        }
                    }
                }
            }
        }
    }
}
