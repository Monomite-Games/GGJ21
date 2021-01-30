using UnityEngine;

using Palomas.Items;

namespace Palomas.Requests
{
    public class Request : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private string Id;
        
        private string ItemId;

        private bool InUse = false;

        public string GetId()
        {
            return this.Id;
        }

        public string GetItemId()
        {
            return this.ItemId;
        }

        public bool IsInUse()
        {
            return this.InUse;
        }

        private void Start()
        {
            GameEvents.RequestChanged += (sender, args) => { if (args.RequestId.Equals(this.Id)) { Activate(args.ItemId); } };
            GameEvents.RequestCompleted += (sender, args) => { if (args.RequestId.Equals(this.Id)) { OnCompleted(); } };
        }

        private void Activate(string itemId)
        {
            InUse = true;
            ItemId = itemId;
        }

        private void OnCompleted()
        {
            InUse = false;
            GameEvents.OnItemDelivered(ItemId);
        }
    }
}