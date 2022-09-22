using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTrigger : MonoBehaviour
{
    StackManager stackManager;

    private void Start()
    {
        stackManager= GetComponent<StackManager>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Pickup"))
        {
            Debug.Log("asdþlfhkjasd");
            stackManager.Pickup(collision.gameObject, true, "Untagged", true);
        }
    }
}
