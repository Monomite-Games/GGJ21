using System.Collections;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class PigeonMovement : MonoBehaviour
    {
        private Rigidbody2D rb;
        public Transform model;

        [Space]

        [SerializeField] private float hSpeed;
        [SerializeField] private float vSpeed;
        [SerializeField] private float flutterSpeed;
        [SerializeField] private float fallSpeed;
        [SerializeField] private float boostSpeed;

        [Space]

        public bool isFluttering = false;
        public bool isFreeFalling = false;
        public bool isRecovering = false;

        [Space]

        public Vector2 moveInput;
        public Vector3 movement;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            GetMoveInputs();

            if (Input.GetKeyDown(KeyCode.Space) && !isFluttering)
            {
                if (!isFreeFalling || !isRecovering)
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
            if (!isRecovering)
            {
                movement.x = moveInput.x * hSpeed;
            }

            if (!isFreeFalling && !isFluttering)
            {
                movement.y = moveInput.y * vSpeed;
            }

            rb.velocity = new Vector3(movement.x, movement.y, 0f);
        }

        private IEnumerator Flutter()
        {
            isFluttering = true;

            movement.y = flutterSpeed;
            yield return new WaitForSeconds(0.7f);
            movement.y = 0.5f;

            isFluttering = false;
        }

        private void FreeFall()
        {
            if (!isFluttering && Input.GetKeyDown(KeyCode.LeftShift))
            {
                isFreeFalling = true;
                movement.y = -fallSpeed;
            }

            if (!isFluttering && Input.GetKeyUp(KeyCode.LeftShift))
            {
                isFreeFalling = false;
                movement.y = 0.5f;

                StartCoroutine(Boost());
            }
        }

        private IEnumerator Boost()
        {
            isRecovering = true;

            if (moveInput.x == 0f)
            {
                movement.x = model.right.x * boostSpeed;
            }
            else
            {
                movement.x = moveInput.x * boostSpeed;
            }
            movement.y = 3f;

            yield return new WaitForSeconds(1f);
            isRecovering = false;
        }
    }
}