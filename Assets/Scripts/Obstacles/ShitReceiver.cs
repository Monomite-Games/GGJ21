using UnityEngine;

namespace Palomas.Requests
{
    public class ShitReceiver : MonoBehaviour
    {
        [SerializeField]
        private Collider2D Collider;

        [SerializeField]
        private Animator Animator;

        public void DisabelBadCollider()
        {
            Collider.enabled = false;
            Animator.SetTrigger("Cagado");
        }
    }
}