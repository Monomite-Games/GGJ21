using UnityEngine;

using Palomas.Items;

namespace Palomas.Requests
{
    public class Request : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private string Id;

        [SerializeField]
        private string ItemId;

        private bool Completed = false;

        public string GetId()
        {
            return this.Id;
        }

        public string GetItemId()
        {
            return this.ItemId;
        }

        public bool IsCompleted()
        {
            return this.Completed;
        }

        private void Start()
        {
            GameEvents.RequestCompleted += (sender, args) => OnCompleted();
        }

        private void OnCompleted()
        {
            Completed = true;
            GameEvents.OnItemDelivered(ItemId);
        }
    }
}