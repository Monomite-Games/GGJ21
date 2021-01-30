using UnityEngine;

using Palomas.Items;

namespace Palomas.Requests
{
    public class Request : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private string Id;
        
        private ItemTypes ItemId;

        private int Points;

        private bool InUse = false;

        public string GetId()
        {
            return Id;
        }

        public ItemTypes GetItemId()
        {
            return ItemId;
        }

        public int GetPoints()
        {
            return Points;
        }

        public bool IsInUse()
        {
            return InUse;
        }

        private void Start()
        {
            GameEvents.RequestChanged += (sender, args) => { if (args.RequestId.Equals(this.Id)) { Activate(args.ItemId, args.SpawnLevel); } };
            GameEvents.RequestCompleted += (sender, args) => { if (args.RequestId.Equals(this.Id)) { OnCompleted(); } };
        }

        private void Activate(ItemTypes itemId, int spawnLevel)
        {
            InUse = true;
            ItemId = itemId;
            Points = spawnLevel * GameConstants.POINTS_PER_LEVEL;
        }

        private void OnCompleted()
        {
            InUse = false;
            ItemId = ItemTypes.None;
            GameEvents.OnItemDelivered(ItemId);
        }
    }
}