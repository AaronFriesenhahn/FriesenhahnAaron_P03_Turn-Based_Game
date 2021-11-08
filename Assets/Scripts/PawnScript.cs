using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnScript : MonoBehaviour
{
    [SerializeField] TurnBasedGameSM _stateMachine = null;
    [SerializeField] Canvas _PlayerOptions = null;
    [SerializeField] PauseFunction _pause;
    [SerializeField] Material _normalMaterial;
    [SerializeField] public int _movement = 10;
    [SerializeField] public int _attackDistance = 5;

    [SerializeField] GameManager _gameManager;
    [SerializeField] TileScript _tiles;

    public bool Selected = false;
    public bool Moving = false;
    public bool Attacking = false;
    //determies if a pawn can be selected again.
    public bool ActionsTaken = false;

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
                if (Selected == true)
                {
                    if (_PlayerOptions != null)
                    {
                        _PlayerOptions.gameObject.SetActive(true);
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
            }
            Moving = false;
            Attacking = false;
            Selected = false;
        }

        //set pawn to mouse position for visual when moving
        if (Moving == true)
        {
            //gameObject.transform.position = //mouse position3d
        }
    }

    private void OnMouseOver()
    {
        //if on Player Turn and not paused while mouse is over pawn, change to a highlighted color
        if (_stateMachine.CurrentState == _stateMachine.GetComponent<PlayerTurnState>())
        {
            if(_pause.Paused == false && gameObject.tag!="EnemyPawn")
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
                    Selected = false;
                    Moving = false;
                    Attacking = false;
                    _gameManager._pawnSelected = null;
                }
            }
        }
    }

    private void OnMouseExit()
    {
        var PawnRenderer = gameObject.GetComponent<Renderer>();
        PawnRenderer.material = _normalMaterial;
    }

    public void SetMovingTrue()
    {
        WaitForClick();
        Moving = true;
    }

    public void SetMovingFalse()
    {
        WaitForClick();
        Moving = false;
    }

    public void SetAttackingTrue()
    {
        WaitForClick();
        Attacking = true;
    }

    public void SetAttackingFalse()
    {
        WaitForClick();
        Attacking = false;
    }

    IEnumerator WaitForClick()
    {
        yield return new WaitForSeconds(1f);
    }
}
