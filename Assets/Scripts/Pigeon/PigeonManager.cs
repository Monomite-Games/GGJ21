using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Palomas.Pigeon
{
    public class PigeonManager : MonoBehaviour
    {
        public PigeonMovement Movement;
        public PigeonShit Shit;
        public PigeonTakeObject TakeObject;
        public Animator Animator;
        public Rigidbody2D Rigidbody;

        public void Fenecimiento()
        {
            this.gameObject.name = "fenecido";

            transform.position = new Vector3(transform.position.x, transform.position.y, 5f);

            Movement.enabled = false;
            Shit.enabled = false;
            TakeObject.enabled = false;

            Rigidbody.velocity = Vector3.zero;
            Rigidbody.isKinematic = true;
            Animator.enabled = false;
        }
    }
}