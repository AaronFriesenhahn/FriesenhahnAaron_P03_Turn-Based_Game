using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something has entered the trigger box.");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Something is staying in the trigger box.");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Something exited the trigger box.");
    }
}
