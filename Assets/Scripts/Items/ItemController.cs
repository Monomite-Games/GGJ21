using UnityEngine;

namespace Palomas.Items
{
    public class ItemController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Collider2D col;

        [SerializeField]
        private ItemTypes ItemId;
        private bool isHeld;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
        }

        public ItemTypes GetItemId()
        {
            return this.ItemId;
        }

        public bool GetItemstatus()
        {
            return this.isHeld;
        }

        public Collider2D GetItemCollider()
        {
            return this.col;
        }

        public void TakeItem(Transform parent)
        {
            isHeld = true;
            rb.isKinematic = true;
            transform.parent = parent;
        }

        public void DropItem()
        {
            isHeld = false;
            rb.isKinematic = false;
            transform.parent = null;
        }

        public void Disappear()
        {
            //TODO
            Destroy(gameObject);
        }
    }
}
