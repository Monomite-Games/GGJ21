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

        private int Points;

        private bool InUse = false;

        public string GetId()
        {
            return this.Id;
        }

        public string GetItemId()
        {
            return this.ItemId;
        }

        public int GetPoints()
        {
            return this.Points;
        }

        public bool IsInUse()
        {
            return this.InUse;
        }

        private void Start()
        {
            GameEvents.RequestChanged += (sender, args) => { if (args.RequestId.Equals(this.Id)) { Activate(args.ItemId, args.SpawnLevel); } };
            GameEvents.RequestCompleted += (sender, args) => { if (args.RequestId.Equals(this.Id)) { OnCompleted(); } };
        }

        private void Activate(string itemId, int spawnLevel)
        {
            InUse = true;
            ItemId = itemId;
            Points = spawnLevel * GameConstants.POINTS_PER_LEVEL;
        }

        private void OnCompleted()
        {
            InUse = false;
            ItemId = string.Empty;
            GameEvents.OnItemDelivered(ItemId);
        }
    }
}