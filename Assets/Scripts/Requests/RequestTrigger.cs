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

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(GameConstants.TAG_ITEM))
            {
                ItemController item = other.gameObject.GetComponent<ItemController>();
                if (Request.IsInUse() && !item.GetItemstatus() && item.GetItemId().Equals(Request.GetItemId()))
                {
                    item.Disappear();
                    GameEvents.OnRequestCompleted(Request.GetId(), Request.GetItemId(), Request.GetPoints());
                }
            }
        }
    }
}
