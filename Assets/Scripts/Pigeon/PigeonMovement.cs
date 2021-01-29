using System.Collections;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class PigeonMovement : MonoBehaviour
    {
        private Rigidbody rb;

        [SerializeField] private float hSpeed;
        [SerializeField] private float vSpeed;
        [SerializeField] private float flutterSpeed;

        private bool isFluttering = false;
        private bool isFreeFalling = false;

        public Vector2 moveInput;
        private Vector3 movement;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            GetMoveInputs();

            if (Input.GetKeyDown(KeyCode.Space) && !isFluttering)
            {
                if (!isFreeFalling)
                {
                    StartCoroutine(Flutter());
                }
            }

            FreeFall();
        }

        private void FixedUpdate()
        {
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

            if (!isFreeFalling)
            {
                movement.y = moveInput.y * vSpeed;
            }
            else
            {
                movement.y = 0f;
            }

            movement *= Time.deltaTime;

            rb.MovePosition(transform.position + movement);
        }

        private IEnumerator Flutter()
        {
            isFluttering = true;

            rb.velocity = new Vector3(rb.velocity.x, flutterSpeed, rb.velocity.z);
            yield return new WaitForSeconds(0.5f);
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            isFluttering = false;
        }

        private void FreeFall()
        {
            if (!isFluttering && Input.GetKeyDown(KeyCode.LeftShift))
            {
                isFreeFalling = true;
                rb.useGravity = true;
            }

            if (!isFluttering && Input.GetKeyUp(KeyCode.LeftShift))
            {
                isFreeFalling = false;
                rb.useGravity = false;
            }
        }
    }
}