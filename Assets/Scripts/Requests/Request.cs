using UnityEngine;

using Palomas.Items;

namespace Palomas.Requests
{
    public class Request : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private int Id;

        [SerializeField]
        private int ItemId;

        private bool IsCompleted = false;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(GameConstants.TAG_ITEM))
            {
                Item item = other.gameObject.GetComponent<Item>();
                if(!IsCompleted && item.GetId().Equals(this.ItemId))
                {
                    IsCompleted = true;
                    item.Disappear();

                    GameEvents.OnRequestCompleted(Id);
                }
            }
        }
    }
}