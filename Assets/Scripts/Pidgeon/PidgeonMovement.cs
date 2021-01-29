using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PidgeonMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float hSpeed;
    [SerializeField] private float vSpeed;

    public Vector2 moveInput;
    private Vector3 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetMoveInputs();
        Movement();
    }

    private void GetMoveInputs()
    {
        moveInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
        moveInput.Normalize();
    }

    private void Movement()
    {
        movement.x = moveInput.x * hSpeed;
        movement.y = moveInput.y * vSpeed;

        movement *= Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }
}
