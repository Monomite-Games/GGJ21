using UnityEngine;

namespace Palomas.Items
{
    public class Item : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private string Id;

        [SerializeField]
        private GameObject Prefab;

        [SerializeField]
        private string Name;

        private bool InUse;

        public string GetId()
        {
            return Id;
        }

        public GameObject GetPrefab()
        {
            return Prefab;
        }

        public bool IsInUse()
        {
            return InUse;
        }

        public string GetName()
        {
            return Name;
        }

        private void Start()
        {
            GameEvents.RequestChanged += (sender, args) => { if (args.ItemId.Equals(this.Id)) { Activate(); } };
            GameEvents.RequestCompleted += (sender, args) => { if (args.ItemId.Equals(this.Id)) { Deactivate(); } };
        }

        private void Activate()
        {
            InUse = true;
        }

        private void Deactivate()
        {
            InUse = false;
        }
    }
}