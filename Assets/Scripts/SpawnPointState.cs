using UnityEngine;

namespace Palomas
{
    public class SpawnPointState : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        private string ItemId = string.Empty;
        private bool InUse => !string.IsNullOrEmpty(ItemId);

        public void SetItemId(string itemId)
        {
            ItemId = itemId;
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
            ItemId = string.Empty;
        }
    }
}
