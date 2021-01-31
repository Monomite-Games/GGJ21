using System.Collections;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class PigeonMovement : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        private Rigidbody2D rb;
        private Animator animator;

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
        public bool canMove = true;

        [Space]
        [Header("Timers")]
        public float fallingTimer = 0f;

        [Space]
        [Header("Inputs")]
        public Vector2 moveInput;
        public Vector3 movement;

        [Space]
        [Header("Particles")]
        public ParticleSystem FlutterParticle;
        public ParticleSystem MovementParticle;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = transform.GetChild(0).GetComponent<Animator>();

            GameEvents.LifeLost += (send, args) => canMove = false;
        }

        private void Update()
        {
            if (canMove)
            {
                GetMoveInputs();
                ChangeAnimLayer();

                if (Input.GetKeyDown(KeyCode.Space) && !isFluttering)
                {
                    if (!isFreeFalling || !isRecovering)
                    {
                        StartCoroutine(Flutter());
                    }
                }

                Rotate();

                if (!isFluttering)
                {
                    FreeFall();
                }

                PlayMoveParticles();
            }
        }

        private void FixedUpdate()
        {
            if (canMove)
            {
                Movement();
            }
        }

        private void GetMoveInputs()
        {
            moveInput = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"));
            moveInput.Normalize();

            animator.SetBool("isMoving", moveInput != Vector2.zero);
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
            animator.SetTrigger("isFluttering");

            isFluttering = true;

            PlayFlutterParticles();

            movement.y = flutterSpeed;
            yield return new WaitForSeconds(0.7f);
            movement.y = 0.5f;

            isFluttering = false;
        }

        private void FreeFall()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isFreeFalling = true;

                fallingTimer = Time.time;

                movement.y = -fallSpeed;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
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

            yield return new WaitForSeconds(timer / 2f);

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

        private void PlayMoveParticles()
        {
            if (movement != Vector3.zero)
            {
                if (!MovementParticle.isPlaying)
                {
                    MovementParticle.Play();
                }
            }
            else
            {
                MovementParticle.Stop();
            }
        }

        private void PlayFlutterParticles()
        {
            if (FlutterParticle.isPlaying)
            {
                FlutterParticle.Stop();
            }

            FlutterParticle.Play();
        }

        private void ChangeAnimLayer()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Vuelo"))
            {
                animator.SetLayerWeight(animator.GetLayerIndex("Oscilation"), 1f);
            }
            else
            {
                animator.SetLayerWeight(animator.GetLayerIndex("Oscilation"), 0f);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Ground") && moveInput == Vector2.zero)
            {
                animator.SetBool("isFlying", false);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Ground"))
            {
                animator.SetBool("isFlying", true);
            }
        }
    }
}