using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class PigeonTakeObject : MonoBehaviour
    {
        public Transform Claws;

        public bool canInteract = false;
        public bool isHolding = false;

        public float rayDistance;

        public GameObject objectDownstairs;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && canInteract)
            {
                if (!isHolding)
                {
                    isHolding = true;
                    TakeItem();
                    Debug.Log("coger");
                }
                else
                {
                    isHolding = false;
                    DropItem();
                }
            }
        }

        private void FixedUpdate()
        {
            CheckDownstairs();
        }

        private void CheckDownstairs()
        {
            RaycastHit2D hit = Physics2D.Raycast(Claws.position, -Vector2.up, rayDistance);

            if (hit.collider != null && hit.transform.CompareTag("Item"))
            {
                objectDownstairs = hit.transform.gameObject;
                canInteract = true;
            }
            else
            {
                objectDownstairs = null;
                canInteract = false;
            }
        }

        private void TakeItem()
        {
            Rigidbody2D itemRb = objectDownstairs.GetComponent<Rigidbody2D>();

            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), objectDownstairs.GetComponent<Collider2D>());

            itemRb.isKinematic = true;

            objectDownstairs.transform.parent = Claws;
        }

        private void DropItem()
        {
            objectDownstairs.transform.parent = null;

            Rigidbody2D rb2D = objectDownstairs.GetComponent<Rigidbody2D>();
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), objectDownstairs.GetComponent<Collider2D>(), false);

            rb2D.isKinematic = false;
        }
    }
}