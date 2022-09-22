using System;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Transform playerMesh;
    private Vector3 direction;
    private Vector3 lookAtDirection;

    public event Action<Vector3> moveDirectionIsChanged;

    private void Start()
    {
        playerMesh = transform.Find("parent");
    }

    private void Update()
    {
        direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        moveDirectionIsChanged?.Invoke(direction);
        if (Mathf.Abs(variableJoystick.Vertical) > .1f || Mathf.Abs(variableJoystick.Horizontal) > .1f)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            lookAtDirection = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.z);
            transform.LookAt(lookAtDirection);
        }
    }
}