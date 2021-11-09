using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCast : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] public GameObject _objectLastDetectedbyRay;

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            raycastHit.transform.SendMessage("HitByRay");
        }
    }
}
