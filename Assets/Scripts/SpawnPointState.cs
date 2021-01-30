using Palomas.Items;
using UnityEngine;

namespace Palomas
{
    public class SpawnPointState : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        private ItemTypes ItemId = ItemTypes.None;

        [SerializeField]
        private int Level = 1;
        private bool InUse => ItemId != ItemTypes.None;

        public void SetItemId(ItemTypes itemId)
        {
            ItemId = itemId;
        }

        public int GetLevel()
        {
            return Level;
        }

        public bool IsInUse()
        {
            return InUse;
        }

        private void Start()
        {
            GameEvents.RequestCompleted += (sender, args) => { if (args.ItemId.Equals(this.ItemId)) { Deactivate(); } };
        }

        private void Deactivate()
        {
            ItemId = ItemTypes.None;
        }
    }
}
