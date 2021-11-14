using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject _mainCamera;
    public bool CameraIsInPlayableArea = false;
    public Vector3 _lastValidPosition;
    public Vector3 _lastValidPosition2;
    public Vector3 _initialPosition;

    public float countdown = 0.5f;
    public float OutsideCountdown = 2f;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera.transform.position = _initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _mainCamera.transform.position += new Vector3(10, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _mainCamera.transform.position += new Vector3(-10, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            _mainCamera.transform.position += new Vector3(0, 0, 10) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _mainCamera.transform.position += new Vector3(0, 0, -10) * Time.deltaTime;
        }

        ReturnCameraToPlayArea();
        CountdownForLastValidPosition2();
        CountdownForOutSidePlayableArea();

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("something Entered");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Something stayed.");
        CameraIsInPlayableArea = true;

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Something left.");
        CameraIsInPlayableArea = false;
        //_mainCamera.transform.position = _lastValidPosition;
    }

    public void ReturnCameraToPlayArea()
    {
        if(CameraIsInPlayableArea == true)
        {
            _lastValidPosition = _mainCamera.transform.position;
            //StartCoroutine(DelayPositions());
        }
        else if (CameraIsInPlayableArea == false)
        {
            if (_lastValidPosition2!= _lastValidPosition)
            {
                _mainCamera.transform.position = _lastValidPosition2;
            }
            else if (_lastValidPosition2 == _lastValidPosition)
            {
                _mainCamera.transform.position = _initialPosition;
            }
            else
            {
                //CountdownForOutSidePlayableArea();
            }
        }
    }

    public void CountdownForLastValidPosition2()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            _lastValidPosition2 = _mainCamera.transform.position;
            countdown = 0.25f;
        }
    }

    public void CountdownForOutSidePlayableArea()
    {
        if (OutsideCountdown > 0)
        {
            OutsideCountdown -= Time.deltaTime;
        }
        else
        {
            if(CameraIsInPlayableArea == false)
            {
                _mainCamera.transform.position = _initialPosition;
            }
            OutsideCountdown = 2f;
        }
    }

    IEnumerator DelayPositions()
    {
        _lastValidPosition = _mainCamera.transform.position;
        yield return new WaitForSecondsRealtime(2f);
        _lastValidPosition2 = _mainCamera.transform.position;
    }
}
