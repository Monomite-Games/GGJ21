using UnityEngine;

using Palomas.Requests;
using Palomas.Items;

namespace Palomas.Menu
{
    public class BubbleController : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;
        private ItemsList ItemsList => ItemsList.Instance;

        [SerializeField]
        private Transform ItemHolder;

        private Request Request;

        private void Start()
        {
            Request = GetComponentInParent<Request>();

            GameEvents.RequestObtained += (sender, args) => { if (args.RequestId.Equals(Request.GetId())) { ChangeToObtained(); } };
            GameEvents.RequestCompleted += (sender, args) => { if (args.RequestId.Equals(Request.GetId())) { ChangeToCompleted(); } };
        }

        private void ChangeToObtained()
        {
            Item item = ItemsList.GetById(Request.GetItemId());
            GameObject itemObject = GameObject.Instantiate(item.GetPrefab(), ItemHolder);
            itemObject.GetComponent<BoxCollider2D>().enabled = false;
            itemObject.GetComponent<Rigidbody2D>().isKinematic = true;
            
        }

        private void ChangeToCompleted()
        {
            Debug.Log("Request completed: " + Request.GetId());
        }
    }
}
