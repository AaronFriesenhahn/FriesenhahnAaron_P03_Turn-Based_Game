using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileScript : MonoBehaviour
{
    [SerializeField] TurnBasedGameSM _stateMachine = null;
    [SerializeField] Canvas _highlightCanvas;
    [SerializeField] Image _highlight;
    [SerializeField] public GameObject PawnSelected = null;
    [SerializeField] GameManager _gameManager;

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
        _highlightCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PawnSelected = _gameManager._pawnSelected;
        DetectPawnOnTile();

        if (_stateMachine.CurrentState == _stateMachine.GetComponent<PlayerTurnState>())
        {
            //if a pawn is on a tile already, don't show it is available
            if (_PawnOccupyingTileSpace != null)
            {
                _highlightCanvas.gameObject.SetActive(false);
            }           
            //player has selected a pawn
            else if (_gameManager._pawnSelected != null)
            {
                if (_gameManager._pawnSelected.GetComponent<PawnScript>().Moving == true)
                {
                    Debug.Log("Checking move distance.");
                    CalculateDistanceBetweenObjects(PawnSelected);
                    //check distance of tile from pawn and show highlightCanvas
                    if (Distance <= PawnSelected.GetComponent<PawnScript>()._movement)
                    {
                        _highlightCanvas.gameObject.SetActive(true);
                        Color highlightblue = Color.blue;
                        highlightblue.a = 0.25f;
                        _highlight.GetComponent<Image>().color = highlightblue;
                    }
                    else if (Distance > PawnSelected.GetComponent<PawnScript>()._movement)
                    {
                        _highlightCanvas.gameObject.SetActive(false);
                    }
                }
                else if (_gameManager._pawnSelected.GetComponent<PawnScript>().Attacking == true)
                {
                    Debug.Log("Checking attack distance.");
                    CalculateDistanceBetweenObjects(PawnSelected);
                    //check attack distance
                    if (Distance <= PawnSelected.GetComponent<PawnScript>()._attackDistance)
                    {
                        _highlightCanvas.gameObject.SetActive(true);
                        Color highlightred = Color.red;
                        highlightred.a = 0.25f;
                        _highlight.GetComponent<Image>().color = highlightred;
                    }
                    else
                    {
                        _highlightCanvas.gameObject.SetActive(false);
                        x = 0;
                    }
                }
                else
                {
                    _highlightCanvas.gameObject.SetActive(false);
                    x = 0;
                }
            }
        }
        else
        {
            _highlightCanvas.gameObject.SetActive(false);
            x = 0;
        }

        if(_gameManager._pawnSelected == null)
        {
            _highlightCanvas.gameObject.SetActive(false);
            x = 0;
        }
    }

    public void CalculateDistanceBetweenObjects(GameObject _pawn)
    {
        if (x == 0)
        {
            float distance = Vector3.Distance(_pawn.transform.position, gameObject.transform.position);
            //round distance's number down
            x += 1;
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

    public void HitByRay()
    {
        //if a tile is within moving distance
        if (_highlightCanvas.gameObject.activeSelf == true)
        {
            if (_gameManager._pawnSelected != null)
            {
                if (_gameManager._pawnSelected.GetComponent<PawnScript>().Moving == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        _gameManager._pawnSelected.transform.position = gameObject.transform.position + pawnHeight;
                    }
                }
            }
        }
    }
}
