using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCast : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] public GameObject _objectLastDetectedbyRay;
    [SerializeField] GameManager _gameManager;

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            //raycastHit.transform.SendMessage("HitByRay");

            if (raycastHit.collider.gameObject.TryGetComponent(out TileScript tile))
            {
                tile.HitByRay();
            }
            if (raycastHit.collider.gameObject.TryGetComponent(out PawnScript pawn))
            {
                pawn.HitByRay();
            }
        }
    }
}
