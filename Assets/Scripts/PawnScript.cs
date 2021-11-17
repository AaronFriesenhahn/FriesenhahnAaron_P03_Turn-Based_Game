using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawnScript : MonoBehaviour
{
    [SerializeField] TurnBasedGameSM _stateMachine = null;
    [SerializeField] Canvas _PlayerOptions = null;
    [SerializeField] PauseFunction _pause;
    [SerializeField] Material _normalMaterial;
    [SerializeField] public int _movement = 10;
    [SerializeField] public int _attackDistance = 5;
    [SerializeField] public int _TilesToMove = 2;

    [SerializeField] GameManager _gameManager;
    [SerializeField] TileScript _tiles;
    [SerializeField] Button _moveButton;
    [SerializeField] Button _attackButton;
    [SerializeField] Button _doneButton;

    public bool Selected = false;
    public bool Moving = false;
    public bool Attacking = false;
    public bool Hovering = true;
    //determies if a pawn can be selected again.
    public bool HasMoved = false;
    public bool HasAttacked = false;

    public bool CanMoveAfterAttacking = false;

    int _resetAttack;
    int _resetTilesToMove;

    //enemy Information
    public GameObject ObjectToNorth = null;
    public GameObject ObjectToEast = null;
    public GameObject ObjectToSouth = null;
    public GameObject ObjectToWest = null;

    // Start is called before the first frame update
    void Start()
    {
        _resetTilesToMove = _TilesToMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<PlayerTurnState>())
        {
            if (_pause.Paused == false)
            {
                if (Selected == true)
                {
                    //DetectObject();

                    if (_PlayerOptions != null)
                    {
                        _PlayerOptions.gameObject.SetActive(true);
                        if (HasAttacked == true)
                        {
                            if (CanMoveAfterAttacking == false)
                            {
                                _TilesToMove = 0;
                            }

                            _attackButton.gameObject.SetActive(false);
                            Debug.Log("has attacked");
                            if (_TilesToMove > 0)
                            {
                                _moveButton.gameObject.SetActive(true);
                            }
                            else
                            {
                                _moveButton.gameObject.SetActive(false);
                            }
                        }
                        else
                        {
                            if (Attacking == true)
                            {
                                _moveButton.gameObject.SetActive(false);
                                Debug.Log("Move button gone. attacking");
                            }
                            else if (Attacking == false)
                            {
                                _moveButton.gameObject.SetActive(true);
                            }
                        }
                        if (HasMoved == true)
                        {
                            _moveButton.gameObject.SetActive(false);
                            Debug.Log("Move button gone! Hasmoved");
                        }
                        else
                        {
                            if (Moving == true)
                            {
                                _attackButton.gameObject.SetActive(false);
                            }
                            else if (Moving == false && HasAttacked == false)
                            {
                                _attackButton.gameObject.SetActive(true);
                            }
                        }
                    }
                    //change material of pawn to blue while selected
                    //if player has moved, then presses Attack or presses End Turn, do not allow to be selected again until it is players turn again
                }
                else if (Selected == false)
                {
                    if (_PlayerOptions != null)
                    {
                        _PlayerOptions.gameObject.SetActive(false);
                    }
                }

                ////set pawn to mouse position for visual when moving
                //if (Moving == true)
                //{
                //    //gameObject.transform.position = //mouse position3d
                //    _attackButton.gameObject.SetActive(false);
                //    _moveButton.gameObject.SetActive(true);
                //}
                //else if (Attacking == true)
                //{
                //    _attackButton.gameObject.SetActive(true);
                //    _moveButton.gameObject.SetActive(false);
                //}
                //else if (Moving == false && Attacking == false)
                //{
                //    _attackButton.gameObject.SetActive(true);
                //    _moveButton.gameObject.SetActive(true);
                //}
            }
            else
            {
                if (_PlayerOptions != null)
                {
                    _PlayerOptions.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (_PlayerOptions != null)
            {
                _PlayerOptions.gameObject.SetActive(false);
                _moveButton.GetComponentInChildren<Text>().text = "Move";
                _attackButton.GetComponentInChildren<Text>().text = "Attack";
            }
            Moving = false;
            Attacking = false;
            Selected = false;
            HasAttacked = false;
            HasMoved = false;
            _TilesToMove = _resetTilesToMove;

        }

        //if hovering over something that is not the pawn, un select it
        //if (Hovering == false && Selected == true)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Selected = false;
        //        Moving = false;
        //        Attacking = false;
        //        _gameManager._pawnSelected = null;
        //    }
        //}
        
    }

    //private void OnMouseOver()
    //{
    //    //if on Player Turn and not paused while mouse is over pawn, change to a highlighted color
    //    if (_stateMachine.CurrentState == _stateMachine.GetComponent<PlayerTurnState>())
    //    {
    //        if(_pause.Paused == false && gameObject.tag=="PlayerPawn")
    //        {
    //            var PawnRenderer = gameObject.GetComponent<Renderer>();
    //            PawnRenderer.material.color = Color.yellow;

    //            //if mouse is clicked while on pawn, change bool Selected to true
    //            if (Input.GetMouseButtonDown(0))
    //            {
    //                Selected = true;
    //                _gameManager._pawnSelected = gameObject;
    //            }
    //            //if mouse is right clicked while on pawn, change bool Selected to false
    //            else if (Input.GetMouseButtonDown(1))
    //            {
    //                Selected = false;
    //                Moving = false;
    //                Attacking = false;
    //                _gameManager._pawnSelected = null;
    //            }
    //        }
    //    }
    //    Hovering = true;
    //}

    private void OnMouseExit()
    {
        var PawnRenderer = gameObject.GetComponent<Renderer>();
        PawnRenderer.material = _normalMaterial;
        Hovering = false;
    }

    public void SetMovingTrue()
    {
        StartCoroutine(WaitForClick());
        if (Moving == false)
        {
            Moving = true;
            _moveButton.GetComponentInChildren<Text>().text = "Return";
            _attackButton.GetComponentInChildren<Text>().text = "Attack";
            Attacking = false;
        }
        else
        {
            Moving = false;
            //return pawn to original position if hasmoved = false
            _moveButton.GetComponentInChildren<Text>().text = "Move";

        }
    }

    public void SetMovingFalse()
    {
        StartCoroutine(WaitForClick()); ;
        Moving = false;
    }

    public void SetAttackingTrue()
    {
        StartCoroutine(WaitForClick());
        if (Attacking == false)
        {
            Attacking = true;
            _attackButton.GetComponentInChildren<Text>().text = "Return";
            _moveButton.GetComponentInChildren<Text>().text = "Move";
            Moving = false;
        }
        else
        {
            Attacking = false;
            _attackButton.GetComponentInChildren<Text>().text = "Attack";
        }
    }

    public void SetAttackingFalse()
    {
        StartCoroutine(WaitForClick());
        Attacking = false;
    }

    IEnumerator WaitForClick()
    {
        yield return new WaitForSeconds(1f);
    }

    public void FinishedMoveOrAttack()
    {
        if (Moving == true)
        {
            //set new position as original position
            //set moving to false
            //do not allow to move again/set hasmoved to true
        }
        if (Attacking == true)
        {
            //do not allow to attack again
        }
    }

    public void Attacked()
    {
        //do vfx stuff
        StartCoroutine(PauseBeforeDestroyingObject());
    }

    IEnumerator PauseBeforeDestroyingObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void DetectObject()
    {
        //cast debug ray or whatever straight up to detect an object
        Debug.DrawRay(transform.position, Vector3.forward, Color.green);
        Debug.DrawRay(transform.position, Vector3.right, Color.red);
        Debug.DrawRay(transform.position, Vector3.back, Color.blue);
        Debug.DrawRay(transform.position, Vector3.left, Color.yellow);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit raycastHit))
        {
            if (raycastHit.collider.gameObject != this.gameObject)
            {
                if (raycastHit.collider.gameObject.tag == "PlayerPawn")
                {
                    Debug.Log(raycastHit.collider + " was hit by raycast north of player.");
                    ObjectToNorth = raycastHit.collider.gameObject;
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out raycastHit))
        {
            if (raycastHit.collider.gameObject != this.gameObject)
            {
                if (raycastHit.collider.gameObject.tag == "PlayerPawn")
                {
                    Debug.Log(raycastHit.collider + " was hit by raycast east of player.");
                    ObjectToEast = raycastHit.collider.gameObject;
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out raycastHit))
        {
            if (raycastHit.collider.gameObject != this.gameObject)
            {
                if (raycastHit.collider.gameObject.tag == "PlayerPawn")
                {
                    Debug.Log(raycastHit.collider + " was hit by raycast south of player.");
                    ObjectToSouth = raycastHit.collider.gameObject;
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out raycastHit))
        {
            if (raycastHit.collider.gameObject != this.gameObject)
            {
                if (raycastHit.collider.gameObject.tag == "PlayerPawn")
                {
                    Debug.Log(raycastHit.collider + " was hit by raycast west of player.");
                    ObjectToWest = raycastHit.collider.gameObject;
                }
            }
        }
        else
        {
            ObjectToNorth = null;
            ObjectToEast = null;
            ObjectToSouth = null;
            ObjectToWest = null;
        }
    }

    public void HitByRay()
    {
        Debug.Log("I was hit by a ray");
        //if on Player Turn and not paused while mouse is over pawn, change to a highlighted color
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<PlayerTurnState>())
        {

        }

        if (_pause.Paused == false && gameObject.tag == "PlayerPawn")
        {
            var PawnRenderer = gameObject.GetComponent<Renderer>();
            PawnRenderer.material.color = Color.yellow;

            //if mouse is clicked while on pawn, change bool Selected to true
            if (Input.GetMouseButtonDown(0))
            {
                Selected = true;
                _gameManager._pawnSelected = gameObject;
            }
            //if mouse is right clicked while on pawn, change bool Selected to false
            else if (Input.GetMouseButtonDown(1))
            {
                if (HasAttacked == true || HasMoved == true)
                {
                    Selected = false;
                    Moving = false;
                    Attacking = false;
                    _gameManager._pawnSelected = null;
                }
                else
                {
                    Selected = false;
                    Moving = false;
                    Attacking = false;
                    _gameManager._pawnSelected = null;
                }
            }
        }

        Hovering = true;
    }
}
