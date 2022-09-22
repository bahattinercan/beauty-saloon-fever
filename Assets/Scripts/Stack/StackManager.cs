using System;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [SerializeField] private float distanceBetweenObjects;
    private Transform prevObject;
    private Transform parent;
    private Vector3 firstStackVector;
    public EStack stackType=EStack.scissors;
    public int stackNumber= 1;
    public int maxStackNumber=15;
    public event Action<int> stackNumberChanged;

    private void Start()
    {
        parent = transform.Find("parent");
        prevObject = parent.GetChild(0);
        firstStackVector = prevObject.localPosition;
        distanceBetweenObjects = prevObject.localScale.y;
        Destroy(GetStack());
    }

    public void Pickup(GameObject pickedObject, bool needTag = false, string tag = null, bool downOrUp = true)
    {
        if (stackNumber >= maxStackNumber)
            return;
        stackNumber++;
        stackNumberChanged?.Invoke(stackNumber);
        if (needTag)
        {
            pickedObject.tag = tag;
        }
        pickedObject.transform.SetParent(parent);
        Vector3 desPos = Vector3.zero; 
        if (prevObject != null)
        { 
            desPos= prevObject.localPosition;
            desPos.y += downOrUp ? distanceBetweenObjects : -distanceBetweenObjects;
        }
        else
        {
            desPos = firstStackVector;
            desPos.y += downOrUp ? distanceBetweenObjects : -distanceBetweenObjects;
        }        
        
        pickedObject.transform.localPosition = desPos;
        prevObject = pickedObject.transform;
    }

    public GameObject GetStack()
    {
        if (stackNumber > 1)
        {
            stackNumber--;
            stackNumberChanged?.Invoke(stackNumber);
            prevObject = parent.GetChild(parent.childCount - 2);            
            return parent.GetChild(parent.childCount - 1).gameObject;
        }
        else if (stackNumber == 1)
        {
            stackNumber--;
            stackNumberChanged?.Invoke(stackNumber);
            prevObject = null;
            return parent.GetChild(parent.childCount - 1).gameObject;
        }      
        return null;
    }
    public bool HasStack(int value=1)
    {
        if (stackNumber >= value)
            return true;
        return false;
    }

    public bool NeedStack()
    {
        if (stackNumber < maxStackNumber)
            return true;
        return false;
    }

    public bool NeedWork()
    {
        if (stackNumber == 0)
            return true;
        else if (stackNumber < (maxStackNumber *.75f))
            return true;
        return false;
    }
}