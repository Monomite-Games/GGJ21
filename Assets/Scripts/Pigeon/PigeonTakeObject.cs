using Palomas.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class PigeonTakeObject : MonoBehaviour
    {
        public Transform Claws;
        private Collider2D col;

        public bool canInteract = false;
        public bool isHolding = false;

        public float rayDistance;

        public ItemController heldItem;
        private Transform objectDownstairs;

        private void Start()
        {
            col = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && canInteract)
            {
                if (!isHolding)
                {
                    heldItem = objectDownstairs.GetComponent<ItemController>();
                    isHolding = true;
                    heldItem.TakeItem(Claws);
                    DisableCollisions(true);
                }
                else
                {   
                    isHolding = false;   
                    heldItem.DropItem();
                    DisableCollisions(false);
                    heldItem = null;
                }
            }
        }

        private void FixedUpdate()
        {
            if (!isHolding)
            {
                CheckDownstairs();
            }       
        }

        private void CheckDownstairs()
        {
            RaycastHit2D hit = Physics2D.Raycast(Claws.position, -Vector2.up, rayDistance);

            if (hit.collider != null && hit.transform.CompareTag("Item"))
            {
                objectDownstairs = hit.transform;
                canInteract = true;
            }
            else
            {
                objectDownstairs = null;
                canInteract = false;
            }
        }

        private void DisableCollisions(bool state)
        {
            Physics2D.IgnoreCollision(col, heldItem.GetItemCollider(), state);
        }
    }
}