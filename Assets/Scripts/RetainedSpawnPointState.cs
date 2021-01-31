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

        private void Start()
        {
            GameEvents.RequestChanged += (sener, args) => { if (args.ItemId.Equals(this.ItemId)) { Activate(); } };
            GameEvents.RequestCompleted += (sender, args) => { if (args.ItemId.Equals(this.ItemId)) { Deactivate(); } };
        }

        private void Activate()
        {
            Collider.enabled = true;
            Animator.SetBool("Relax", true);
        }
    }
}
