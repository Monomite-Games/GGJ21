using UnityEngine;

using Palomas.Requests;

namespace Palomas
{
    public class PersonAnimatorController : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private Animator Animator;

        private Request Request;

        private void Start()
        {
            Request = GetComponentInParent<Request>();

            GameEvents.RequestChanged += (sender, args) => 
            { 
                if (args.RequestId.Equals(Request.GetId())) 
                { 
                    Animator.SetBool("Perdido", true); 
                } 
            };
            GameEvents.RequestCompleted += (sender, args) => { if (args.RequestId.Equals(Request.GetId())) { Animator.SetBool("Relax", true); } };
        }
    }
}