using System.Collections;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class PigeonMovement : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        private Rigidbody2D rb;

        [Space]
        [Header("Speeds")]
        [SerializeField] private float hSpeed;
        [SerializeField] private float vSpeed;
        [SerializeField] private float flutterSpeed;
        [SerializeField] private float fallSpeed;
        [SerializeField] private float boostSpeed;

        [Space]
        [Header("State")]
        public bool isFluttering = false;
        public bool isFreeFalling = false;
        public bool isRecovering = false;

        [Space]
        [Header("Timers")]
        public float fallingTimer = 0f;

        [Space]
        [Header("Inputs")]
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

            Rotate();

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
                fallingTimer = Time.time;

                isFreeFalling = true;
                movement.y = -fallSpeed;
            }

            if (!isFluttering && Input.GetKeyUp(KeyCode.LeftShift))
            {
                isFreeFalling = false;
                movement.y = 0.5f;

                fallingTimer = Time.time - fallingTimer;

                StartCoroutine(Boost(fallingTimer));

                fallingTimer = 0f;
            }
        }

        private IEnumerator Boost(float timer)
        {
            isRecovering = true;

            if (moveInput.x != 0f)
            {
                movement.x = moveInput.x;
                movement.y = 3f;
            }
            movement.x *= boostSpeed;

            yield return new WaitForSeconds(timer/2f);

            isRecovering = false;
        }

        private void Rotate()
        {
            Vector3 rot;

            if (movement.x > 0f)
            {
                rot = transform.rotation.eulerAngles;
                rot.y = 0f;
                transform.rotation = Quaternion.Euler(rot);
            }
            else if (movement.x < 0f)
            {
                rot = transform.rotation.eulerAngles;
                rot.y = 180f;
                transform.rotation = Quaternion.Euler(rot);
            }
        }
    }
}