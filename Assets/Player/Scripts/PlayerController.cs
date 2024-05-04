using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = .25f;
    //[SerializeField] float turnSpeed = .25f;

    Rigidbody rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveVertical();
        MoveHorizontal();
        //Turn();
    }

    public void MoveVertical()
    {
        float moveAmountThisFrame = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 moveOffset = transform.forward * moveAmountThisFrame;
        rb.MovePosition(rb.position + moveOffset);
    }

    public void MoveHorizontal()
    {
        float moveAmountThisFrame = Input.GetAxis("Horizontal") * moveSpeed;
        Vector3 moveOffset = transform.right * moveAmountThisFrame;
        rb.MovePosition(rb.position + moveOffset);
    }

    /*public void Move()
    {
        float moveAmountThisFrame = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 moveOffset = transform.forward * moveAmountThisFrame;
        rb.MovePosition(rb.position + moveOffset);
    }*/

    public void Turn()
    {
        /*float turnAmountThisFrame = Input.GetAxis("Horizontal") * turnSpeed;
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        rb.MoveRotation(rb.rotation * turnOffset);*/
    }
}
