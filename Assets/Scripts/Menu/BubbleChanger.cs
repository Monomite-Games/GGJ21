using UnityEngine;

namespace Palomas.Menu
{
    public class BubbleChanger : MonoBehaviour
    {
        private GameEvents GameEvents => GameEvents.Instance;

        [SerializeField]
        private int RequestId;

        private void Start()
        {
            GameEvents.RequestCompleted += (sender, args) => { if (args.RequestId.Equals(this.RequestId)) { ChangeToCompleted(); } };
        }

        private void ChangeToCompleted()
        {
            Debug.Log("Request completed: " + RequestId);
        }
    }
}
