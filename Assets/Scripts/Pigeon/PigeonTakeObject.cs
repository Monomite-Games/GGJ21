using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class PigeonTakeObject : MonoBehaviour
    {
        public Transform Claws;

        private bool canInteract = false;
        private bool isHolding = false;

        private RaycastHit focus;
        private Rigidbody nearestItem;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && canInteract)
            {
                nearestItem = focus.collider.transform.GetComponent<Rigidbody>();

                if (nearestItem != null)
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
        }

        private void FixedUpdate()
        {
            CheckDownstairs();
        }

        private void CheckDownstairs()
        {
            float distance = 1.5f;

            if (Physics.Raycast(transform.position, Vector3.down, out focus, distance) && focus.collider.transform.CompareTag("Item"))
            {
                canInteract = true;
            }
            else
            {
                canInteract = false;
            }
        }

        private void TakeItem()
        {
            nearestItem.useGravity = false;
            nearestItem.transform.parent = Claws;
        }

        private void DropItem()
        {
            nearestItem.useGravity = true;
            nearestItem.isKinematic = false;
            nearestItem.transform.parent = null;
        }
    }
}