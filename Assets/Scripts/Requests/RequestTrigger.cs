using UnityEngine;

using Palomas.Items;

namespace Palomas.Requests
{
    public class RequestTrigger : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        private Request Request;

        private void Start()
        {
            Request = GetComponentInParent<Request>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameConstants.TAG_ITEM))
            {
                ItemController item = other.gameObject.GetComponent<ItemController>();
                if (Request.IsInUse() && item.GetItemId().Equals(Request.GetItemId()))
                {
                    GameEvents.OnRequestCompleted(Request.GetId());
                }
            }
        }
    }
}
