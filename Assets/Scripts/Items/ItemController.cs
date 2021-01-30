using UnityEngine;

namespace Palomas.Items
{
    public class ItemController : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private string ItemId;

        private void Start()
        {
            GameEvents.ItemDelivered += (sender, args) => { if (args.ItemId.Equals(this.ItemId)) { Disappear(); } };
        }

        public string GetItemId()
        {
            return this.ItemId;
        }

        public void Disappear()
        {
            //TODO
            Debug.Log("Item delivered: " + ItemId);
            Destroy(gameObject);
        }
    }
}
