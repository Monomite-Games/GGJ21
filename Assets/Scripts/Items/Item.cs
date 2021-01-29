using UnityEngine;

namespace Palomas.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private int Id;

        public int GetId()
        {
            return Id;
        }

        public void Disappear()
        {
            //TODO
            Debug.Log("Item delivered: " + Id);
            Destroy(gameObject);
        }
    }
}