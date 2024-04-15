using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Vector3 _input;

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 input)
    {

        _input = input;

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _input * Time.deltaTime);
    }
    public void LookToMouseInput(Vector3 input)
    {
        Vector3 heightConfirmation = new Vector3(input.x,transform.position.y,input.z);
        transform.LookAt(heightConfirmation);

    }

}
