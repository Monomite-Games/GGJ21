using UnityEngine;
using UnityEngine.UI;

using Palomas.Requests;
using Palomas.Items;

namespace Palomas.Menu
{
    public class RequestView : MonoBehaviour
    {
        private RequestsList RequestsList => RequestsList.Instance;
        private ItemsList ItemsList => ItemsList.Instance;

        [SerializeField]
        private Text Text;

        [SerializeField]
        private Transform ItemHolder;

        public void SetRequestId(string requestId)
        {
            Request request = RequestsList.GetById(requestId);
            Item item = ItemsList.GetById(request.GetItemId());

            Text.text = item.GetName();
            GameObject itemObject = GameObject.Instantiate(item.GetPrefab(), ItemHolder);
            itemObject.GetComponent<BoxCollider2D>().enabled = false;
            itemObject.GetComponent<Rigidbody2D>().isKinematic = true;
            itemObject.transform.localScale = new Vector3(50.0f, 50.0f, 50.0f);
        }
    }
}
