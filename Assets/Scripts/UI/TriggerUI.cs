using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUI : MonoBehaviour
{
    public string elementName = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.canvasT.Find(elementName).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform element=UIManager.instance.canvasT.Find(elementName);
            if (element!=null)
                element.gameObject.SetActive(false);
        }
    }
}
