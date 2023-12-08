using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    Rigidbody rb;
    bool onGround;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += moveSpeed * Time.deltaTime * Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += moveSpeed * Time.deltaTime * Vector3.back;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += moveSpeed * Time.deltaTime * Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += moveSpeed * Time.deltaTime * Vector3.right;
        }
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            onGround = false;
            rb.AddForce(jumpForce * Vector3.up);
        }
    }
}
