using Palomas.Items;
using UnityEngine;

namespace Palomas
{
    public class RetainedSpawnPointState : SpawnPointState
    {
        [SerializeField]
        private Collider2D Collider;

        [SerializeField]
        private Animator Animator;

        protected override void Deactivate()
        {
            base.Deactivate();

            Collider.enabled = true;
            Animator.SetBool("Relax", true);
        }
    }
}
